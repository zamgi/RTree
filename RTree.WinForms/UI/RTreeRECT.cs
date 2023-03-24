using System.Collections.Generic;

namespace trees.win_forms
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class RTreeRECT : ISpatialData
    {
        private Envelope _Envelope;
        public RTreeRECT( in (float x, float y, string s) t )
        {
            _Envelope = new Envelope( t.x, t.y, t.x + 1, t.y + 1 );
            Text      = t.s;
        }
        public RTreeRECT( in (float x, float y, string s) t, float w, float h )
        {
            _Envelope = new Envelope( t.x, t.y, t.x + w, t.y + h );
            Text = t.s;
        }
        public RTreeRECT( in (float x, float y, float w, float h, string s) t )
        {
            _Envelope = new Envelope( t.x, t.y, t.x + t.w, t.y + t.h );
            Text      = t.s;
        }
        public RTreeRECT( float x, float y, float w, float h, string s )
        {
            _Envelope = new Envelope( x, y, x + w, y + h );
            Text      = s;
        }
        public ref readonly Envelope Envelope => ref _Envelope;
        public string Text { get; }
        public override string ToString() => $"{Text},  (x={_Envelope.Min_X}, y={_Envelope.Min_Y})"; //_Envelope.ToString();
    }
}
