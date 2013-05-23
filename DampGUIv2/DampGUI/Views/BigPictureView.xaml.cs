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
        /**
        * BigPictureView
        * 
        * @brief Is the constructor for BigPictureView 
        */
        public BigPictureView()
        {
            InitializeComponent();
        }

        /**
         *  Window_MouseDown_1
         * 
         * @brief  this function closes the window when something is pressed
         * @param object sender, MouseButtonEventArgs e
         */
        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        /**
         *  Window_Activated_1
         * 
         * @brief  this function detects when the mouse is pressed, even if it is not over the window 
         * @param object sender, EventArgs e
         */
        private void Window_Activated_1(object sender, EventArgs e)
        {
            Mouse.Capture(this, CaptureMode.SubTree);
        }
    }
}
