using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;
using CommunicationLibrary;
using MessageBox = System.Windows.MessageBox;


namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XmlDocument xmlCfg = new XmlDocument();
        private const string ConfPath = "loginConfig.xml";
        private bool _rememberLogin = false;
        private bool _autoLogin = false;
        private const string LoginConfNode = "LoginConfig";
        private const string RememberAccountElement = "RememberAccount";
        private const string AutoLoginElement = "AutoLogin";
        private const string AccountNameElement = "AccountName";
        private string _username = "";

        public EventArgs e = null;
        public delegate void LoggedIn(MainWindow m, EventArgs e);
        public event LoggedIn Login;
        

        public MainWindow()
        {
            if (File.Exists(ConfPath))
            {
                ReadConfXml();
            }

            InitializeComponent();

            if (!_rememberLogin)
            {
                AutoLoginCheck.IsEnabled = false;
                AutoLoginCheck.IsChecked = false;
                RememberLoginCheck.IsChecked = _rememberLogin;
            }
            else
            {
                RememberLoginCheck.IsChecked = _rememberLogin;
                AutoLoginCheck.IsChecked = _autoLogin;
            }
            Username.Text = _username;
            Username.Focus();
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
            if(Username.Text != "")
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
            var logincheck = ComLogin.Login(Username.Text, Password.Password);

            if (logincheck && Login != null)
            {
                Login(this, this.e);
                WriteXml();
            }
            else if(logincheck)
            {
                WriteXml();
            }
            else
            {
                MessageBox.Show("Wrong account name or password", "Critical Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Logo_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void RetrieveButton_OnClick(object sender, RoutedEventArgs e)
        {
            Window w = new RetrieveAccount();
            w.Owner = this;
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            w.Show();
            this.Hide();
        }

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://google.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CreateConfXml()
        {
            XmlNode rootNode = xmlCfg.CreateElement("XML");
            xmlCfg.AppendChild(rootNode);

            XmlNode configNode = xmlCfg.CreateElement(LoginConfNode);
            var configElement = xmlCfg.CreateElement(RememberAccountElement);
            configElement.InnerText = RememberLoginCheck.IsChecked.Value.ToString();

            configNode.AppendChild(configElement);
            configElement = xmlCfg.CreateElement(AccountNameElement);
            if (RememberLoginCheck.IsChecked.Value == true)
                configElement.InnerText = Username.Text;
            else
                configElement.InnerText = "";

            configNode.AppendChild(configElement);

            configElement = xmlCfg.CreateElement(AutoLoginElement);
            configElement.InnerText = AutoLoginCheck.IsChecked.Value.ToString();
            configNode.AppendChild(configElement);
            rootNode.AppendChild(configNode);

            xmlCfg.Save(ConfPath);
        }

        private void ReadConfXml()
        {
            try
            {
                xmlCfg.Load(ConfPath);
                XmlElement root = xmlCfg.DocumentElement;

                if (root != null)
                {
                    bool.TryParse(root.GetElementsByTagName(RememberAccountElement).Item(0).InnerText, out _rememberLogin);
                    bool.TryParse(root.GetElementsByTagName(AutoLoginElement).Item(0).InnerText, out _autoLogin);
                    if (_rememberLogin == true)
                    {
                        _username = root.GetElementsByTagName(AccountNameElement).Item(0).InnerText;
                    }
                }
            }
            //on exception, delete file
            catch (XmlException e)
            {
                File.Delete(ConfPath);
            }
        }

        private void WriteXml()
        {
            if (File.Exists(ConfPath))
            {
                try
                {
                    xmlCfg.Load(ConfPath);
                    XmlElement root = xmlCfg.DocumentElement;

                    if (root != null)
                    {
                        root.GetElementsByTagName(RememberAccountElement).Item(0).InnerText =
                            RememberLoginCheck.IsChecked.Value.ToString();
                        root.GetElementsByTagName(AutoLoginElement).Item(0).InnerText =
                            AutoLoginCheck.IsChecked.Value.ToString();

                        if (RememberLoginCheck.IsChecked.Value == true)
                        {
                            root.GetElementsByTagName(AccountNameElement).Item(0).InnerText = Username.Text;
                        }
                        else
                        {
                            root.GetElementsByTagName(AccountNameElement).Item(0).InnerText = "";
                            //XmlNode node = root.GetElementsByTagName(AccountNameElement).Item(0);
                            //node.ParentNode.RemoveChild(node);
                        }
                    }
                    xmlCfg.Save(ConfPath);
                }
                catch (XmlException e)
                {
                    File.Delete(ConfPath);
                    CreateConfXml();
                }
            }
            else
            {
                CreateConfXml();
            }
        }
    }
}
