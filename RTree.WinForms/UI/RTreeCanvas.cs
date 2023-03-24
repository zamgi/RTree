using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

using _Node_ = System.Collections.Generic.RTree< trees.win_forms.RTreeRECT >.Node;
using M = System.Runtime.CompilerServices.MethodImplAttribute;
using O = System.Runtime.CompilerServices.MethodImplOptions;
using D = System.ComponentModel.DesignerSerializationVisibilityAttribute;
using X = System.ComponentModel.DesignerSerializationVisibility;

namespace trees.win_forms
{
    /// <summary>
    /// 
    /// </summary>
    internal partial class RTreeCanvas : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public delegate bool DrawItemInfoDelegate( _Node_ n, out bool isSelected );

        #region [.ctor().]
        private Brush _BackBrush;
        private Brush _RootFillBrush;
        private Brush _FillBrush;
        private Pen   _SelectBoundRectPen;
        private Pen   _SelectRectPen;
        private Pen   _BoundRectPen;
        private Pen   _RectPen;
        private HashSet< RTreeRECT > _SelectedRTs;
        private Image      _Image;
        private HatchBrush _SearchResHighligthHBrush;
        private Pen        _SearchRectPen;
        public RTreeCanvas()
        {
            this.SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true );

            _BackBrush                = Brushes.White;
            _SelectBoundRectPen       = new Pen( Brushes.Red, width: 2.0f );
            _SelectRectPen            = new Pen( Brushes.BlueViolet );
            _BoundRectPen             = Pens.Gray;
            _RectPen                  = Pens.Blue;
            _RootFillBrush            = Brushes.LightGreen;
            _FillBrush                = new HatchBrush( HatchStyle.BackwardDiagonal, Color.Violet, Color.Transparent ); //= Brushes.Violet;
            _SelectRectColor          = Color.Green;
            _SearchResHighligthHBrush = new HatchBrush( HatchStyle.BackwardDiagonal, _SelectRectColor, Color.Transparent );
            _SearchRectPen            = new Pen( _SelectRectColor, width: 2.0f );
            _Image                    = new Bitmap( 100, 100 );
            _SelectedRTs              = new HashSet< RTreeRECT >();
            Clear();

            this.AutoScroll        = true;
            this.AutoScrollMinSize = new Size( _Image.Width, _Image.Height );

