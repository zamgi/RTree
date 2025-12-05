using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using trees.win_forms.Properties;
using _Node_ = System.Collections.Generic.RTree< trees.win_forms.RTreeRECT >.Node;

namespace trees.win_forms
{
    /// <summary>
    /// 
    /// </summary>
    internal partial class RTreeForm : Form
    {
        #region [.ctor().]
        private Settings                _Settings;
        private RTree< RTreeRECT >      _RTree;
        private IReadOnlyList< _Node_ > _Nodes;
        private HashSet< _Node_ >       _Checked_Nodes;
        private Size                    _BoundRectSize;
        private _Node_                  _Selected_Node;
        private SearchQuery             _Query;
        private TimeSpan                _BuildElapsed;

        public RTreeForm()
        {
            InitializeComponent();

            _Settings = Settings.Default;
            _Checked_Nodes = new HashSet< _Node_ >();

            #region [.additional init of rtreeCanvas/etc.]
            rtreeCanvas.StartDrawSelectFigure += rtreeCanvas_StartDrawSelectFigure;
            rtreeCanvas.EndDrawSelectFigure   += rtreeCanvas_EndDrawSelectFigure;

            rtreeCanvas.IsDrawBboxs     = drawBboxsCheckBox.Checked = _Settings.IsDrawBboxs;
            rtreeCanvas.IsDrawTexts     = darwTextsCheckBox.Checked = _Settings.IsDrawBboxs;
            rtreeCanvas.SelectRectColor = selectColorComboBox.SelectColor = _Settings.SelectRectColor;
            rtreeCanvas.SelectFigure    = selectFigureComboBox.SelectFigure = (Enum.TryParse< SearchByMethodEnum >( _Settings.SelectFigure, true, out var sf ) ? sf : SearchByMethodEnum.Circle);

            fillNodeListCheckBox.Checked = _Settings.FillNodeList;
            objRectCountNUD.SetValue( _Settings.ObjRectCount );
            paddingX_NUD   .SetValue( _Settings.PADDING_X    );
            paddingY_NUD   .SetValue( _Settings.PADDING_Y    );
            minWH_NUD      .SetValue( _Settings.MIN_OBJ_WH   );
            maxWH_NUD      .SetValue( _Settings.MAX_OBJ_WH   );
            #endregion

            rebuildTreeButton_Click( null, null );
        }
        #endregion

        #region [.init & show tree.]
        private void InitByTree( RTree< RTreeRECT > rtree )
        {
            var nodes = rtree.GetNodes();
            //nodes.Sort( (x, y) => y.Envelope.Area.CompareTo( x.Envelope.Area ) );

            _RTree = rtree;
            _Nodes = nodes;
            _BoundRectSize = nodes.GetBoundRectSize();
            _Checked_Nodes.Clear();
            _Selected_Node = null;
            _Query         = null;
        }
        private void ShowTree()
        {
            Refill_rtTreeView( _RTree?.Root );
            Draw2Canvas();
            SetTitle();

            var scroll_sz = rtreeCanvas.AutoScrollMinSize;
            var sz = rtreeCanvas.ClientSize;
            rtreeCanvas.AutoScrollPosition = new Point( Math.Max( 0, (scroll_sz.Width - sz.Width) / 2 ), Math.Max( 0, (scroll_sz.Height - sz.Height) / 2 ) );
        }

        private async void RebuildTree( Func< RTree< RTreeRECT > > rebuildTreeFunc )
        {
            this.Enabled = false;
            try
            {
                if ( _RTree != null )
                {
                    InitByTree( TreeCreator.Empty() );
                    rtreeCanvas.Visible = false;
                    ShowTree();

                    await Task.Delay( 500 );
                }

                var sw = Stopwatch.StartNew();
                var rtree = await Task.Run( () => rebuildTreeFunc() );
                _BuildElapsed = sw.StopAndElapsed();

                InitByTree( rtree );
                ShowTree();                
            }
            finally
            {
                rtreeCanvas.Visible = true;
                this.Enabled = true;                
            }
            rtTreeView.SelectedNode = null;
            _Selected_Node = null;
        }
        #endregion


        #region [.override methods.]
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            if ( !base.DesignMode )
            {
                FormPositionStorer.Load( this, _Settings.RTreeFormPositionJson );

                mainSplitContainer.SplitterMoved   += new SplitterEventHandler(mainSplitContainer_SplitterMoved);
                canvasSplitContainer.SplitterMoved += new SplitterEventHandler(canvasSplitContainer_SplitterMoved);
            }
        }
        protected override void OnClosed( EventArgs e )
        {
            base.OnClosed( e );

            if ( !base.DesignMode )
            {
                _Settings.RTreeFormPositionJson = FormPositionStorer.Save( this );
                _Settings.SaveNoThrow();
            }
        }
        //protected override void OnShown( EventArgs e )
        //{
        //    base.OnShown( e );
        //    ShowTree();
        //}

