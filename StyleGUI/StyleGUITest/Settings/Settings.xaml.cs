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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            
            Topmost = true;
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            SettingsWindow.Close();
        }

        private void Drag_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            e.Handled = false;
        }

        private void ChangeEmail_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeEmail changeEmail = new ChangeEmail();

            changeEmail.Owner = this;
            changeEmail.ShowDialog();
        }

        private void ChangePassword_OnClick(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword();

            changePassword.Owner = this;
            changePassword.ShowDialog();
        
        }
    }
}
