using System.Drawing;

using trees.win_forms;

using D = System.ComponentModel.DesignerSerializationVisibilityAttribute;
using X = System.ComponentModel.DesignerSerializationVisibility;

namespace System.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    internal class ComboBoxFigure : ComboBox
    {
        private bool _IsTacitInit;
        public ComboBoxFigure() { }

        private void TacitInit()
        {
            if ( _IsTacitInit ) return;
            _IsTacitInit = true;

            this.DropDownStyle = ComboBoxStyle.DropDownList;

            this.BeginUpdate();
            this.Items.Clear();
            this.Items.Add( SearchByMethodEnum.Rect );
            this.Items.Add( SearchByMethodEnum.Circle );
            this.SelectedIndex = 0;
            this.EndUpdate();
        }

        [D(X.Hidden)] public SearchByMethodEnum SelectFigure
        {
            get
            {
                TacitInit();
                return (SearchByMethodEnum) this.SelectedItem;
            }
            set
            {
                TacitInit();
                this.SelectedItem = value;
            }
        }
    }
}
