using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Login
{
    class LoginClass
    {
        private ILoginMethod _loginMethod;
        public LoginClass(ILoginMethod loginMethod)
        {
            _loginMethod = loginMethod;
        }

        public bool LoginUser(string username, string password)
        {
            if(username == null || password == null)
                throw new ArgumentNullException();
            
            if (_loginMethod.Login(username, password) == true)
            {
                var loginConf = new XmlDocument();
               /* if(File.Exists(confPath))
                    loginConf.Load(confPath);*/

            }
            return false;
        }
    }
}
