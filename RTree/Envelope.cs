namespace System.Collections.Generic
{
	/// <summary>
	/// 
	/// </summary>
	public readonly struct Envelope : IEquatable< Envelope >
	{
		public float Min_X { get; }
		public float Min_Y { get; }
		public float Max_X { get; }
		public float Max_Y { get; }

		public override string ToString() => $"x={Min_X}, y={Min_Y}, max_x={Max_X}, max_y={Max_Y}, (w={Max_X - Min_X}, h={Max_Y - Min_Y})";

        public float Width    => Max_X - Min_X;
        public float Height   => Max_Y - Min_Y;
        public float Center_X => Width / 2 + Min_X;
        public float Center_Y => Height / 2 + Min_Y;
        //public (float x, float y) Center => (Center_X, Center_Y);
        public float Area     => MathF.Max( this.Max_X - this.Min_X, 0 ) * MathF.Max( this.Max_Y - this.Min_Y, 0 );
		public float Margin   => MathF.Max( this.Max_X - this.Min_X, 0 ) + MathF.Max( this.Max_Y - this.Min_Y, 0 );
		public float GetDiagonalLength() => MathF.Sqrt( Height * Height + Width * Width );
        public float GetCoverCircleRadius() => MathF.Sqrt( Height * Height + Width * Width ) / 2;


        public static implicit operator Envelope( in (float min_x, float min_y, float max_x, float max_y) t ) => new Envelope( t.min_x, t.min_y, t.max_x, t.max_y );

		public Envelope( float min_x, float min_y, float max_x, float max_y )
		{
			this.Min_X = min_x;
			this.Min_Y = min_y;
			this.Max_X = max_x;
			this.Max_Y = max_y;
		}
        //public Envelope Clone() => new Envelope( this.Min_X, this.Min_Y, this.Max_X, this.Max_Y );

        public Envelope Extend( in Envelope e ) => new Envelope
		(
			min_x: MathF.Min( this.Min_X, e.Min_X ),
			min_y: MathF.Min( this.Min_Y, e.Min_Y ),
			max_x: MathF.Max( this.Max_X, e.Max_X ),
			max_y: MathF.Max( this.Max_Y, e.Max_Y ) 
		);
		public Envelope Intersection( in Envelope e ) => new Envelope
		(
			min_x: MathF.Max( this.Min_X, e.Min_X ),
			min_y: MathF.Max( this.Min_Y, e.Min_Y ),
			max_x: MathF.Min( this.Max_X, e.Max_X ),
			max_y: MathF.Min( this.Max_Y, e.Max_Y )
		);

		public bool Contains( in Envelope e ) => this.Min_X <= e.Min_X && e.Max_X <= this.Max_X &&
												 this.Min_Y <= e.Min_Y && e.Max_Y <= this.Max_Y;
        public bool Contains( float x, float y ) => this.Min_X <= x && x <= this.Max_X &&
													this.Min_Y <= y && y <= this.Max_Y;
        public bool Contains( in (float x, float y) pt ) => this.Min_X <= pt.x && pt.x <= this.Max_X &&
                                                            this.Min_Y <= pt.y && pt.y <= this.Max_Y;

        public bool ContainsInto( float x, float y ) => this.Min_X < x && x < this.Max_X &&
														this.Min_Y < y && y < this.Max_Y;
        public bool ContainsInto( in (float x, float y) pt ) => this.Min_X < pt.x && pt.x < this.Max_X &&
																this.Min_Y < pt.y && pt.y < this.Max_Y;

        public bool Intersects( in Envelope e ) => this.Min_X <= e.Max_X && e.Min_X <= this.Max_X && 
												   this.Min_Y <= e.Max_Y && e.Min_Y <= this.Max_Y;

		public bool Equals( Envelope e ) => (this == e);
		public override bool Equals( object obj ) => (obj is Envelope e) && (e == this);
		public override int GetHashCode() => HashCode.Combine( Min_X, Min_Y, Max_X, Max_Y );

        public static bool operator ==( in Envelope left, in Envelope right ) => left.Min_X == right.Min_X && left.Max_X == right.Max_X &&
																				 left.Min_Y == right.Min_Y && left.Max_Y == right.Max_Y;
		public static bool operator !=( in Envelope left, in Envelope right ) => !(left == right);
		
		public static Envelope Empty { get; } = new Envelope( min_x: float.PositiveInfinity, min_y: float.PositiveInfinity, max_x: float.NegativeInfinity, max_y: float.NegativeInfinity );
        //public static Envelope Infinite { get; } = new Envelope( min_x: float.NegativeInfinity, min_y: float.NegativeInfinity, max_x: float.PositiveInfinity, max_y: float.PositiveInfinity );
    }
}
