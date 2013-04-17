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
using System.Windows.Shapes;

namespace StyleGUITest
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
        }


        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Drag_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            e.Handled = false;
        }

    }
}
