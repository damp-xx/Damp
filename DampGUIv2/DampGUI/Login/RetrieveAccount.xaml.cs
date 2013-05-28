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
        public RetrieveAccount()
        {
            InitializeComponent();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            ReturnToMain();
        }

        private void ReturnToMain()
        {
            Owner.Left = Left;
            Owner.Top = Top;
            Owner.Show();
            _cancelButtonClicked = true;
            Close();
        }

        private void Logo_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void RetrieveText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SubmitButton.IsEnabled = !RetrieveText.Text.Equals("");
        }

        private void RetrieveAccount_OnClosing(object sender, CancelEventArgs e)
        {
            if(_cancelButtonClicked ==false)
                Owner.Close();
        }

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
