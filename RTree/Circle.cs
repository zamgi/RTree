using System.Drawing;

namespace System.Collections.Generic
{
    /// <summary>
    /// 
    /// </summary>
    public readonly struct Circle : IEquatable< Circle >
    {
        public float Center_X { get; }
        public float Center_Y { get; }
        public float Radius   { get; }

        public override string ToString() => $"x={Center_X}, y={Center_Y}, radius={Radius}";

        //public float Width  => Max_X - Min_X;
        //public float Height => Max_Y - Min_Y;
        //public float Area   => MathF.Max( this.Max_X - this.Min_X, 0 ) * MathF.Max( this.Max_Y - this.Min_Y, 0 );
        //public float Margin => MathF.Max( this.Max_X - this.Min_X, 0 ) + MathF.Max( this.Max_Y - this.Min_Y, 0 );

        public static implicit operator Circle( in (float center_x, float center_y, float radius) t ) => new Circle( t.center_x, t.center_y, t.radius );

        public Circle( float center_x, float center_y, float radius )
        {
            this.Center_X = center_x;
            this.Center_Y = center_y;
            this.Radius   = radius;
        }

        public bool Contains( float x, float y )
        {
            var cathetus_x = Center_X - x;
            var cathetus_y = Center_Y - y;
            var dist = MathF.Sqrt( cathetus_x * cathetus_x + cathetus_y * cathetus_y );
            return (dist < Radius);
        }
        public bool Contains( in (float x, float y) pt ) => Contains( pt.x, pt.y );
        public bool Contains( in Point pt ) => Contains( pt.X, pt.Y );

        public bool Equals( Circle e ) => (this == e);
        public override bool Equals( object obj ) => (obj is Circle e) && (e == this);
        public override int GetHashCode() => HashCode.Combine( Center_X, Center_Y, Radius );

        public static bool operator ==( in Circle left, in Circle right ) => left.Center_X == right.Center_X && left.Center_Y == right.Center_Y && left.Radius == right.Radius;
        public static bool operator !=( in Circle left, in Circle right ) => !(left == right);

        public static Circle Empty { get; } = new Circle( center_x: float.PositiveInfinity, center_y: float.PositiveInfinity, radius: 0 );
    }

    /// <summary>
    /// 
    /// </summary>
    public static class Circle_Extensions
    {
        public static bool Intersects( in this Envelope env, in Circle circle ) => env.Intersects( circle, out var _ );
        public static bool Intersects( in this Envelope env, in Circle circle, out float dist )
        {
            var cathetus_x = env.Center_X - circle.Center_X;
            var cathetus_y = env.Center_Y - circle.Center_Y;
            dist = MathF.Sqrt( cathetus_x * cathetus_x + cathetus_y * cathetus_y );

            var osculation_dict = circle.Radius + env.GetCoverCircleRadius();
            if ( dist < osculation_dict )
            {
                //circle.center in rect
                var suc = env.Contains( circle.Center_X, circle.Center_Y ); if ( suc ) return (true);

                //circle.top in rect
                suc = env.ContainsInto( circle.Top() ); if ( suc ) return (true);
                suc = env.ContainsInto( circle.Bottom() ); if ( suc ) return (true);
                suc = env.ContainsInto( circle.Left() ); if ( suc ) return (true);
                suc = env.ContainsInto( circle.Right() ); if ( suc ) return (true);

                //rect.top-left in circle
                suc = circle.Contains( env.TopLeft() ); if ( suc ) return (true);
                suc = circle.Contains( env.TopRight() ); if ( suc ) return (true);
                suc = circle.Contains( env.BottomLeft() ); if ( suc ) return (true);
                suc = circle.Contains( env.BottomRight() ); if ( suc ) return (true);

                //rect.center in circle
                suc = circle.Contains( env.Center_X, env.Center_Y ); if ( suc ) return (true);

                //intercect long-horizontal rect 
                if ( (env.Min_X < circle.Center_X) && (circle.Center_X < env.Max_X) )
                {
                    suc = ((circle.Center_Y < env.Min_Y) && (env.Min_Y < circle.Center_Y + circle.Radius)) || //above circle-center
                          ((env.Max_Y < circle.Center_Y) && (circle.Center_Y - circle.Radius < env.Max_Y)); //beyond circle-center
#if DEBUG
                    if ( suc ) return (true);
#endif
                }
                //intercect long-vertical rect 
                else if ( (env.Min_Y < circle.Center_Y) && (circle.Center_Y < env.Max_Y) )
                {
                    suc = ((circle.Center_X < env.Min_X) && (env.Min_X < circle.Center_X + circle.Radius)) || //rigth circle-center
                          ((env.Max_X < circle.Center_X) && (circle.Center_X - circle.Radius < env.Max_X)); //left circle-center
#if DEBUG
                    if ( suc ) return (true);
#endif
                }
                return (suc);
            }
            return (false);
        }
        
        public static Circle ToCircle_Outscribed( in this Envelope env ) => new Circle( env.Center_X, env.Center_Y, env.GetCoverCircleRadius() );

        //public static Envelope ToEnvelope_Outscribed( in this Circle circle ) => new Envelope
        //(
        //    circle.Left(),
        //    circle.Top(),
        //    circle.Right(),
        //    circle.Bottom()
        //);
        //public static Envelope ToEnvelope_Inscribed( in this Circle circle ) => new Envelope(...);


        //[Y=0:X=0] => left:bottom of screen (Y=left:bottom-left:top, X=left:bottom-right:bottom
        /*
          (Y)
           ^
           |
           |
           |
        (0)+-------->(X)
         */
        public static (float x, float y) Top( in this Circle circle ) => (circle.Center_X, circle.Center_Y + circle.Radius);
        public static (float x, float y) Bottom( in this Circle circle ) => (circle.Center_X, circle.Center_Y - circle.Radius);
        public static (float x, float y) Left( in this Circle circle ) => (circle.Center_X - circle.Radius, circle.Center_Y);
        public static (float x, float y) Right( in this Circle circle ) => (circle.Center_X + circle.Radius, circle.Center_Y);

        public static (float x, float y) TopLeft( in this Envelope env ) => (env.Min_X, env.Max_Y);
        public static (float x, float y) TopRight( in this Envelope env ) => (env.Max_X, env.Max_Y);
        public static (float x, float y) BottomLeft( in this Envelope env ) => (env.Min_X, env.Min_Y);
        public static (float x, float y) BottomRight( in this Envelope env ) => (env.Max_X, env.Min_Y);
    }
}
