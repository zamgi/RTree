namespace System.Collections.Generic
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISpatialData
	{
		ref readonly Envelope Envelope { get; }
	}

	/// <summary>
	/// 
	/// </summary>
	public interface ISpatialIndex< /*out*/ T >
	{
		IReadOnlyList< T > SearchAll();
		IReadOnlyList< (T t, float dist) > Search_By_Rect( in Envelope boundingBox, int topN );
		IReadOnlyList< (T t, float dist) > Search_By_Circle( in Circle circle, int topN );
	}

	/// <summary>
	/// 
	/// </summary>
	public interface ISpatialDatabase< T > : ISpatialIndex< T >
	{
		void Add( T item );
		void Remove( T item );
		void Clear();

		void BulkLoad( IEnumerable< T > items );
		void BulkLoad( List< ISpatialData > data );
	}

	/// <summary>
	/// 
	/// </summary>
	internal static class ProjectionComparer< T >
	{
		public static ProjectionComparer< T, K > Create< K >( Func< T, K > projection ) => new ProjectionComparer< T, K >( projection );
	}

	/// <summary>
	/// 
	/// </summary>
	internal static class Compares
	{
        public static IComparer< ISpatialData > CompareMinX { get; } = ProjectionComparer< ISpatialData >.Create( d => d.Envelope.Min_X );
        public static IComparer< ISpatialData > CompareMinY { get; } = ProjectionComparer< ISpatialData >.Create( d => d.Envelope.Min_Y );
	}

    /// <summary>
    /// 
    /// </summary>
    internal sealed class ProjectionComparer< T, K > : IComparer< T >
	{
		private readonly Func< T, K >   _ProjectionFunc;
		private readonly IComparer< K > _Comparer;

		public ProjectionComparer( Func< T, K > projectionFunc, IComparer< K > comparer = null )
		{
			_Comparer       = comparer ?? Comparer< K >.Default;
			_ProjectionFunc = projectionFunc;
		}

		public int Compare( T x, T y )
		{
			if ( x == null )
			{
				if ( y == null )
				{
					return (0);
				}
				return (-1);
			}
			else if ( y == null )
			{
				return (1);
			}
			return (_Comparer.Compare( _ProjectionFunc( x ), _ProjectionFunc( y ) ));
        }
	}
}