        protected override void OnKeyDown( KeyEventArgs e )
        {
            if ( e.Control )  //Ctrl
            {
                switch ( e.KeyCode )
                {
                    case Keys.W: //Exit | Close
                        this.Close();
                        break;

                    case Keys.Z:
                    case Keys.X:
                        rebuildTreeButton_Click( null, null );
                        break;
                }
            }

            base.OnKeyDown( e );
        }
        #endregion

        private void SetTitle()
        {
            this.Text = $"[R-Tree count: {_RTree.Count}, nodes: {_Nodes.Count}, elapsed: {_BuildElapsed}]";
            if ( _Query != null )
            {
                this.Text += $"  =>  Search results: {(_Query.Results.AnyEx() ? _Query.Results.Count.ToString() : "0")}, (topN: {_Query.TopN}, elapsed: {_Query.SearchElapsed})";
            }
            repeatSearchButton.Visible = (_Query != null);
        }

        private void Draw2Canvas() => rtreeCanvas.Draw( _Nodes, (fillNodeListCheckBox.Checked ? IsNodeChecked : IsNodeChecked_Always), _BoundRectSize, _Query );

        private void Search( in (Envelope env, SearchByMethodEnum figure) t )
        {
            const int MIN_SEARCH_RECT_WIDTH = 15;
            const int MIN_SEARCH_RECT_HEIGHT = 15;

            if ( (MIN_SEARCH_RECT_WIDTH <= t.env.Width) && (MIN_SEARCH_RECT_HEIGHT <= t.env.Height) )
            {
                //const int topN = 10;
                var topN = _RTree.Count;

                var sw = Stopwatch.StartNew();
                var res = t.figure switch
                {
                    SearchByMethodEnum.Rect   => _RTree.Search_By_Rect( t.env, topN ),
                    SearchByMethodEnum.Circle => _RTree.Search_By_Circle( t.env.ToCircle_Outscribed(), topN ),
                    _ => throw (new ArgumentException( t.figure.ToString() ))
                };

                _Query = (t.env, t.figure, res, topN, sw.StopAndElapsed());
                Draw2Canvas();

                SetTitle();
            }
            else
            {
                Debug.WriteLine( $"empty rect: {t.env}" );
            }
        }
        private async void Repeat_Search()
        {
            if ( _Query != null )
            {
                var t = (_Query.Envelope, _Query.SearchByMethod);
                _Query = null;
                Draw2Canvas();
                SetTitle();

                await Task.Delay( 250 );

                Search( t );
            }
        }

        private void Refill_rtTreeView( _Node_ root, bool isNodesChecked = true )
        {
            void Refill_rtTreeView_Routine( _Node_ root, TreeNodeCollection tnodes )
            {
                var root_tn = new TreeNode( text: root.ToString() ) { Checked = isNodesChecked, Tag = root };
                tnodes.Add( root_tn );
                if ( isNodesChecked ) _Checked_Nodes.Add( root );

                if ( !root.IsLeaf )
                {
                    foreach ( var child in root.Children.Cast< _Node_ >() )
                    {
                        Refill_rtTreeView_Routine( child, root_tn.Nodes );
                    }
                }
            };

            var do_fillNodeList = !(mainSplitContainer.Panel1Collapsed = !fillNodeListCheckBox.Checked);

            var so = rtTreeView.SelectedNode;
            rtTreeView.BeginUpdate();
            rtTreeView.AfterSelect -= rtTreeView_AfterSelect;
            {
                var tnodes = rtTreeView.Nodes;
                tnodes.Clear();
                _Checked_Nodes.Clear();
                if ( do_fillNodeList && (root != null) )
                {
                    Refill_rtTreeView_Routine( root, tnodes );

                    rtTreeView.ExpandAll();
                }
            }
            rtTreeView.AfterSelect += rtTreeView_AfterSelect;
            rtTreeView.EndUpdate();
            if ( do_fillNodeList ) rtTreeView.SelectedNode = so;
            //--if ( do_fillNodeList && (rtListBox.SelectedItem == null) ) rtListBox.ClearSelected();            
        }
        private bool IsNodeChecked_Always( _Node_ n, out bool isSelected )
        {
            isSelected = false;
            return (true);
        }
        private bool IsNodeChecked( _Node_ n, out bool isSelected )
        {
            if ( _Checked_Nodes.Contains( n ) )
            {
                isSelected = (n == _Selected_Node);
                return (true);
            }
            isSelected = default;
            return (false);
        }

