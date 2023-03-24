using System.Drawing;

using D = System.ComponentModel.DesignerSerializationVisibilityAttribute;
using X = System.ComponentModel.DesignerSerializationVisibility;

namespace System.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    internal class ComboBoxColor : ComboBox
    {
        private bool _IsTacitInit;
        public ComboBoxColor() { }

        private void TacitInit()
        {
            if ( _IsTacitInit ) return;
            _IsTacitInit = true;

            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DrawMode      = DrawMode.OwnerDrawFixed;

            this.BeginUpdate();
            this.Items.Clear();
            foreach ( var knownColor in Enum.GetValues< KnownColor >() )
            {
                var color = Color.FromKnownColor( knownColor );
                if ( color.IsNamedColor && !color.IsSystemColor && (knownColor != KnownColor.Transparent) )
                {
                    this.Items.Add( knownColor );
                }
            }
            this.EndUpdate();
        }

        protected override void OnDrawItem( DrawItemEventArgs e )
        {
            TacitInit();

            base.OnDrawItem( e );

            KnownColor knownColor;
            if ( e.Index == -1 )
            {
                knownColor = SelectKnownColor;
            }
            else
            {
                knownColor = (KnownColor) this.Items[ e.Index ];
            }

            var color = Color.FromKnownColor( knownColor );
            using var brush = new SolidBrush( color );
            e.Graphics.FillRectangle( brush, e.Bounds );

            using var brush2 = new SolidBrush( Invert( color ) );
            e.Graphics.DrawString( knownColor.ToString(), this.Font, brush2, e.Bounds );
        }
        private static Color Invert( Color color ) => Color.FromArgb( 255 - color.R, 255 - color.G, 255 - color.B );
        protected override void OnSelectedValueChanged( EventArgs e )
        {
            TacitInit();
            base.OnSelectedValueChanged( e );

            SelectKnownColor = (KnownColor) this.SelectedItem;
            this.Invalidate();
        }

        [D(X.Hidden)] public KnownColor SelectKnownColor { get; set; } = KnownColor.Red;
        [D(X.Hidden)] public Color SelectColor
        {
            get => Color.FromKnownColor( SelectKnownColor );
            set
            {
                TacitInit();
                foreach ( var knownColor in Enum.GetValues< KnownColor >() )
                {
                    var color = Color.FromKnownColor( knownColor );
                    if ( color == value )
                    {
                        SelectKnownColor = knownColor;
                        return;
                    }
                }

                throw (new ArgumentException( $"Can't get KnownColor by Color: {value}." ));
            }
        }
    }
}
