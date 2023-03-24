using System.Collections.Immutable;
using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RTree< T >
	{
		#region [.Search.]
		private List< ImmutableStack< ISpatialData > > DoPathSearch( in Envelope boundingBox )
		{
			if ( !Root.Envelope.Intersects( boundingBox ) )
			{
				return (new List< ImmutableStack< ISpatialData > >());
			}

			var intersections = new List< ImmutableStack< ISpatialData > >();
			var queue = new Queue< ImmutableStack< ISpatialData > >();
			queue.Enqueue( ImmutableStack< ISpatialData >.Empty.Push( Root ) );

			do
			{
				var current = queue.Dequeue();
				foreach ( var c in ((Node) current.Peek()).Children )
				{
					if ( c.Envelope.Intersects( boundingBox ) )
					{
						if ( c is T )
							intersections.Add( current.Push( c ) );
						else
							queue.Enqueue( current.Push( c ) );
					}
				}
			}
			while ( queue.Count != 0 );

			return (intersections);
		}

        /// <summary>
        /// 
        /// </summary>
        private sealed class DoSearch_Comparer : IComparer< (T t, float dist) >
        {
            public static DoSearch_Comparer Inst { get; } = new DoSearch_Comparer();
            private DoSearch_Comparer() { }
            public int Compare( (T t, float dist) x, (T t, float dist) y ) => x.dist.CompareTo( y.dist );
        }

        private static List< (T t, float dist) > PostProcesResult( List< (T t, float dist) > intersections, int topN )
        {
            intersections.Sort( DoSearch_Comparer.Inst );
            if ( topN < intersections.Count )
            {
                intersections.RemoveRange( topN, intersections.Count - topN );
            }
			return (intersections);
        }
        private List< (T t, float dist) > DoSearch_By_Rect( in Envelope boundingBox, int topN )
		{
			if ( !Root.Envelope.Intersects( boundingBox ) )
			{
				return (new List< (T t, float dist) >());
			}

			var intersections = new List< (T t, float dist) >( _Count );
			var queue = new Queue< Node >();
			queue.Enqueue( Root );

			while ( queue.Count != 0 )
			{
				var node = queue.Dequeue();
				if ( node.IsLeaf )
				{
					foreach ( T t_child in node.Children.Cast< T >() )
					{
						if ( t_child.Envelope.Intersects( boundingBox ) )
						{
                            ref readonly var env = ref t_child.Envelope;
                            var dist = MathF.Sqrt( MathF.Pow( (env.Center_X - boundingBox.Center_X), 2 ) + MathF.Pow( (env.Center_Y - boundingBox.Center_Y), 2 ) );

                            intersections.Add( (t_child, dist) );
						}
					}
				}
				else
				{
					foreach ( var child in node.Children.Cast< Node >() )
					{
						if ( child.Envelope.Intersects( boundingBox ) )
						{
							queue.Enqueue( child );
						}
					}
				}
			}

            PostProcesResult( intersections, topN );
            return (intersections);
		}
		/*private List< (T t, float dist) > DoSearch( float x, float y, int topN )
		{
			var intersections = new List< (T t, float dist) >( _Count );

			void add_from_root_level()
			{
				foreach ( var child in Root.Children.Cast< Node >() )
				{
					foreach ( var t_child in child.Children.Cast< T >() )
					{
						ref readonly var env = ref t_child.Envelope;
						var dist = MathF.Sqrt( MathF.Pow( (env.Center_X - x), 2 ) + MathF.Pow( (env.Center_Y - y), 2 ) );
						intersections.Add( (t_child, dist) );
					}
				}
			};

			if ( !Root.Envelope.Contains( x, y ) )
			{
				add_from_root_level();
                PostProcesResult( intersections, topN );
				return (intersections);
                //---return (new List< (T t, float dist) >());
            }


			var queue = new Queue< Node >();
			queue.Enqueue( Root );

			while ( queue.Count != 0 )
			{
				var node = queue.Dequeue();
				if ( node.IsLeaf )
				{
                    //foreach ( var t_child in node._Children.Cast< T >() )
                    //{
                    //	if ( t_child.Envelope.Contains( x, y ) )
                    //	{
                    //		intersections.Add( t_child );
                    //	}
                    //}
                    foreach ( var t_child in node.Children.Cast< T >() )
					{
						ref readonly var env = ref t_child.Envelope;
                        var dist = MathF.Sqrt( MathF.Pow( (env.Center_X - x), 2 ) + MathF.Pow( (env.Center_Y - y), 2 ) );
                        intersections.Add( (t_child, dist) );
                    }
                }
				else
				{
					foreach ( var child in node.Children.Cast< Node >() )
					{
						if ( child.Envelope.Contains( x, y ) )
						{
							queue.Enqueue( child );
                        }
					}
				}
			}

			if ( intersections.Count == 0 )
			{
                add_from_root_level();
            }
            PostProcesResult( intersections, topN );
            return (intersections);
		}
		//*/
		private List< (T t, float dist) > DoSearch_By_Circle( in Circle circle, int topN )
		{
			if ( !Root.Envelope.Intersects( circle ) )
			{
				return (new List< (T t, float dist) >());
			}

			var intersections = new List< (T t, float dist) >( _Count );
			var queue = new Queue< Node >();
			queue.Enqueue( Root );

			while ( queue.Count != 0 )
			{
				var node = queue.Dequeue();
				if ( node.IsLeaf )
				{
					foreach ( T t_child in node.Children.Cast< T >() )
					{
						if ( t_child.Envelope.Intersects( circle, out var dist ) )
						{
                            intersections.Add( (t_child, dist) );
						}
					}
				}
				else
				{
					foreach ( var child in node.Children.Cast< Node >() )
					{
						if ( child.Envelope.Intersects( circle ) )
						{
							queue.Enqueue( child );
						}
					}
				}
			}

            PostProcesResult( intersections, topN );
            return (intersections);
		}
		#endregion

		#region [.Insert.]
		private List< Node > FindCoveringArea__PREV( in Envelope area, int depth )
		{
			var path = new List< Node >();
			var node = this.Root;
			var _area = area; //FIX CS1628

            for (; ; )
            {
				path.Add( node );
				if ( node.IsLeaf || (path.Count == depth) )
				{
					return (path);
				}

				node = node.Children
					.Select( child => (ExtendedArea: child.Envelope.Extend( _area ).Area, child.Envelope.Area, Node: child as Node) )
					.OrderBy( x => x.ExtendedArea )
					.ThenBy( x => x.Area )
					.Select( x => x.Node )
					.First();
			}
		}
		private void Insert__PREV( ISpatialData data, int depth )
		{
			var path = FindCoveringArea__PREV( data.Envelope, depth );

			var insertNode = path.Last();
			insertNode.Add( data );

			while ( --depth >= 0 )
			{
				var node = path[ depth ];
                if ( _MaxEntries < node.Children.Count )
				{
					var newNode = SplitNode( node );
					if ( depth == 0 )
						SplitRoot( newNode );
					else
						path[ depth - 1 ].Add( newNode );
				}
				else
				{
                    node.ResetEnvelope();
				}
			}
		}


        private List< Node > _Path = new List< Node >();
		private void FindCoveringArea__v0( in Envelope area, int depth )
		{
			var _area = area;
            for ( var node = this.Root; ; )
            {
                _Path.Add( node );
				if ( node.IsLeaf || (_Path.Count == depth) )
				{
					return;
				}

				node = node.Children
					.Select( child => (ExtendedArea: child.Envelope.Extend( _area ).Area, child.Envelope.Area, Node: child as Node) )
					.OrderBy( x => x.ExtendedArea )
					.ThenBy( x => x.Area )
					.Select( x => x.Node )
					.First();
            }
		}
		private void FindCoveringArea( in Envelope area, int depth )
		{
            for ( var node = this.Root; ; )
            {
                _Path.Add( node );
				if ( node.IsLeaf || (_Path.Count == depth) )
				{
					return;
				}


                var child = node.Children[ 0 ];
				var f_extendedArea = child.Envelope.Extend( area ).Area;
				var f_area         = child.Envelope.Area;
                for ( int i = node.Children.Count - 1; 1 <= i; i-- )
				{
                    var n_child = node.Children[ i ];
					var n_extendedArea = n_child.Envelope.Extend( area ).Area;
                    if ( n_extendedArea < f_extendedArea )
					{
						f_extendedArea = n_extendedArea;
                        f_area         = n_child.Envelope.Area;
						child          = n_child;
                    }
					else if ( n_extendedArea == f_extendedArea )
					{
                        var n_area = n_child.Envelope.Area;
                        if ( n_area < f_area )
                        {
							f_area = n_area;
                            child  = n_child;
                        }
                    }
                }
				node = (Node) child;

                #region comm. prev.
                /*
				node = node.Children
					.Select( child => (ExtendedArea: child.Envelope.Extend( _area ).Area, child.Envelope.Area, Node: child as Node) )
					.OrderBy( x => x.ExtendedArea )
					.ThenBy( x => x.Area )
					.Select( x => x.Node )
					.First();
				*/
                #endregion
            }
        }
		private void Insert( ISpatialData data, int depth )
		{
			FindCoveringArea( data.Envelope, depth );

			var insertNode = _Path[ _Path.Count - 1 ];
			insertNode.Add( data );

			while ( 0 <= --depth )
			{
				var node = _Path[ depth ];
                if ( _MaxEntries < node.Children.Count )
				{
					var newNode = SplitNode( node );
					if ( depth == 0 )
						SplitRoot( newNode );
					else
                        _Path[ depth - 1 ].Add( newNode );
				}
				else
				{
                    node.ResetEnvelope();
				}
			}

			_Path.Clear();
        }


		#region [.Split Node.]
		private void SplitRoot( Node newNode ) => this.Root = new Node( new List< ISpatialData > { this.Root, newNode }, this.Root.Height + 1 );
		private Node SplitNode( Node node )
		{
            node.SortChildren( _MinEntries ); //---SortChildren( node );

            var splitPoint = GetBestSplitIndex( node.Children, _MinEntries );
            var cnt = node.Children.Count - splitPoint;
            var newChildren = node.Children.Skip( splitPoint ).ToList( cnt );
			node.RemoveRange( splitPoint, cnt );
			return (new Node( newChildren, node.Height ));
		}

		#region comm. move to nodes. [.Sort Children.]
		/*
		private void SortChildren( Node node )
		{
			node._Children.Sort( CompareMinX );
			var splitsByX = GetPotentialSplitMargins( node.Children );
			node._Children.Sort( CompareMinY );
			var splitsByY = GetPotentialSplitMargins( node.Children );

			if ( splitsByX < splitsByY )
			{
				node._Children.Sort( CompareMinX );
			}
		}

		private float GetPotentialSplitMargins( IReadOnlyList< ISpatialData > children ) => GetPotentialEnclosingMargins( children ) + GetPotentialEnclosingMargins( children.AsEnumerable().Reverse().ToList() );
		private float GetPotentialEnclosingMargins( IReadOnlyList< ISpatialData > children )
		{
			var envelope = Envelope.EmptyBounds;
			int i = 0;
			for ( ; i < _MinEntries; i++ )
			{
				envelope = envelope.Extend( children[ i ].Envelope );
			}

			var totalMargin = envelope.Margin;
			for ( ; i < children.Count - _MinEntries; i++ )
			{
				envelope = envelope.Extend( children[ i ].Envelope );
				totalMargin += envelope.Margin;
			}

			return (totalMargin);
		}
		*/
		#endregion

		private static int GetBestSplitIndex( IReadOnlyList< ISpatialData > children, int minEntries )
		{
            var i     = minEntries;
			var f_idx = i;
            var leftEnv  = GetEnclosingEnvelope_Left ( children, i );
			var rightEnv = GetEnclosingEnvelope_Right( children, i );

			var f_overlap   = leftEnv.Intersection( rightEnv ).Area;
			var f_totalArea = leftEnv.Area + rightEnv.Area;

            i++;
            for ( var len = children.Count; i < len; i++ )
			{
                leftEnv  = GetEnclosingEnvelope_Left ( children, i );
                rightEnv = GetEnclosingEnvelope_Right( children, i );

				var n_overlap = leftEnv.Intersection( rightEnv ).Area;            
				if ( n_overlap < f_overlap )
				{
					f_overlap   = n_overlap;
					f_totalArea = leftEnv.Area + rightEnv.Area;
					f_idx = i;
                }
				else if ( n_overlap == f_overlap )
				{
					var n_totalArea = leftEnv.Area + rightEnv.Area;
					if ( n_totalArea < f_totalArea )
					{
						f_totalArea = n_totalArea;
						f_idx = i;
                    }
				}
			}
			return (f_idx);
		}
		private int GetBestSplitIndex__PREV( IReadOnlyList< ISpatialData > children )
			=> Enumerable.Range( _MinEntries, children.Count - _MinEntries )
				.Select( i =>
				{
					 var leftEnvelope  = GetEnclosingEnvelope( children.Take( i ) );
					 var rightEnvelope = GetEnclosingEnvelope( children.Skip( i ) );

					 var overlap   = leftEnvelope.Intersection( rightEnvelope ).Area;
					 var totalArea = leftEnvelope.Area + rightEnvelope.Area;
					 return (i, overlap, totalArea);
				})
				.OrderBy( x => x.overlap )
				.ThenBy( x => x.totalArea )
				.Select( x => x.i )
				.First();
		#endregion
		#endregion

		#region [.Build Tree.]
		private Node BuildTree( List< ISpatialData > data )
		{
			var treeHeight     = GetHeight( data.Count, _MaxEntries );
			var rootMaxEntries = GetRootMaxEntries( data.Count, _MaxEntries, treeHeight ); // (int) Math.Ceiling( data.Count / Math.Pow( _MaxEntries, treeHeight - 1 ) );
			return (BuildNodes( data, 0, data.Count - 1, treeHeight, rootMaxEntries ));
		}

		private static int GetHeight( int numNodes, int maxEntries ) => (int) Math.Ceiling( Math.Log( numNodes ) / Math.Log( maxEntries ) );
        private static int GetRootMaxEntries( int numNodes, int maxEntries, int treeHeight ) => (int) Math.Ceiling( numNodes / Math.Pow( maxEntries, treeHeight - 1 ) );

        private Node BuildNodes( List< ISpatialData > data, int left, int right, int height, int maxEntries )
		{
			var num = right - left + 1;
			if ( num <= maxEntries )
			{
				var items = (height == 1) ? data.GetRange( left, num ) 
										  : new List< ISpatialData > { BuildNodes( data, left, right, height - 1, _MaxEntries ), };
				return (new Node( items, height ));
			}
			
			data.Sort( left, num, Compares.CompareMinX );

			var nodeSize      = (num + (maxEntries - 1)) / maxEntries;
			var subSortLength = nodeSize * (int) Math.Ceiling( Math.Sqrt( maxEntries ) );

			var children = new List< ISpatialData >( maxEntries );
			for ( int subCounter = left; subCounter <= right; subCounter += subSortLength )
			{
				var subRight = Math.Min( subCounter + subSortLength - 1, right );
				data.Sort( subCounter, subRight - subCounter + 1, Compares.CompareMinY );

				for ( var nodeCounter = subCounter; nodeCounter <= subRight; nodeCounter += nodeSize )
				{
					children.Add( BuildNodes( data, nodeCounter, Math.Min( nodeCounter + nodeSize - 1, subRight ), height - 1, _MaxEntries ) );
				}
			}

			return (new Node( children, height ));
		}
		#endregion

		private static Envelope GetEnclosingEnvelope_Left( IReadOnlyList< ISpatialData > children, int takeCount )
		{
			var env = Envelope.Empty;
			for ( var i = 0; i < takeCount; i++ )
			{
				env = env.Extend( children[ i ].Envelope );
			}
			return (env);
		}
		private static Envelope GetEnclosingEnvelope_Right( IReadOnlyList< ISpatialData > children, int skipCount )
		{
			var env = Envelope.Empty;
			for ( var i = skipCount; i < children.Count; i++ )
			{
				env = env.Extend( children[ i ].Envelope );
			}
			return (env);
		}
		private static Envelope GetEnclosingEnvelope( IEnumerable< ISpatialData > items )
		{
			var env = Envelope.Empty;
			foreach ( var data in items )
			{
				env = env.Extend( data.Envelope );
			}
			return (env);
		}

		private static void FillAllChildren( Node root, List< T > list )
		{
			if ( root.IsLeaf )
			{
				list.AddRange( root.Children.Cast< T >() );
			}
			else
			{
				foreach ( var node in root.Children.Cast< Node >() )
				{
					FillAllChildren( node, list );
				}
			}
		}
		private static void FillAllChildrenNodes( Node root, List< Node > list )
		{
			list.Add( root );

			if ( !root.IsLeaf )
            {
				foreach ( var node in root.Children.Cast< Node >() )
				{
					FillAllChildrenNodes( node, list );
				}
            }
		}
	}

	/// <summary>
	/// 
	/// </summary>
	internal static class RTree_Extensions
	{
        public static List< T > ToList< T >( this IEnumerable< T > seq, int capacity )
		{
			var lst = new List< T >( capacity );
			lst.AddRange( seq );
			return (lst);
		}
    }
}