            _ScrollIfNeedTimer = new Timer() { Interval = ScrollDelayInMilliseconds, Enabled = false };
            _ScrollIfNeedTimer.Tick += ScrollIfNeedTimer_Tick;
        }
        protected override void Dispose( bool disposing )
        {
            base.Dispose( disposing );

            _ScrollIfNeedTimer.Enabled = false;

            _Image.Dispose();
            _SelectRectPen.Dispose();
            _SelectBoundRectPen.Dispose();
            _SearchResHighligthHBrush.Dispose();
            _SearchRectPen.Dispose();
            _FillBrush.Dispose();
        }
        #endregion

        [D(X.Hidden)] public bool IsDrawBboxs { get; set; } = true;
        [D(X.Hidden)] public bool IsDrawTexts { get; set; } = true;

        private void SetImageSize( in Size sz )
        {
            if ( _Image.Size != sz )
            {
                var size = new Size( Math.Max( sz.Width, 100 ), Math.Max( sz.Height, 100 ) );

                var prev_Image = _Image;
                _Image = new Bitmap( size.Width, size.Height );
                this.AutoScrollMinSize = new Size( _Image.Width, _Image.Height );

                using ( var gr = Graphics.FromImage( _Image ) )
                {
                    Clear( gr );
                    gr.DrawImage( prev_Image, Point.Empty );
                }
                prev_Image.Dispose();

                this.Invalidate();
            }
        }
        private void Clear( Graphics gr ) => gr.FillRectangle( _BackBrush, new Rectangle( Point.Empty, _Image.Size ) );
        private void Clear()
        {
            using ( var gr = Graphics.FromImage( _Image ) )
            {
                Clear( gr );
            }
            this.Invalidate();
        }
        public void Draw( IReadOnlyList< _Node_ > nodes, DrawItemInfoDelegate drawItemFunc, in Size boundRectSize, SearchQuery query )
        {
            #region [.empty/no-nodes.]
            if ( !nodes.AnyEx() )
            {
                Clear();
                this.Invalidate();
                return;
            }
            #endregion


            var height = boundRectSize.Height;
            this.SetImageSize( boundRectSize );

            using var gr = Graphics.FromImage( _Image );
            Clear( gr );

            _SelectedRTs.Clear();
            foreach ( var n in nodes )
            {
                if ( drawItemFunc( n, out var isSelected ) )
                {
                    if ( n.IsLeaf )
                    {
                        var pen = (isSelected ? _SelectRectPen : _RectPen);
                        foreach ( var trc in n.Children.Cast< RTreeRECT >() )
                        {
                            var (x, y, w, h) = trc.Envelope.ToChoords( height );

                            if ( isSelected )
                            {
                                _SelectedRTs.Add( trc );

                                //if ( rt == t.RT )
                                //    gr.FillRectangle( _RootFillBrush, x, y, w, h );
                                //else
                                    gr.FillRectangle( _FillBrush, x, y, w, h );
                                gr.DrawRectangle( pen, x, y, w, h );
                                if ( IsDrawTexts )
                                    gr.DrawString( trc.Text, this.Font, Brushes.Black, x, y );
                            }
                            else if ( !_SelectedRTs.Contains( trc ) )
                            {
                                gr.DrawRectangle( pen, x, y, w, h );
                                if ( IsDrawTexts )
                                    gr.DrawString( trc.Text, this.Font, Brushes.Black, x, y );
                            }
                        }
                    }

                    //--------------------------------------------------//
                    if ( IsDrawBboxs )
                    {
                        var (x, y, w, h) = n.Envelope.ToChoords( height );

                        const float INDENT = 1.5f;
                        var pen = (isSelected ? _SelectBoundRectPen : _BoundRectPen);
                        gr.DrawRectangle( pen, x - INDENT, y - INDENT, w + 2 * INDENT, h + 2 * INDENT );                                
                    }
                }
            }

            DrawQuery( gr, height, query );

            this.Invalidate();
        }
        private void DrawQuery( Graphics gr, int boundRectHeight, SearchQuery query )
        {
            if ( query == null ) return;

            switch ( query.SearchByMethod )
            {
                case SearchByMethodEnum.Rect  : gr.DrawRectangle( _SearchRectPen, query.Envelope.ToRectangle( boundRectHeight ) ); break;
                case SearchByMethodEnum.Circle:
                    gr.DrawCircle_Outscribed( _SearchRectPen, query.Envelope.ToRectangle( boundRectHeight ) );
                    /*void draw_cross( int len = 7 )
                    {
                        var rc = query.Envelope.ToRectangle( boundRectHeight );
                        var pt = (x: rc.X + rc.Width / 2, y: rc.Y + rc.Height / 2);
                        gr.DrawLine( _SearchRectPen, pt.x - len, pt.y, pt.x + len, pt.y );
                        gr.DrawLine( _SearchRectPen, pt.x, pt.y - len, pt.x, pt.y + len );
                    };
                    draw_cross();
                    //*/
                    break;
                default: throw (new ArgumentException( query.SearchByMethod.ToString() ));
            }

            if ( query.Results.AnyEx() )
            {
                foreach ( var (rs, dist) in query.Results )
                {
                    gr.FillRectangle( _SearchResHighligthHBrush, rs.Envelope.ToRectangle( boundRectHeight ) );
                }
            }
        }

        protected override void OnScroll( ScrollEventArgs e )
        {
            base.OnScroll( e );

            using ( var gr = Graphics.FromHwnd( this.Handle ) )
            {
                var pt = new Point();
                if ( e.ScrollOrientation == ScrollOrientation.HorizontalScroll )
                {
                    pt.Y = this.AutoScrollPosition.Y;
                    pt.X = -e.NewValue;                    
                }
                else
                {
                    pt.X = this.AutoScrollPosition.X;
                    pt.Y = -e.NewValue;
                }
                gr.DrawImage( _Image, new RectangleF( pt, _Image.Size ) );
            }
        }
        protected override void OnPaint( PaintEventArgs e )
        {
            base.OnPaint( e );
            
            var pt = this.AutoScrollPosition;
            e.Graphics.DrawImage( _Image, new RectangleF( pt, _Image.Size ) );
        }



        #region [.draw select rows rect.]
        private Color _SelectRectColor;
        [D(X.Hidden)] public Color SelectRectColor 
        {
            get => _SelectRectColor;
            set
            {
                _SelectRectColor = value;

                _SearchResHighligthHBrush.Dispose();
                _SearchRectPen.Dispose();

                _SearchResHighligthHBrush = new HatchBrush( HatchStyle.BackwardDiagonal, _SelectRectColor, Color.Transparent );
                _SearchRectPen            = new Pen( _SelectRectColor, width: 2.0f );
            }
        }
        [D(X.Hidden)] public SearchByMethodEnum SelectFigure { get; set; } = SearchByMethodEnum.Circle;

        public event EventHandler StartDrawSelectFigure;
        public event EventHandler< (Envelope env, SearchByMethodEnum figure) > EndDrawSelectFigure;


        private bool      _DrawSelectRect;
        private Point     _SelectLocation;
        private int       _SelectVerticalScrollingOffset;
        private Rectangle _SelectRect;
        private Timer     _ScrollIfNeedTimer;

        private int VerticalScrollingOffset => this.AutoScrollPosition.Y;

        protected override void OnMouseDown( MouseEventArgs e )
        {
            _DrawSelectRect = (e.Button == MouseButtons.Left);
            if ( _DrawSelectRect )
            {
                _SelectVerticalScrollingOffset = this.VerticalScrollingOffset;
                _SelectLocation = e.Location;
                _SelectRect     = new Rectangle( _SelectLocation, Size.Empty ); //Rectangle.Empty;
                StartDrawSelectFigure?.Invoke( this, EventArgs.Empty );

                _ScrollIfNeedTimer.Interval = ScrollDelayInMilliseconds;
                _ScrollIfNeedTimer.Enabled  = true;
            }
        }
        protected override void OnMouseUp( MouseEventArgs e )
        {
            base.OnMouseUp( e );

            if ( _DrawSelectRect )
            {
                _ScrollIfNeedTimer.Enabled = false;
                _DrawSelectRect = false;
                this.Invalidate();

                //bottom_left_choords_starts 
                var env_bottom_left_choords_starts = _SelectRect.ToEnvelope( _Image.Size.Height, this.AutoScrollPosition );
                EndDrawSelectFigure?.Invoke( this, (env_bottom_left_choords_starts, SelectFigure) );
            }
        }
        protected override void OnMouseMove( MouseEventArgs e )
        {
            base.OnMouseMove( e );

            if ( _DrawSelectRect )
            {
                ProcessDrawSelectFigure( e.X, e.Y, SelectFigure );
            }
        }

        private void ProcessDrawSelectFigure( int current_x, int current_y, SearchByMethodEnum selectFigure )
        {
            switch ( selectFigure )
            {
                case SearchByMethodEnum.Rect  : ProcessDrawSelectRect  ( current_x, current_y ); break;
                case SearchByMethodEnum.Circle: ProcessDrawSelectCircle( current_x, current_y ); break;
                default: throw new ArgumentException( nameof(selectFigure) );
            }
        }
        private void ProcessDrawSelectRect( int current_x, int current_y )
        {
            if ( _DrawSelectRect )
            {
                using ( var gr = Graphics.FromHwnd( this.Handle ) )
                {
                    gr.DrawXORRectangle( _SelectRect, SelectRectColor );

                    var y0 = _SelectLocation.Y - (this.VerticalScrollingOffset - _SelectVerticalScrollingOffset);
                    var x = Math.Min( current_x, _SelectLocation.X );
                    var y = Math.Min( current_y, y0 );
                    _SelectRect = new Rectangle( x, y, Math.Abs( current_x - _SelectLocation.X ), Math.Abs( current_y - y0 ) );
                    gr.DrawXORRectangle( _SelectRect, SelectRectColor );
                }
            }
        }
        private void ProcessDrawSelectCircle( int current_x, int current_y )
        {
            if ( _DrawSelectRect )
            {
                using var gr = Graphics.FromHwnd( this.Handle );

                gr.DrawXORCircle( _SelectRect.ToCircle_Outscribed(), SelectRectColor );
                //---gr.DrawXORRectangle( _SelectRect, SelectRectColor );

                var sel_x = _SelectLocation.X;
                var sel_y = _SelectLocation.Y - (this.VerticalScrollingOffset - _SelectVerticalScrollingOffset);

                void draw_cross( int len = 7 )
                {
                    using var pen = new Pen( SelectRectColor, width: 2.0f );
                    gr.DrawLine( pen, sel_x - len, sel_y, sel_x + len, sel_y );
                    gr.DrawLine( pen, sel_x, sel_y - len, sel_x, sel_y + len );
                };
                draw_cross();

                var w = Math.Abs( sel_x - current_x );
                var h = Math.Abs( sel_y - current_y );
                var max_wh = Math.Max( w, h );

                var offset_x = 0;
                var offset_y = 0;
                if ( sel_x < current_x )
                {
                    if ( sel_y < current_y )
                    {
                        ;//_SelectRect = new Rectangle( sel_x, sel_y, max_wh, max_wh );
                    }
                    else
                    {
                        offset_y = max_wh;
                        //_SelectRect = new Rectangle( sel_x, sel_y - max_wh, max_wh, max_wh );
                    }
                }
                else
                {
                    if ( sel_y < current_y )
                    {
                        offset_x = max_wh;
                        //_SelectRect = new Rectangle( sel_x - max_wh, sel_y, max_wh, max_wh );
                    }
                    else
                    {
                        offset_y = offset_x = max_wh;
                        //_SelectRect = new Rectangle( sel_x - max_wh, sel_y - max_wh, max_wh, max_wh );
                    }
                }
                _SelectRect = new Rectangle( sel_x - offset_x, sel_y - offset_y, max_wh, max_wh );


                gr.DrawXORCircle( _SelectRect.ToCircle_Outscribed(), SelectRectColor );
                //---gr.DrawXORRectangle( _SelectRect, SelectRectColor );
            }
        }
        private void ScrollIfNeedTimer_Tick( object sender, EventArgs e )
        {
            if ( _DrawSelectRect )
            {
                var pt = this.PointToClient( Control.MousePosition );
                //if ( this.ScrollIfNeed( pt ) )
                //{
                //    ProcessDrawSelectRect( pt.X, pt.Y );
                //}
            }
            else
            {
                if ( _ScrollIfNeedTimer.Enabled ) _ScrollIfNeedTimer.Enabled = false;
            }            
        }
        #endregion

        #region [.Scroll if need to point.]
        private const int SCROLL_DELAY_IN_MILLISECONDS = 10;
        [D(X.Hidden)] public int ScrollDelayInMilliseconds { get; set; } = SCROLL_DELAY_IN_MILLISECONDS;

        /*private DateTime _LastScrollDateTime;
        public bool ScrollIfNeed( in Point pt )
        {
            if ( ShouldScrollUp( pt ) )
            {
                var firstIdx = this.GetFirstDisplayedScrollingRowIndex();
                if ( (0 < firstIdx) && (TimeSpan.FromMilliseconds( ScrollDelayInMilliseconds ) < (DateTime.Now - _LastScrollDateTime)) )
                {
                    this.SetFirstDisplayedScrollingRowIndex( firstIdx - 1 );
                    _LastScrollDateTime = DateTime.Now;
                    return (true);
                }
            }
            else if ( ShouldScrollDown( pt ) )
            {
                var firstIdx = this.GetFirstDisplayedScrollingRowIndex();
                if ( (firstIdx < (this.RowCount - 1)) && (TimeSpan.FromMilliseconds( ScrollDelayInMilliseconds ) < (DateTime.Now - _LastScrollDateTime)) )
                {
                    this.SetFirstDisplayedScrollingRowIndex( firstIdx + 1);
                    _LastScrollDateTime = DateTime.Now;
                    return (true);
                }
            }
            return (false);
        }
        [M(O.AggressiveInlining)] private bool ShouldScrollUp( in Point pt )
        {
            var columnHeadersHeight = this.ColumnHeadersHeight;
            return //pt.Y > columnHeadersHeight && //---remove column-up-edge---//
                   pt.Y < columnHeadersHeight + 15
                && pt.X >= 0
                && pt.X <= this.Bounds.Width;
        }
        [M(O.AggressiveInlining)] private bool ShouldScrollDown( in Point pt )
        {
            var bounds = this.Bounds;
            return pt.Y > bounds.Height - 15
                && pt.Y < bounds.Height
                && pt.X >= 0
                && pt.X <= bounds.Width;
        }
        */
        #endregion
    }
}
