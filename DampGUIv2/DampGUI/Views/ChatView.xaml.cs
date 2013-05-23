using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using CommunicationLibrary;

namespace DampGUI
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : Window
    {
        public Dispatcher disp;

        /**
         *  ChatView
         * 
         * @brief This is the constructor for the ChatView, it takes the id of the Friend you wish to chat with, and sets the Dispatcher. 
         * @param string idName
         */
        public ChatView(string idName)
        {
            ChatData = new ObservableCollection<string>();
            InitializeComponent();
            Contact = idName;
            disp = ChatText.Dispatcher;
        }

        /**
         *  SendData
         * 
         * @brief  this function sends the data to the users screen, both when you receive and send
         * @param string msg
         */
        public void SendData(string msg)
        {
            ChatData.Add(msg);

            StringBuilder con = new StringBuilder();
            foreach (var chatmsg in ChatData)
            {
                con.AppendLine(chatmsg);
            }
            ChatText.Text = con.ToString();
            ChatMsg.Text = "";
        }

        /**
         *  Contact
         * 
         * @brief  this is a property
         * @param set(string)
         * @return get(string)
         */
        public string Contact { get; set; }

        /**
        *  ChatData
        * 
        * @brief  this is a property
        * @param set(ObservableCollection<string>)
        * @return get(ObservableCollection<string>)
        */
        public ObservableCollection<string> ChatData { get; set; }

        /**
         *  ButtonBase_OnClick
         * 
         * @brief  This is a GUI event, that triggers when the button send is pressed and sends the message to the ComLib
         * @param object sender, RoutedEventArgs e
         */
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComFriend.SendChatMessage(ChatMsg.Text, Contact) == true)
            {
            }

            SendData("Me: " + ChatMsg.Text);
        }
    }
}
