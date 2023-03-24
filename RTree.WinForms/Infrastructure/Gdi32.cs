using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

using M = System.Runtime.CompilerServices.MethodImplAttribute;
using O = System.Runtime.CompilerServices.MethodImplOptions;

namespace trees.win_forms
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Gdi32
    {
        /// <summary>
        /// 
        /// </summary>
        private enum DrawingMode : int
        {
            R2_BLACK           = 1,   /*  0       */
            R2_NOTMERGEPEN     = 2,   /* DPon     */
            R2_MASKNOTPEN      = 3,   /* DPna     */
            R2_NOTCOPYPEN      = 4,   /* PN       */
            R2_MASKPENNOT      = 5,   /* PDna     */
            R2_NOT             = 6,   /* Dn       */
            R2_XORPEN          = 7,   /* DPx      */
            R2_NOTMASKPEN      = 8,   /* DPan     */
            R2_MASKPEN         = 9,   /* DPa      */
            R2_NOTXORPEN       = 10,  /* DPxn     */
            R2_NOP             = 11,  /* D        */
            R2_MERGENOTPEN     = 12,  /* DPno     */
            R2_COPYPEN         = 13,  /* P        */
            R2_MERGEPENNOT     = 14,  /* PDno     */
            R2_MERGEPEN        = 15,  /* DPo      */
            R2_WHITE           = 16,  /*  1       */
        }

        private const string GDI32_DLL = "gdi32.dll";
        [DllImport(GDI32_DLL)] private static extern bool Rectangle( IntPtr hDC, int left, int top, int right, int bottom );
        [DllImport(GDI32_DLL)] private static extern bool Ellipse( IntPtr hDC, int left, int top, int right, int bottom );
        [DllImport(GDI32_DLL)] private static extern int SetROP2( IntPtr hDC, /*int*/ DrawingMode fnDrawMode );
        [DllImport(GDI32_DLL)] private static extern bool MoveToEx( IntPtr hDC, int x, int y, ref Point p );
        [DllImport(GDI32_DLL)] private static extern bool LineTo( IntPtr hdc, int x, int y );
        [DllImport(GDI32_DLL)] private static extern IntPtr CreatePen( int fnPenStyle, int nWidth, int crColor );
        [DllImport(GDI32_DLL)] private static extern IntPtr SelectObject( IntPtr hDC, IntPtr hObj );
        [DllImport(GDI32_DLL)] private static extern bool DeleteObject( IntPtr hObj );

        [M(O.AggressiveInlining)] private static int ArgbToRGB( int rgb ) => ((rgb >> 16 & 0x0000FF) | (rgb & 0x00FF00) | (rgb << 16 & 0xFF0000));

        public static void DrawXORRectangle( this Graphics gr, in Rectangle rc, Color color, int penWidth = 2 )
        {
            IntPtr hDC = gr.GetHdc();
            IntPtr hPen = CreatePen( 0, penWidth, ArgbToRGB( color.ToArgb() ) );
            SelectObject( hDC, hPen );
            SetROP2( hDC, DrawingMode.R2_NOTXORPEN );
            Rectangle( hDC, rc.Left, rc.Top, rc.Right, rc.Bottom );
            DeleteObject( hPen );
            gr.ReleaseHdc( hDC );
        }

        public static void DrawXOREllipse_Inscribed( this Graphics gr, in Rectangle rc, Color color, int penWidth = 2 )
        {
            IntPtr hDC = gr.GetHdc();
            IntPtr hPen = CreatePen( 0, penWidth, ArgbToRGB( color.ToArgb() ) );
            SelectObject( hDC, hPen );
            SetROP2( hDC, DrawingMode.R2_NOTXORPEN );
            Ellipse( hDC, rc.Left, rc.Top, rc.Right, rc.Bottom );
            DeleteObject( hPen );
            gr.ReleaseHdc( hDC );
        }
        
        public static void DrawXORCircle_Inscribed( this Graphics gr, in Rectangle rc, Color color, int penWidth = 2 )
        {
            IntPtr hDC = gr.GetHdc();
            IntPtr hPen = CreatePen( 0, penWidth, ArgbToRGB( color.ToArgb() ) );
            SelectObject( hDC, hPen );
            SetROP2( hDC, DrawingMode.R2_NOTXORPEN );

            var x = rc.Left;
            var y = rc.Top;
            var max_wh = Math.Max( rc.Width, rc.Height );
            Ellipse( hDC, x, y, x + max_wh, y + max_wh );

            //Debug.WriteLine( $"x: {x}, y: {y}, max_x: {x + max_wh}, max_y: {y + max_wh}, wh: {max_wh}" );

            DeleteObject( hPen );
            gr.ReleaseHdc( hDC );
        }
        public static void DrawXORCircle_Outscribed( this Graphics gr, in Rectangle rc, Color color, int penWidth = 2 ) => gr.DrawXORCircle( rc.ToEnvelope().ToCircle_Outscribed(), color, penWidth );
        public static void DrawXORCircle( this Graphics gr, in Circle circle, Color color, int penWidth = 2 )
        {
            IntPtr hDC = gr.GetHdc();
            IntPtr hPen = CreatePen( 0, penWidth, ArgbToRGB( color.ToArgb() ) );
            SelectObject( hDC, hPen );
            SetROP2( hDC, DrawingMode.R2_NOTXORPEN );


            var left   = (int) (circle.Center_X - circle.Radius);
            var top    = (int) (circle.Center_Y - circle.Radius);
            var right  = (int) (circle.Center_X + circle.Radius);
            var bottom = (int) (circle.Center_Y + circle.Radius);

            Ellipse( hDC, left, top, right, bottom );

            //Debug.WriteLine( $"x: {x}, y: {y}, max_x: {x + max_wh}, max_y: {y + max_wh}, wh: {max_wh}" );

            DeleteObject( hPen );
            gr.ReleaseHdc( hDC );
        }

        public static void DrawXORCircle( this Graphics gr, in (Point center, int radius) circle, Color color, int penWidth = 2 ) => gr.DrawXORCircle( new Circle( circle.center.X, circle.center.Y, circle.radius ), color, penWidth );
        public static void DrawXORCircle( this Graphics gr, in Point center, int radius, Color color, int penWidth = 2 ) => gr.DrawXORCircle( new Circle( center.X, center.Y, radius ), color, penWidth );



        public static void DrawCircle_Outscribed( this Graphics gr, Pen pen, in Rectangle rc )
        {
            var circle = rc.ToEnvelope().ToCircle_Outscribed();

            var x = (int) (circle.Center_X - circle.Radius);
            var y = (int) (circle.Center_Y - circle.Radius);
            var w = (int) (2 * circle.Radius);
            var h = (int) (2 * circle.Radius);

            gr.DrawEllipse( pen, x, y, w, h );
        }            
    }
}
