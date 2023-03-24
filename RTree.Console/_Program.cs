using System;
using System.Collections.Generic;
using System.Linq;

namespace trees.console
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// 
        /// </summary>
        private sealed class RTreeNode : ISpatialData
        {
            private Envelope _Envelope;
            public RTreeNode( in (float x, float y, string s) t )
            {
                //---_Envelope = new Envelope( t.x, t.y, t.x + 1e-1f, t.y + 1e-1f );
                _Envelope = new Envelope( t.x, t.y, t.x + 1, t.y + 1 );
                Data = t;
            }
            public (float x, float y, string s) Data { get; }
            public ref readonly Envelope Envelope => ref _Envelope;
        }

        private static void Main( string[] args )
        {
            Console.ResetColor();

            var array = new (float x, float y, string s)[]
            {
                (10, 10, "z"), (20, 20, "z"),

                (1, 1, "a-1"),
                (2, 8, "a-2"),
                (3, 6, "a-3"),
                (4, 3, "a-4"),
                (4, 8, "a-5"),
                (7, 5, "a-6"),

                (2, 27, "b-1"),
                (1, 29, "b-2"),
                (2, 32, "b-3"),
                (3, 34, "b-4"),
                (5, 25, "b-5"),

                ( 9, 22, "c-1"),
                (10, 31, "c-2"),
                (11, 28, "c-3"),
                (12, 25, "c-4"),
                (10, 23, "c-5"),
            };

            var axis = DrawDots( array );

            var rtree = new RTree< RTreeNode >();
            rtree.AddRange< RTreeNode >/*BulkLoad*/( array.Select( a => new RTreeNode( a ) ) );
            rtree.Query( ref axis, 1, 0 );
            rtree.Query( ref axis, 3, 23 );
            rtree.Query( ref axis, 13, 3 );
            rtree.Query( ref axis, 10, 29 );
            rtree.Query( ref axis, 100, 103 );

            Console.ReadKey();
        }


        private static void Query( this RTree< RTreeNode > rtree, ref (int Top, int Bottom) axis, 
                                        float center_x, float center_y, 
                                        float width = 7, float height = 7, 
                                        int topN = 7 )
        {
            //DrawDot( center_x, center_y );
            //-------------------------------------------//

            var env = new Envelope( center_x - width / 2.0f, center_y - height / 2.0f, 
                                    center_x + width / 2.0f, center_y + height / 2.0f );
            DrawEnv( env, axis );
            var res = rtree.Search_By_Rect( env, topN );

            Console.WriteLine( $"query point: [{env.Center_X}:{env.Center_Y}], [{env.Width}x{env.Height}]" );
            if ( res.Any() )
            {
                for ( var i = 0; i < res.Count; i++ )
                {
                    var n = res[ i ];

                    Console.WriteLine( $"{i + 1}). [{n.t.Data.x}:{n.t.Data.y}], '{n.t.Data.s}' => dist: {n.dist:N3}" );
                }
            }
            else
            {
                Console.WriteLine( $" - (no)" );
            }
            Console.WriteLine( "------------------------------------------\r\n" );
            axis.Bottom = Console.CursorTop;
        }
        private static void AddRange< T >( this RTree< RTreeNode > rtree, IEnumerable< RTreeNode > array )
        {
            foreach ( var a in array )
            {
                rtree.Add( a );
            }
        }


        private const float X_MUL = 1.85f;
        private static (int Top, int Bottom) DrawDots( IEnumerable< (float x, float y, string s) > array )
        {
            var pos = Console.GetCursorPosition();
            Console.SetCursorPosition( 0, pos.Top );

            #region [.draw axis.]
            //Console.Clear();

            var max_x = (int) (array.Max( a => a.x ) + 7);
            var max_y = (int) (array.Max( a => a.y ));
            DrawRect( 1, pos.Top + 0, max_x + 1, pos.Top + max_y + 1, pos, ConsoleColor.White );
            #endregion

            var max_width  = short.MaxValue; // Console.WindowWidth  - 1;
            var max_height = short.MaxValue; // Console.WindowHeight - 1;

            var fc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach ( var a in array )
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition( Math.Min( max_width, (int) (X_MUL * a.x) ), pos.Top + Math.Min( max_height, (int) a.y ) );
                Console.Write( $"{a.x}:{a.y}" );
                Console.ForegroundColor = fc;
                Console.Write( $",{a.s}" );                
            }
            Console.ForegroundColor = fc;
            
            Console.SetCursorPosition( 0/*pos.Left*/, pos.Top + max_y + 4 );
            return (Top: pos.Top, Bottom: pos.Top + max_y + 4);
        }
        private static void DrawEnv( in Envelope env, in (int Top, int Bottom) axis )
        {
            DrawDot( env.Center_X, env.Center_Y, axis );
            DrawRect( env.Min_X, env.Min_Y, env.Max_X, env.Max_Y, axis );
        }
        private static void DrawDot( float x, float y, in (int Top, int Bottom) axis, ConsoleColor color = ConsoleColor.Red )
        {
            var max_width  = short.MaxValue; // Console.WindowWidth  - 1;
            var max_height = short.MaxValue; // Console.WindowHeight - 1;

            var pos = Console.GetCursorPosition();
            var fc = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.SetCursorPosition( Math.Min( max_width, (int) (X_MUL * x) ), axis.Top + Math.Min( max_height, (int) y) );
            Console.Write( $"{x}:{y}" ); // "x" );
            Console.ForegroundColor = fc;
            Console.SetCursorPosition( pos.Left, axis.Bottom );
        }
        private static void DrawRect( float min_x_, float min_y_, float max_x_, float max_y_, in (int Top, int Bottom) axis, ConsoleColor color = ConsoleColor.Green )
        {
            var pos = Console.GetCursorPosition();
            var fc = Console.ForegroundColor;
            Console.ForegroundColor = color;

            var max_width  = short.MaxValue; // Console.WindowWidth  - 1;
            var max_height = short.MaxValue; // Console.WindowHeight - 1;

            int between_h( int y ) => Math.Max( 0, Math.Min( max_height, y ) );
            int between_w( int x ) => Math.Max( 0, Math.Min( max_width , x ) );

            var min_x = between_w( (int) (X_MUL * min_x_) );
            var min_y = between_h( axis.Top + (int) min_y_ );
            var max_x = between_w( (int) (X_MUL * max_x_) );
            var max_y = between_h( axis.Top + (int) max_y_ );
            for ( var i = min_x; i <= max_x; i++ )
            {
                Console.SetCursorPosition( i, min_y );
                Console.Write( "_" );
            }
            for ( var i = min_x; i <= max_x; i++ )
            {
                Console.SetCursorPosition( i, max_y );
                Console.Write( "_" );
            }
            for ( var i = min_y + 1; i <= max_y; i++ )
            {
                Console.SetCursorPosition( min_x, i );
                Console.Write( "|" );
            }
            for ( var i = min_y + 1; i <= max_y; i++ )
            {
                Console.SetCursorPosition( max_x, i );
                Console.Write( "|" );
            }
            Console.ForegroundColor = fc;

            Console.SetCursorPosition( pos.Left, axis.Bottom/*max_y + 4*/ );
        }
    }
}
