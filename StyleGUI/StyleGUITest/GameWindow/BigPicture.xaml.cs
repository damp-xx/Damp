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
    /// Interaction logic for BigPicture.xaml
    /// </summary>
    public partial class BigPicture : Window
    {
        public BigPicture()
        {
            InitializeComponent();   
          
       }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Activated_1(object sender, EventArgs e)
        {
            System.Windows.Input.Mouse.Capture(this, System.Windows.Input.CaptureMode.SubTree);
        }


    }
}
