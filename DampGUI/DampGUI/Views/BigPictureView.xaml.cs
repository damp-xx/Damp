using System;
using System.Windows;
using System.Windows.Input;

namespace DampGUI
{
    /// <summary>
    /// Interaction logic for BigPictureView.xaml
    /// </summary>
    public partial class BigPictureView : Window
    {
        public BigPictureView()
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
