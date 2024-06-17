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

namespace Bookshelf.Presentation.Views
{
    /// <summary>
    /// Interação lógica para BooksView.xam
    /// </summary>
    public partial class BooksView : UserControl
    {
        public BooksView()
        {
            InitializeComponent();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null) {
                if (e.Delta > 0) {
                    scrollViewer.LineUp();
                } else {
                    scrollViewer.LineDown();
                }
                e.Handled = true;
            }
        }
    }
}
