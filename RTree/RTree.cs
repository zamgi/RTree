using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RTree< T > : ISpatialDatabase< T >, ISpatialIndex< T > where T : ISpatialData
    {
        public  const int   DEFAULT_MAX_ENTRIES    = 9;
        private const int   MinValueOf_MAX_ENTRIES = 4;
        private const int   MinValueOf_MIN_ENTRIES = 2;
        private const float DEFAULT_FILL_FACTOR    = 0.4f;

        private readonly EqualityComparer< T > _Comparer;
        private readonly int _MaxEntries;
        private readonly int _MinEntries;

        public static RTree< T > CreateFrom( IEnumerable< T > items )
        {
            var o = new RTree< T >();
            o.BulkLoad( items );
            return (o);
        }
        //public RTree() : this( DEFAULT_MAX_ENTRIES ) { }
        public RTree( int? maxEntries ) : this( maxEntries.GetValueOrDefault( DEFAULT_MAX_ENTRIES ) ) { }
        public RTree( int maxEntries = DEFAULT_MAX_ENTRIES, EqualityComparer< T > comparer = null )
        {
            _Comparer = comparer ?? EqualityComparer< T >.Default;
            _MaxEntries = Math.Max( MinValueOf_MAX_ENTRIES, maxEntries );
            _MinEntries = Math.Max( MinValueOf_MIN_ENTRIES, (int) MathF.Ceiling( _MaxEntries * DEFAULT_FILL_FACTOR ) );

            Clear();
        }

        public Node Root { get; private set; }
        public ref readonly Envelope Envelope => ref Root.Envelope;

        private int _Count;
        public int Count => _Count;

        public void Clear()
        {
            Root = new Node( new List< ISpatialData >(), 1 );
            _Count = 0;
        }

        public IReadOnlyList< T > SearchAll()
        {
            var lst = new List< T >( _Count );
            FillAllChildren( this.Root, lst );
            return (lst);
        }
        public IReadOnlyList< (T t, float dist) > Search_By_Rect( in Envelope boundingBox, int topN ) => DoSearch_By_Rect( boundingBox, topN );
        public IReadOnlyList< (T t, float dist) > Search_By_Circle( in Circle circle, int topN ) => DoSearch_By_Circle( circle, topN );

        public bool TrySearch_By_Rect( in Envelope boundingBox, int topN, out IReadOnlyList< (T t, float dist) > res ) => DoTrySearch_By_Rect( boundingBox, topN, out res );
        public bool TrySearch_By_Circle( in Circle circle, int topN, out IReadOnlyList< (T t, float dist) > res ) => DoTrySearch_By_Circle( circle, topN, out res );

        public bool TrySearchFirst_By_Rect( in Envelope boundingBox, out (T t, float dist) res ) => DoTrySearchFirst_By_Rect( boundingBox, out res );
        public bool TrySearchFirst_By_Circle( in Circle circle, out (T t, float dist) res ) => DoTrySearchFirst_By_Circle( circle, out res );

        public List< Node > GetNodes()
        {
            var lst = new List< Node >( _Count / 5 );
            FillAllChildrenNodes( this.Root, lst );
            return (lst);
        }

        public void Add( T t )
        {
            Insert( t, this.Root.Height );
            _Count++;
        }

        public void BulkLoad( List< ISpatialData > data )
        {
            if ( data.Count == 0 ) return;

            if ( this.Root.IsLeaf && (this.Root.Children.Count + data.Count < _MaxEntries) )
            {
                foreach ( var i in data )
                {
                    Add( (T) i );
                }
                return;
            }

            if ( data.Count < _MinEntries )
            {
                foreach ( var i in data )
                {
                    Add( (T) i );
                }
                return;
            }

            var dataRoot = BuildTree( data );
            _Count += data.Count;

            if ( this.Root.Children.Count == 0 )
            {
                this.Root = dataRoot;
            }
            else if ( this.Root.Height == dataRoot.Height )
            {
                if ( this.Root.Children.Count + dataRoot.Children.Count <= _MaxEntries )
                {
                    foreach ( var isd in dataRoot.Children )
                    {
                        this.Root.Add( isd );
                    }
                }
                else
                {
                    SplitRoot( dataRoot );
                }
            }
            else
            {
                if ( this.Root.Height < dataRoot.Height )
                {
                    var tmp = this.Root;
                    this.Root = dataRoot;
                    dataRoot = tmp;
                }

                this.Insert( dataRoot, this.Root.Height - dataRoot.Height );
            }
        }

        private List< ISpatialData > _Buf = new List< ISpatialData >();
        public void BulkLoad( IEnumerable< T > items )
        {
            _Buf.AddRange( items.Cast< ISpatialData >() );
            BulkLoad( _Buf );
            _Buf.Clear();
        }

        public void Remove( T t )
        {
            var candidates = DoPathSearch( t.Envelope );
            var seq = candidates.Where( c =>
            {
                if ( c.Peek() is T tt )
                    return (_Comparer.Equals( t, tt ));
                return (false);
            });

            foreach ( var c in seq )
            {
                var path = c.Pop();
                ((Node) path.Peek()).Remove( t );
                _Count--;
                while ( !path.IsEmpty )
                {
                    path = path.Pop( out var e );
                    var n = (Node) e;
                    if ( n.Children.Count != 0 )
                    {
                        n.ResetEnvelope();
                    }
                    else
                    {
                        if ( !path.IsEmpty )
                        {
                            ((Node) path.Peek()).Remove( n );
                        }
                    }
                }
            }
        }

        public override string ToString() => Count.ToString();
    }
}
