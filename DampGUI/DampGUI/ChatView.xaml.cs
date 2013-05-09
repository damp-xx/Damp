using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CommunicationLibrary;

namespace DampGUI
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : Window
    {
        public ChatView(string idName)
        {
            InitializeComponent();
            Contact = idName;
            
        }


        public void SendData(string msg)
        {
            ChatData.Add(msg);
            StringBuilder con = new StringBuilder();
            foreach (var chatmsg in ChatData)
            {
                con.AppendLine(chatmsg);
            }
            AllChat = con.ToString();
            ChatMessage = "";

        }

        public string Contact { get; set; }
        private ObservableCollection<string> chatData = new ObservableCollection<string>();

        public ObservableCollection<string> ChatData
        {
            get
            {
                return chatData;
            }
            set
            {
                chatData = value;
            }
        }

        private string chatMessage;

        public string ChatMessage
        {
            get { return chatMessage; }
            set
            {
                chatMessage = value;
          
            }
        }

        private string allChat;

        public string AllChat
        {
            get { return allChat; }
            set
            {
                allChat = value;
       
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComFriend.SendChatMessage(ChatMessage, Contact) == true)
            {
                SendData("Me: \n" + ChatMessage);
            }
        }
    }
}
