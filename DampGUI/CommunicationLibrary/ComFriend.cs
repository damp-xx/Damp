using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DampCS;
using DampGUI;

namespace CommunicationLibrary
{
    public class ComFriend
    {
        public static Friends GetFriendList()
        {
            var client = new DampServerClient(ComLogin._ComIp);
            //Console.WriteLine("Token before send Forgotten Email: {0}", ComLogin._ComToken);
            var ResultFromServerXml = client.SendRequest("GetMyUser", new Dictionary<string, string> {{"",""}}, ComLogin._ComToken);
            Friends FriendList = new Friends();
            if (ResultFromServerXml.Name.Equals("User"))
            {
                foreach (XmlElement x in ResultFromServerXml.GetElementsByTagName("User"))
                {
                    Friend k = new Friend
                        {
                            Id = x. GetElementsByTagName("UserId").Item(0).InnerText,
                            Name = x.GetElementsByTagName("Username").Item(0).InnerText,
                            Description = x.GetElementsByTagName("Email").Item(0).InnerText,
                            AchivementsComplete = new ObservableCollection<string> {"Test"},
                            City = "Aarhus",
                            Country = "Denmark",
                            Gender = "Male",
                            Language = "Male",
                            Photo = new Photo("")
                        };
                    FriendList.Add(k);
                }
                return FriendList;
            }
            return null;
        }

        public static bool SendChatMessage(string message)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("ChatSend", new Dictionary<string, string>{{"Message", message}}, ComLogin._ComToken);

            if (ResultFromServerXml.Name.Equals("Status"))
            {
                var xmlNode = ResultFromServerXml.GetElementsByTagName("Code").Item(0);

                if (xmlNode.InnerText == "200")
                {
                    return true;
                }
            }
            return false;
        }
        
        public static bool AddFriend(string userID)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("AddFriend", new Dictionary<string, string>{{"Friend", userID}}, ComLogin._ComToken);

                        if (ResultFromServerXml.Name.Equals("Status"))
            {
                var xmlNode = ResultFromServerXml.GetElementsByTagName("Code").Item(0);

                if (xmlNode.InnerText == "200")
                {
                    return true;
                }
            }
            return false;
        }
        
        public static bool RemoveFriend(string userID)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("RemoveFriend", new Dictionary<string, string>{{"Friend", userID}}, ComLogin._ComToken);

            if (ResultFromServerXml.Name.Equals("Status"))
            {
                var xmlNode = ResultFromServerXml.GetElementsByTagName("Code").Item(0);

                if (xmlNode.InnerText == "200")
                {
                    return true;
                }
            }
            return false;
        }

        public static bool AcceptFriend(string FriendID)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("AcceptFriend", new Dictionary<string, string> { { "Friend", FriendID } }, ComLogin._ComToken);

            if (ResultFromServerXml.Name.Equals("Status"))
            {
                var xmlNode = ResultFromServerXml.GetElementsByTagName("Code").Item(0);

                if (xmlNode.InnerText == "200")
                {
                    return true;
                }
            }
            return false;
        }

        public static Friends SearchUser(string searchString)
        {
            var client = new DampServerClient(ComLogin._ComIp);
            var ResultFromServerXml = client.SendRequest("FriendSearch", new Dictionary<string, string> { { "Query", searchString } }, ComLogin._ComToken);
            Friends FriendList = new Friends();
            if (ResultFromServerXml.Name.Equals("FriendSearchResponse"))
            {
                
                foreach (XmlElement x in ResultFromServerXml.GetElementsByTagName("User") )
                {
                    Friend k = new Friend
                    {
                        Id = x.GetElementsByTagName("UserId").Item(0).InnerText,
                        Name = x.GetElementsByTagName("Username").Item(0).InnerText,
                        Description = "Email",
                        AchivementsComplete = new ObservableCollection<string> { "Test" },
                        City = "Aarhus",
                        Country = "Denmark",
                        Gender = "Male",
                        Language = "Male",
                        Photo = new Photo("")
                    };
                    FriendList.Add(k);
                }

                return FriendList;
            }
            return null;
        }
    }
}
