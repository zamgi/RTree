using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
            paddingX_NUD.SetValue( _Settings.PADDING_X );
            paddingY_NUD.SetValue( _Settings.PADDING_Y );
            minWH_NUD.SetValue( _Settings.MIN_OBJ_WH );
            maxWH_NUD.SetValue( _Settings.MAX_OBJ_WH );
            #endregion

            rebuildTree_2_Button_Click( null, null );
        }
        #endregion

        #region [.init & show tree.]
        private void InitByTree( RTree< RTreeRECT > rtree )
        {
            var nodes = rtree.GetNodes();
            nodes.Sort( (x, y) => y.Envelope.Area.CompareTo( x.Envelope.Area ) );

            _RTree         = rtree;
            _Nodes         = nodes;
            _BoundRectSize = nodes.GetBoundRectSize();
            _Checked_Nodes.Clear();
            _Selected_Node = null;
            _Query         = null;
        }
        private void ShowTree()
        {
            Refill_rtListBox( _Nodes );
            Draw2Canvas();
            SetTitle();

            var scroll_sz = rtreeCanvas.AutoScrollMinSize;
            var sz        = rtreeCanvas.ClientSize;
            rtreeCanvas.AutoScrollPosition = new Point( Math.Max( 0, (scroll_sz.Width - sz.Width) / 2 ), Math.Max( 0, (scroll_sz.Height - sz.Height) / 2 ) );
        }

        private async void RebuildTree( Func< RTree< RTreeRECT > > rebuildTreeFunc )
        {
            //var idx = rtListBox.SelectedIndex;
            this.Enabled = false;
            try
            {
                if ( _RTree != null )
                {
                    InitByTree( TreeCreator.Empty() );
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
                this.Enabled = true;
            }
            rtListBox.SelectedIndex = -1;// idx;
        }
        #endregion


        #region [.override methods.]
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            if ( !base.DesignMode )
            {
                FormPositionStorer.Load( this, _Settings.RTreeFormPositionJson );
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
                        rebuildTree_1_Button_Click( null, null );
                        break;

                    case Keys.X:
                        rebuildTree_2_Button_Click( null, null );
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
        }

        private void Draw2Canvas() => rtreeCanvas.Draw( _Nodes, (fillNodeListCheckBox.Checked ? IsNodeChecked : IsNodeChecked_Always), _BoundRectSize, _Query );

        private void Refill_rtListBox( IReadOnlyList< _Node_ > nodes, bool isNodesChecked = true )
        {
            var do_fillNodeList = !(mainSplitContainer.Panel1Collapsed = !fillNodeListCheckBox.Checked);            

            var so = rtListBox.SelectedItem;
            rtListBox.BeginUpdate();
            rtListBox.ItemCheck -= rtListBox_ItemCheck;
            {
                var items = rtListBox.Items;
                items.Clear();
                _Checked_Nodes.Clear();
                if ( do_fillNodeList && nodes.AnyEx() )
                {
                    foreach ( var t in nodes )
                    {
                        items.Add( t, isNodesChecked );
                        if ( isNodesChecked ) _Checked_Nodes.Add( t );
                    }
                }
            }
            rtListBox.ItemCheck += rtListBox_ItemCheck;
            rtListBox.EndUpdate();
            if ( do_fillNodeList ) rtListBox.SelectedItem = so;
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
                isSelected = (n == _Selected_Node );
                return (true);
            }
            isSelected = default;
            return (false);
        }

        private void rtListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            var t = (_Node_) rtListBox.Items[ e.Index ];
            if ( e.NewValue == CheckState.Checked )
            {
                _Checked_Nodes.Add( t );
            }
            else
            {
                _Checked_Nodes.Remove( t );
            }
            Draw2Canvas();
        }
        private void rtListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            _Selected_Node = ((rtListBox.SelectedIndex != -1) ? (_Node_) rtListBox.SelectedItem : default);
            Draw2Canvas();
        }
        private void checkAllMenuItem_Click( object sender, EventArgs e )
        {
            Refill_rtListBox( _Nodes );
            Draw2Canvas();
        }
        private void unckeckAllMenuItem_Click( object sender, EventArgs e )
        {
            Refill_rtListBox( _Nodes, isNodesChecked: false );
            Draw2Canvas();
        }

        private void rebuildTree_1_Button_Click( object sender, EventArgs e )
        {
            var maxEntries = maxEntries_1_NUD.ValueAsInt32;

            RebuildTree( () => TreeCreator.Create_1( shuffleObjectBeforeAdd2Tree: true, maxEntries ) );
        }
        private void rebuildTree_2_Button_Click( object sender, EventArgs e )
        {
            _Settings.ObjRectCount = objRectCountNUD.ValueAsInt32;
            _Settings.PADDING_X    = paddingX_NUD.ValueAsInt32;
            _Settings.PADDING_Y    = paddingY_NUD.ValueAsInt32;
            _Settings.MIN_OBJ_WH   = minWH_NUD.ValueAsInt32;
            _Settings.MAX_OBJ_WH   = maxWH_NUD.ValueAsInt32;

            if ( _Settings.MAX_OBJ_WH < _Settings.MIN_OBJ_WH ) { _Settings.MIN_OBJ_WH = _Settings.MAX_OBJ_WH; minWH_NUD.SetValue( _Settings.MIN_OBJ_WH ); }

            RebuildTree( () => TreeCreator.Create_2( _Settings.ObjRectCount, 
                                                     _Settings.PADDING_X, 
                                                     _Settings.PADDING_Y, 
                                                     _Settings.MIN_OBJ_WH,
                                                     _Settings.MAX_OBJ_WH ) );
        }

        private void rtreeCanvas_StartDrawSelectFigure( object sender, EventArgs e ) { }
        private void rtreeCanvas_EndDrawSelectFigure( object sender, (Envelope env, SearchByMethodEnum figure) t )
        {
            const int MIN_SEARCH_RECT_WIDTH  = 15;
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

        private void drawMode_Changed( object sender, EventArgs e )
        {
            _Settings.IsDrawBboxs  = rtreeCanvas.IsDrawBboxs = drawBboxsCheckBox.Checked;
            _Settings.IsDrawTexts  = rtreeCanvas.IsDrawTexts = darwTextsCheckBox.Checked;
            _Settings.SaveNoThrow();

            Draw2Canvas();
        }
        private void fillNodeListCheckBox_CheckStateChanged( object sender, EventArgs e )
        {
            _Settings.FillNodeList = fillNodeListCheckBox.Checked;
            _Settings.SaveNoThrow();

            Refill_rtListBox( _Nodes );
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
    }
}
