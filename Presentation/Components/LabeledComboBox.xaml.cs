using System;
using System.Collections;
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
    /// Interação lógica para LabeledComboBox.xam
    /// </summary>
    public partial class LabeledComboBox : UserControl {
        public LabeledComboBox() {
            InitializeComponent();
        }

        public static readonly DependencyProperty LabelTextProperty =
    DependencyProperty.Register("LabelText", typeof(string), typeof(LabeledComboBox), new PropertyMetadata(string.Empty));

        public string LabelText {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly DependencyProperty ComboBoxItemsProperty =
            DependencyProperty.Register("ComboBoxItems", typeof(IEnumerable), typeof(LabeledComboBox), new PropertyMetadata(null));

        public IEnumerable ComboBoxItems {
            get { return (IEnumerable)GetValue(ComboBoxItemsProperty); }
            set { SetValue(ComboBoxItemsProperty, value); }
        }

        public static readonly DependencyProperty SelectedComboBoxItemProperty =
            DependencyProperty.Register("SelectedComboBoxItem", typeof(object), typeof(LabeledComboBox), new PropertyMetadata(null));

        public object SelectedComboBoxItem {
            get { return GetValue(SelectedComboBoxItemProperty); }
            set { SetValue(SelectedComboBoxItemProperty, value); }
        }
    }
}
