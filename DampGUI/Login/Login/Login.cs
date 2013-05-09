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
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException();
            
            return _loginMethod.Login(username, password);
        }
    }
}