        private void rtTreeView_AfterCheck( object sender, TreeViewEventArgs e )
        {
            if ( e.Node == null ) return;

            var is_checked = e.Node.Checked;
            void check_childs_routine( TreeNode root )
            {
                var t = (_Node_) root.Tag;
                root.Checked = is_checked;
                if ( is_checked )
                {
                    _Checked_Nodes.Add( t );
                }
                else
                {
                    _Checked_Nodes.Remove( t );
                }

                foreach ( var n in root.Nodes.Cast< TreeNode >() )
                {                    
                    check_childs_routine( n );
                }
            }

            rtTreeView.AfterCheck -= rtTreeView_AfterCheck;
            check_childs_routine( e.Node );
            rtTreeView.AfterCheck += rtTreeView_AfterCheck;
            Draw2Canvas();
        }
        private void rtTreeView_AfterSelect( object sender, TreeViewEventArgs e )
        {
            _Selected_Node = (_Node_) rtTreeView.SelectedNode?.Tag;
            Draw2Canvas();
        }
        private void rtTreeView_MouseUp( object sender, MouseEventArgs e )
        {
            if ( e.Button == MouseButtons.Left )
            {
                var ht = rtTreeView.HitTest( e.Location );
                if ( rtTreeView.SelectedNode != ht.Node )
                {
                    rtTreeView.SelectedNode = ht.Node;
                    _Selected_Node = (_Node_) ht.Node?.Tag;
                    Draw2Canvas();
                }
            }
        }
        private void checkAllMenuItem_Click( object sender, EventArgs e )
        {
            Refill_rtTreeView( _RTree?.Root );
            Draw2Canvas();
        }
        private void unckeckAllMenuItem_Click( object sender, EventArgs e )
        {
            Refill_rtTreeView( _RTree?.Root, isNodesChecked: false );
            Draw2Canvas();
        }

        private void rebuildTreeButton_Click( object sender, EventArgs e )
        {
            var maxEntries = maxEntriesNUD.ValueAsInt32;
            _Settings.ObjRectCount = objRectCountNUD.ValueAsInt32;
            _Settings.PADDING_X    = paddingX_NUD.ValueAsInt32;
            _Settings.PADDING_Y    = paddingY_NUD.ValueAsInt32;
            _Settings.MIN_OBJ_WH   = minWH_NUD.ValueAsInt32;
            _Settings.MAX_OBJ_WH   = maxWH_NUD.ValueAsInt32;

            if ( _Settings.MAX_OBJ_WH < _Settings.MIN_OBJ_WH ) { _Settings.MIN_OBJ_WH = _Settings.MAX_OBJ_WH; minWH_NUD.SetValue( _Settings.MIN_OBJ_WH ); }

            RebuildTree( () => TreeCreator.Create_2( _Settings.ObjRectCount,
                                                     maxEntries,
                                                     _Settings.PADDING_X,
                                                     _Settings.PADDING_Y,
                                                     _Settings.MIN_OBJ_WH,
                                                     _Settings.MAX_OBJ_WH ) );
            //RebuildTree( () => TreeCreator.Create_1( shuffleObjectBeforeAdd2Tree: true, maxEntries ) );
        }

        private void rtreeCanvas_StartDrawSelectFigure( object sender, EventArgs e ) { }
        private void rtreeCanvas_EndDrawSelectFigure( object sender, (Envelope env, SearchByMethodEnum figure) t ) => Search( t );

        private void drawMode_Changed( object sender, EventArgs e )
        {
            _Settings.IsDrawBboxs = rtreeCanvas.IsDrawBboxs = drawBboxsCheckBox.Checked;
            _Settings.IsDrawTexts = rtreeCanvas.IsDrawTexts = darwTextsCheckBox.Checked;
            _Settings.SaveNoThrow();

            Draw2Canvas();
        }
        private void fillNodeListCheckBox_CheckStateChanged( object sender, EventArgs e )
        {
            _Settings.FillNodeList = fillNodeListCheckBox.Checked;
            _Settings.SaveNoThrow();
            
            Refill_rtTreeView( _RTree?.Root );
            mainSplitContainer_SplitterMoved( sender, /*e*/null );
            Draw2Canvas();
        }
        private void selectColorComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            _Settings.SelectRectColor = rtreeCanvas.SelectRectColor = selectColorComboBox.SelectColor;
            _Settings.SaveNoThrow();

            Draw2Canvas();
        }
        private void selectFigureComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            _Settings.SelectFigure = (rtreeCanvas.SelectFigure = selectFigureComboBox.SelectFigure).ToString();
            _Settings.SaveNoThrow();
        }

        private void repeatSearchButton_Click( object sender, EventArgs e ) => Repeat_Search();

        private void mainSplitContainer_SplitterMoved( object sender, SplitterEventArgs e ) => paddingX_NUD.ValueAsInt32 = (mainSplitContainer.Panel1.Width / 2) + 40;
        private void canvasSplitContainer_SplitterMoved( object sender, SplitterEventArgs e ) => paddingY_NUD.ValueAsInt32 = (canvasSplitContainer.Panel2.Height / 2) + 50;
    }
}
