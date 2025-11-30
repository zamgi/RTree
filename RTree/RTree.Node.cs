namespace System.Collections.Generic
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RTree< T >
    {
        /// <summary>
        /// 
        /// </summary>
        public class Node : ISpatialData
        {
            private Envelope _Envelope;
            private readonly List< ISpatialData > _Children;
            internal Node( List< ISpatialData > items, int height )
            {
                Height    = height;
                _Children = items;
                ResetEnvelope();
            }

            public int Height { get; }
            public bool IsLeaf => (Height == 1);
            public ref readonly Envelope Envelope => ref _Envelope;
            public IReadOnlyList< ISpatialData > Children => _Children;

            internal void Add( ISpatialData node )
            {
                _Children.Add( node );
                _Envelope = Envelope.Extend( node.Envelope );
            }
            internal void Remove( ISpatialData node )
            {
                _Children.Remove( node );
                ResetEnvelope();
            }
            internal void RemoveRange( int index, int count )
            {
                _Children.RemoveRange( index, count );
                ResetEnvelope();
            }
            internal void ResetEnvelope() => _Envelope = GetEnclosingEnvelope( _Children );


            internal void SortChildren( int minEntries )
            {
                _Children.Sort( Compares.CompareMinX );
                var splitsByX = GetPotentialSplitMargins( _Children, minEntries );
                _Children.Sort( Compares.CompareMinY );
                var splitsByY = GetPotentialSplitMargins( _Children, minEntries );

                if ( splitsByX < splitsByY )
                {
                    _Children.Sort( Compares.CompareMinX );
                }
            }
            private static float GetPotentialSplitMargins( IReadOnlyList< ISpatialData > children, int minEntries )
                => GetPotentialEnclosingMargins( children, minEntries ) + GetPotentialEnclosingMargins( children.ToReverseList(), minEntries );
            private static float GetPotentialEnclosingMargins( IReadOnlyList< ISpatialData > children, int minEntries )
            {
                var envelope = Envelope.Empty;
                int i = 0;
                for ( ; i < minEntries; i++ )
                {
                    envelope = envelope.Extend( children[ i ].Envelope );
                }

                var totalMargin = envelope.Margin;
                for ( var len = children.Count - minEntries; i < len; i++ )
                {
                    envelope = envelope.Extend( children[ i ].Envelope );
                    totalMargin += envelope.Margin;
                }

                return (totalMargin);
            }

            public override string ToString() => $"[{_Envelope}], childs: {_Children.Count}" + (IsLeaf ? ", (leaf)" : null);
        }
    }

    internal static partial class Extensions
    {
        public static IReadOnlyList< T > ToReverseList< T >( this IReadOnlyList< T > lst )
        {
            if ( lst == null ) return (lst);

            var array = new T[ lst.Count ];
            for ( int i = 0, len = lst.Count - 1; i <= len; i++ )
            {
                array[ len - i ] = lst[ i ];
            }
            return (array);
        }
    }
}
