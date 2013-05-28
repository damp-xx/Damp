using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using CommunicationLibrary;
using MessageBox = System.Windows.MessageBox;

namespace DampGUI.Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private const string ConfPath = "loginConfig.xml";
        private XmlConfig Config;
        private const string LoginConfNode = "LoginConfig";
        private const string RememberAccountElement = "RememberAccount";
        private const string AutoLoginElement = "AutoLogin";
        private const string AccountNameElement = "AccountName";

        public LoginWindow()
        {
            Config = new XmlConfig(ConfPath)
                {
                    AccountnameElementString = AccountNameElement,
                    AutologinElementString = AutoLoginElement,
                    ConfigNodeString = LoginConfNode,
                    RememberAccountElementString = RememberAccountElement
                };


            InitializeComponent();
            if (!Config.ReadConfig())
            {
                AutoLoginCheck.IsEnabled = false;
                AutoLoginCheck.IsChecked = false;
                RememberLoginCheck.IsChecked = false;
                Username.Focus();
            }
            else
            {
                RememberLoginCheck.IsChecked = Config.RememberAccountIsChecked;
                Username.Text = Config.Accountname;
                AutoLoginCheck.IsChecked = Config.AutologinIsChecked;
                Password.Focus();
            }
        }

        private void RememberCheckOn(object sender, RoutedEventArgs e)
        {
            AutoLoginCheck.IsEnabled = true;
        }

        private void RememberCheckOff(object sender, RoutedEventArgs e)
        {
            AutoLoginCheck.IsEnabled = false;
            AutoLoginCheck.IsChecked = false;
        }

        private void Password_OnTextInput(object sender, RoutedEventArgs routedEventArgs)
        {
            if (Username.Text != "")
                LoginButton.IsEnabled = !Password.Password.Equals("");
        }

        private void Username_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Password.Password != "")
                LoginButton.IsEnabled = !Username.Text.Equals("");
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComLogin.Login(Username.Text, Password.Password))
            {
                var m = new MainWindow();
                Config.Accountname = Username.Text;
                Config.AutologinIsChecked = AutoLoginCheck.IsChecked.Value;
                Config.RememberAccountIsChecked = RememberLoginCheck.IsChecked.Value;
                Config.SaveConfFile();
                m.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Wrong account name or password", "Critical Warning", MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void Logo_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void RetrieveButton_OnClick(object sender, RoutedEventArgs e)
        {
            Window w = new RetrieveAccount();
            w.Owner = this;
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            w.Show();
            Hide();
        }

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://10.20.255.127/create_account.html?");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}