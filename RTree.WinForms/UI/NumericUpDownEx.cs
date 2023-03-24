using trees.win_forms;

using D = System.ComponentModel.DesignerSerializationVisibilityAttribute;
using X = System.ComponentModel.DesignerSerializationVisibility;

namespace System.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    internal class NumericUpDownAdv : NumericUpDown
    {
        protected override void OnKeyUp( KeyEventArgs e )
        {
            base.OnKeyUp( e );

            //access to 'NumericUpDown.Value' raise event 'NumericUpDown.ValueChanged' 
            if ( this.Value == decimal.Zero )
            {
                base.OnValueChanged( e );
            }
        }

        [D(X.Hidden)] public int ValueAsInt32 
        {  
            get => (int) this.Value; 
            set => this.SetValue( value );
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal sealed class NumericUpDownEx : NumericUpDownAdv
    {
        private const string UP_KEY   = "{UP}";
        private const string DOWN_KEY = "{DOWN}";

        private bool _OnMouseWheel;
        protected override void OnMouseWheel( MouseEventArgs e )
        {
            _OnMouseWheel = true;
            SendKeys.Send( GetKey( e.Delta ) );
        }
        protected override void OnValueChanged( EventArgs e )
        {
            if ( _OnMouseWheel )
            {
                _OnMouseWheel = false;
                var div = (int) this.Increment;
                var res = Math.DivRem( this.ValueAsInt32, div, out var remaind );
                if ( remaind != 0 )
                {
                    this.ValueAsInt32 = res * div;
                }
                return;
            }

            base.OnValueChanged( e );
        }
        private static string GetKey( int delta ) => (delta < 0) ? DOWN_KEY : UP_KEY;
    }
}
