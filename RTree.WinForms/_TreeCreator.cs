using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace trees.win_forms
{
    /// <summary>
    /// 
    /// </summary>
    internal static class TreeCreator
    {
        public static RTree< RTreeRECT > Empty() => new RTree< RTreeRECT >();

        public static RTree< RTreeRECT > Create_1( bool shuffleObjectBeforeAdd2Tree = false,
            int? maxEntries = null,
            bool useRandomWH = false, int MIN_WH = 20, int MAX_WH = 120 )
        {
            var array = new (float x, float y, string s)[]
            {
                (100, 100, "z-1"), 
                (200, 200, "z-2"),

                (254, 80, "a-1"),
                (399, 60, "a-2"),
                (634, 30, "a-3"),
                (584, 80, "a-4"),
                (297, 50, "a-5"),

                (80, 254, "a-6"),
                (460, 399, "a-7"),
                (30, 734, "a-8"),
                (280, 584, "a-9"),
                (50, 297, "a-10"),

                (200, 270, "b-1"),
                (111, 290, "b-2"),
                (552, 320, "b-3"),
                (443, 340, "b-4"),
                (795, 250, "b-5"),

                (270, 200, "b-6"),
                (290, 111, "b-7"),
                (320, 552, "b-8"),
                (340, 443, "b-9"),
                (250, 795, "b-10"),

                (700, 220, "c-1"),
                (100, 310, "c-2"),
                (411, 280, "c-3"),
                (712, 250, "c-4"),
                (310, 230, "c-5"),

                (220, 750, "c-6"),
                (310, 100, "c-7"),
                (280, 411, "c-8"),
                (250, 712, "c-9"),
                (230, 310, "c-10"),
            };

            var rnd = new Random();
            if ( shuffleObjectBeforeAdd2Tree )
            {                
                for ( var i = array.Length - 1; 0 <= i; i-- )
                {
                    var idx_1 = rnd.Next( 0, array.Length );
                    var idx_2 = rnd.Next( 0, array.Length );
                    var t = array[ idx_1 ];
                    array[ idx_1 ] = array[ idx_2 ];
                    array[ idx_2 ] = t;
                }
            }

            var rtree = new RTree< RTreeRECT >( maxEntries );
            rtree.BulkLoad( array.Select( a => new RTreeRECT( a, w: useRandomWH ? rnd.Next( MIN_WH, MAX_WH + 1 ) : MAX_WH, 
                                                                 h: useRandomWH ? rnd.Next( MIN_WH, MAX_WH + 1 ) : MAX_WH ) ) );

            return (rtree);
        }

        public static RTree< RTreeRECT > Create_2( int objCount = 50,
            int? maxEntries = null,
            int PADDING_X = 500 / 2, int PADDING_Y = 300 / 2,
            int MIN_WH = 20, int MAX_WH = 120 )
        {
            var rnd = new Random();
            var wa  = Screen.PrimaryScreen.WorkingArea;
            wa.Inflate( -PADDING_X, -PADDING_Y );
            
            var rtree = new RTree< RTreeRECT >( maxEntries );
            for ( var i = 1; i <= objCount; i++ )
            {
                var x = rnd.Next( wa.Left, wa.Right  + 1 );
                var y = rnd.Next( wa.Top , wa.Bottom + 1 );
                var w = rnd.Next( MIN_WH , MAX_WH + 1 );
                var h = rnd.Next( MIN_WH , MAX_WH + 1 );
                var rc = new RTreeRECT( x, y, w, h, $"{i}" );
                rtree.Add( rc );
            }

            return (rtree);
        }
    }
}
