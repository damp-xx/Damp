using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;
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
        private bool rememberLogin = false;
        private bool autoLogin = false;

        public MainWindow()
        {
            if (!File.Exists(ConfPath))
            {
                CreateXml();
            }
            else
            {
                ReadXml();
            }

            InitializeComponent();

            if (!rememberLogin)
            {
                AutoLoginCheck.IsEnabled = false;
                AutoLoginCheck.IsChecked = false;
                RememberLoginCheck.IsChecked = rememberLogin;
            }
            else
            {
                RememberLoginCheck.IsChecked = rememberLogin;
                AutoLoginCheck.IsChecked = autoLogin;
            }
            
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
            
            throw new NotImplementedException();
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

        private void CreateXml()
        {
            XmlNode rootNode = xmlCfg.CreateElement("XML");
            xmlCfg.AppendChild(rootNode);

            XmlNode configNode = xmlCfg.CreateElement("LoginConfig");
            var configElement = xmlCfg.CreateElement("RememberAccount");
            configElement.InnerText = "false";
            configNode.AppendChild(configElement);
            configElement = xmlCfg.CreateElement("AutoLogin");
            configElement.InnerText = "false";
            configNode.AppendChild(configElement);
            rootNode.AppendChild(configNode);
            xmlCfg.Save(ConfPath);
        }

        private void ReadXml()
        {
            try
            {
                xmlCfg.Load(ConfPath);
                var node = xmlCfg.GetElementsByTagName("LoginConfig").Item(0);
                if (node != null)
                {
                    var element = node.ChildNodes;
                    try
                    {
                        rememberLogin = string.IsNullOrEmpty(element.Item(0).InnerText) ? false : bool.Parse(element.Item(0).InnerText);
                    }
                    catch (FormatException e)
                    {
                        rememberLogin = false;
                    }
                    try
                    {
                        autoLogin = string.IsNullOrEmpty(element.Item(1).InnerText) ? false : bool.Parse(element.Item(1).InnerText);
                    }
                    catch (FormatException e)
                    {
                        autoLogin = false;
                    }
                }
            }
            //on exception, overwrite file using default values
            catch (XmlException e)
            {
                File.Delete(ConfPath);
                CreateXml();
            }
        }
        private void WriteXml(string rememberAccountSet, string autoLoginSet)
        {
            
        }
    }
}
