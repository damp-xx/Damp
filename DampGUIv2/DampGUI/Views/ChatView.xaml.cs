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

        public ChatView(string idName)
        {
            InitializeComponent();
            Contact = idName;
            disp = ChatText.Dispatcher;
        }


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

        public string Contact { get; set; }
        private ObservableCollection<string> chatData = new ObservableCollection<string>();

        public ObservableCollection<string> ChatData
        {
            get { return chatData; }
            set { chatData = value; }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComFriend.SendChatMessage(ChatMsg.Text, Contact) == true)
            {
            }

            SendData("Me: " + ChatMsg.Text);
        }
    }
}
