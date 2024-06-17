using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bookshelf.Presentation.Components {
    /// <summary>
    /// Interação lógica para OverlayBookCard.xam
    /// </summary>
    public partial class OverlayBookCard : UserControl {
        public OverlayBookCard() {
            InitializeComponent();
        }

        private void RadComboBox_GotFocus(object sender, RoutedEventArgs e) {
            var comboBox = sender as Telerik.Windows.Controls.RadComboBox;
            if (comboBox != null) {
                // Use a dispatcher to delay the opening of the dropdown
                Dispatcher.BeginInvoke(new Action(() => {
                    comboBox.IsDropDownOpen = true;
                }), System.Windows.Threading.DispatcherPriority.Input);
            }
        }
    }
}
