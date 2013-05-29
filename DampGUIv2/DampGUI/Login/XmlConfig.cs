/**
 * @file   	XmlConfig.cs
 * @author 	Pierre-Emil Zachariasen, 11833
 * @date   	April, 2013
 * @brief  	This file implements the XmlConfig class, for the the login to store and load configuration
 * @section	LICENSE GPL 
 */

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
        /**@{*/
        /**
         * @Brief strings containing XML node names to store to
         */
        public string ConfigNodeString { get; set; }
        public string AccountnameElementString { get; set; }
        public string RememberAccountElementString { get; set; }
        public string AutologinElementString { get; set; }
        /**@}*/

        /**@{*/
        /**
         * @Brief Accesors to the variables saved in the config file. Returns the value of them as the real type instead of string
         * 
         */
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
        /**@}*/

        /**
         * @Brief Constructor for XmlConfig
         * @Param String filepath - path to the config file relative to the application
         */
        public XmlConfig(string filepath)
        {
            _filepath = filepath;
        }

        /**
         * @Brief Reads the current config if it exist, else it creates a new config file
         * @Return  Bool, true if read from file, false if new config file was created
         */
        public bool ReadConfig()
        {
            bool rv;
            if (File.Exists(_filepath))
            {
                try
                {
                    XmlConfigFile.Load(_filepath);
                    _rootNode = XmlConfigFile.GetElementsByTagName("XML").Item(0);
                    _configNode = XmlConfigFile.GetElementsByTagName(ConfigNodeString).Item(0);
                    _accountnameElement = XmlConfigFile.GetElementsByTagName(AccountnameElementString).Item(0);
                    _autologinElement = XmlConfigFile.GetElementsByTagName(AutologinElementString).Item(0);
                    _rememberAccountElement = XmlConfigFile.GetElementsByTagName(RememberAccountElementString).Item(0);
                    rv =  true;
                }
                catch (XmlException)
                {
                    File.Delete(_filepath);
                    CreateConfXml();
                    rv = false;
                }
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
        /**
         * @Brief Creates the XML for the config from scratch
         */
        private void CreateConfXml()
        {
            _rootNode = XmlConfigFile.CreateElement("XML");
            _configNode = XmlConfigFile.CreateElement(ConfigNodeString);
            _accountnameElement = XmlConfigFile.CreateElement(AccountnameElementString);
            _rememberAccountElement = XmlConfigFile.CreateElement(RememberAccountElementString);
            _autologinElement = XmlConfigFile.CreateElement(AutologinElementString);
        }
        /**
         * @Brief Saves the current xml document to the filepath specified in the constructor
         */
        public void SaveConfFile()
        {
            XmlConfigFile.Save(_filepath);
        }
    }
}
