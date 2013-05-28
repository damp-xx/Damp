using System.IO;
using System.Xml;

namespace DampGUI.Login
{
    class XmlConfig
    {
        private XmlDocument XmlConfigFile = new XmlDocument();

        private XmlNode _rootNode;
        private XmlNode _configNode;
        private XmlNode _accountnameElement;
        private XmlNode _rememberAccountElement;
        private XmlNode _autologinElement;

        private string _filepath;

        public string ConfigNodeString { get; set; }
        public string AccountnameElementString { get; set; }
        public string RememberAccountElementString { get; set; }
        public string AutologinElementString { get; set; }

        public string Accountname
        {
            get { return _accountnameElement.InnerText; } 
            set { _accountnameElement.InnerText = value; }
        }
        public bool RememberAccountIsChecked
        {
            get { return bool.Parse(_rememberAccountElement.InnerText); } 
            set { _rememberAccountElement.InnerText = value.ToString(); }
        }
        public bool AutologinIsChecked
        {
            get { return bool.Parse(_autologinElement.InnerText); }
            set { _autologinElement.InnerText = value.ToString(); }
        }


        public XmlConfig(string filepath)
        {
            _filepath = filepath;
        }

        public bool ReadConfig()
        {
            bool rv;
            if (File.Exists(_filepath))
            {
                rv = ReadConfXml();
                
            }
            else
            {
                CreateConfXml();
                rv = false;
            }

            XmlConfigFile.AppendChild(_rootNode);
            _rootNode.AppendChild(_configNode);
            _configNode.AppendChild(_rememberAccountElement);
            _configNode.AppendChild(_accountnameElement);
            _configNode.AppendChild(_autologinElement);

            return rv;
        }

        private bool ReadConfXml()
        {
            try
            {
                XmlConfigFile.Load(_filepath);
                _rootNode = XmlConfigFile.GetElementsByTagName("XML").Item(0);
                _configNode = XmlConfigFile.GetElementsByTagName(ConfigNodeString).Item(0);
                _accountnameElement = XmlConfigFile.GetElementsByTagName(AccountnameElementString).Item(0);
                _autologinElement = XmlConfigFile.GetElementsByTagName(AutologinElementString).Item(0);
                _rememberAccountElement = XmlConfigFile.GetElementsByTagName(RememberAccountElementString).Item(0);
                return true;
            }
            catch (XmlException)
            {
                File.Delete(_filepath);
                CreateConfXml();
                return false;
            }
        }
        private void CreateConfXml()
        {
            _rootNode = XmlConfigFile.CreateElement("XML");
            _configNode = XmlConfigFile.CreateElement(ConfigNodeString);
            _accountnameElement = XmlConfigFile.CreateElement(AccountnameElementString);
            _rememberAccountElement = XmlConfigFile.CreateElement(RememberAccountElementString);
            _autologinElement = XmlConfigFile.CreateElement(AutologinElementString);
        }

        public void SaveConfFile()
        {
            XmlConfigFile.Save(_filepath);
        }
    }
}
