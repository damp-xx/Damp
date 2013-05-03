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


<<<<<<< HEAD:DampClient/CommunicationLibrary/Program.cs
            var resultOfLogin = ComLogin.Authenticate("Jens", "Jens");
=======
            var resultOfLogin = ComLogin.Login("jens", "jens");
>>>>>>> f7d9e19ea7f2cab0b28c4e4d00b1f69118bc8a59:DampGUI/CommunicationLibrary/Program.cs

            Console.WriteLine("login status is: {0}", resultOfLogin);
            //var resultOfSendChat = ComFriend.SendChatMessage("Test til bardyr");
            //Console.WriteLine("{0}", resultOfSendChat);
            var resultOfGetProfile = ComProfile.GetProfile();
            Console.WriteLine("{0}", resultOfGetProfile);
<<<<<<< HEAD:DampClient/CommunicationLibrary/Program.cs
            var resultOfSearchUser = ComFriend.AddFriend("2");
            Console.WriteLine(resultOfSearchUser);
            Parallel.For(0, 1000, (i) =>
                {
                    ComEvents.Listen();

                });
=======
            //var resultOfSearchUser = ComFriend.AddFriend("2");
            //Console.WriteLine(resultOfSearchUser);
>>>>>>> f7d9e19ea7f2cab0b28c4e4d00b1f69118bc8a59:DampGUI/CommunicationLibrary/Program.cs
            Console.Read();
        }
    }
}