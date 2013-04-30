using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationLibrary
{
    class Program
    {
        static void Main(string[] args)
        {


            var resultOfLogin = ComLogin.Authenticate("bardyr", "mormor");

            Console.WriteLine("login status is: {0}", resultOfLogin);
            //var resultOfSendChat = ComFriend.SendChatMessage("Test til bardyr");
            //Console.WriteLine("{0}", resultOfSendChat);
            var resultOfGetProfile = ComProfile.GetProfile();
            Console.WriteLine("{0}", resultOfGetProfile);
            var resultOfSearchUser = ComFriend.AddFriend("2");
            Console.WriteLine(resultOfSearchUser);
            Console.Read();
        }
    }
}