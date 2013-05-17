using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using CommunicationLibrary;

namespace DampGUI
{
    internal class EventSubscriber : IEventSubscribe
    {
        public static Dictionary<string, ChatView> ChatIdentyfier = new Dictionary<string, ChatView>();

        public void HandleNewChatMessage(string from, string message, string fromTitle)
        {
            Console.WriteLine("HandleNewChatMessage");
            Console.WriteLine(from + message);
            if (ChatIdentyfier.ContainsKey(from))
            {
                try
                {
                    Console.WriteLine("from known");
                    var d = ChatIdentyfier[from];

                    d.Dispatcher.BeginInvoke(new Action(delegate() { d.SendData(fromTitle + ": " + message); }));
                }
                catch (InvalidCastException)
                {
                    Console.WriteLine("blablabla" + from);
                }
            }
            else
            {
                Console.WriteLine("from unknown");
                MainWindow.dd.dd.BeginInvoke(new Action(delegate()
                    {
                        var d = new ChatView(from);
                        d.Show();
                        d.SendData(fromTitle + ": " + message);
                        ChatIdentyfier.Add(from, d);
                    }));
            }
        }

        public void HandleUserOnline(XmlElement Event)
        {
            MainPageViewModel.k.dd.BeginInvoke(new Action(delegate()
                {
                    var userid = Event.GetElementsByTagName("Message").Item(0).InnerText;

                    MainPageViewModel.k.dd.BeginInvoke(new Action(delegate()
                    {
                        foreach (var friend in MainPageViewModel.k.friends.Where(friend => friend.Id.Equals(userid)))
                        {
                            friend.Name = "(Online) " + friend.RealName;
                            break;
                        }
                    }));

                 
                }));
        }

        public void HandleUserOffline(XmlElement Event)
        {
            MainPageViewModel.k.dd.BeginInvoke(new Action(delegate()
            {
                var userid = Event.GetElementsByTagName("Message").Item(0).InnerText;

                MainPageViewModel.k.dd.BeginInvoke(new Action(delegate()
                {
                    foreach (var friend in MainPageViewModel.k.friends.Where(friend => friend.Id.Equals(userid)))
                    {
                        friend.Name = friend.RealName;
                        break;
                    }
                }));


            }));
        }
        public void HandleFriendRequest(XmlElement Event)
        {
            MainPageViewModel.k.dd.BeginInvoke(new Action(delegate()
            {
                var userid = Event.GetElementsByTagName("From").Item(0).InnerText;

                MainPageViewModel.k.dd.BeginInvoke(new Action(delegate()
                {
                    Friend friend = ComFriend.GetUser(userid);
                    MainPageViewModel.k.friends.Add(friend);
                }));


            }));
        }
        public void HandleFriendAccepted(XmlElement Event)
        {
            MainPageViewModel.k.dd.BeginInvoke(new Action(delegate()
            {
                var userid = Event.GetElementsByTagName("From").Item(0).InnerText;

                MainPageViewModel.k.dd.BeginInvoke(new Action(delegate()
                    {
                        Friend friend = ComFriend.GetUser(userid);
                        MainPageViewModel.k.friends.Add(friend);
                    }));


            }));
        }

        public static void NewChat(string from, ChatView Instance)
        {
            if (!ChatIdentyfier.ContainsKey(from))
            {
                //Console.WriteLine("Hej from: {0}", from);
                ChatIdentyfier.Add(from, Instance);
            }
        }
    }
}
