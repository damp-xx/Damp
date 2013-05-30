/**
 * @file   	RetrieveAccount.xaml.cs
 * @author 	Pierre-Emil Zachariasen, 11833
 * @date   	April, 2013
 * @brief  	This file implements the Retrieve Account window for the GUI
 * @section	LICENSE GPL 
 */

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunicationLibrary;

namespace DampGUI.Login
{
    /// <summary>
    /// Interaction logic for RetrieveAccount.xaml
    /// </summary>
    public partial class RetrieveAccount : Window
    {
        private bool _cancelButtonClicked;
        /**
        * @brief Constructor for Retrieve Account window
        */
        public RetrieveAccount()
        {
            InitializeComponent();
        }
        /**
        * @brief Evenhandler for Cancel Button clikc, returns to mainwindow
        */
        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            ReturnToMain();
        }

        /**
        * @brief Function to return to mainwindow. 
        */
        private void ReturnToMain()
        {
            Owner.Left = Left;
            Owner.Top = Top;
            Owner.Show();
            _cancelButtonClicked = true;
            Close();
        }
        /**
        * @brief Evenhandler Logo Mouse Down
        */
        private void Logo_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        /**
        * @brief Evenhandler input on Retrieve Account text
        */
        private void RetrieveText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SubmitButton.IsEnabled = !RetrieveText.Text.Equals("");
        }

        /**
        * @brief Evenhandler Closing the window
        */
        private void RetrieveAccount_OnClosing(object sender, CancelEventArgs e)
        {
            if(_cancelButtonClicked ==false)
                Owner.Close();
        }
        /**
        * @brief Evenhandler Submit button On click
        */
        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComLogin.ForogtAccount(RetrieveText.Text))
            {
                MessageBox.Show("Confirmation E-mail send", "", MessageBoxButton.OK, MessageBoxImage.Information);
                ReturnToMain();
            }
            else
            {
                MessageBox.Show("E-mail Address not recognized", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
