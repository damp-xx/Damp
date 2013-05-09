using SimpleMvvmToolkit;

namespace DampGUI
{
    // TODO: find ud af om klassen skal bruges
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class ChatViewModel : ViewModelBase<ChatViewModel>
    {
   
        //   // private /* IXxxServiceAgent */ serviceAgent;

        //    // Default ctor
        //    public ChatViewModel(string idName)
        //    {
        //        Contact = idName;
        //    }

        //    //public ChatViewModel(/* IXxxServiceAgent */ serviceAgent)
        //    //{
        //    // //   this.serviceAgent = serviceAgent;
        //    //}
  //    public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        //    private ObservableCollection<string> chatData = new ObservableCollection<string>();

        //    public ObservableCollection<string> ChatData
        //    {
        //        get
        //        {
        //            return chatData;
        //        }
        //        set
        //        {
        //            chatData = value;
        //            NotifyPropertyChanged(vm => vm.ChatData);
        //           // NotifyPropertyChanged(vm => vm.AllChat);

        //        }
        //    }

        //    private string chatMessage;

        //    public string ChatMessage
        //    {
        //        get { return chatMessage; }
        //        set
        //        {
        //            chatMessage = value;
        //            NotifyPropertyChanged(vm => vm.ChatMessage);
        //        }
        //    }

        //    private string allChat;

        //    public string AllChat
        //    {
        //        get { return allChat; }
        //        set { allChat = value;
        //            NotifyPropertyChanged(vm => vm.AllChat);
        //        } 
        //    }

        //    // TODO: Add methods that will be called by the view

        //    public void SendMessage()
        //    {

        //        if(ComFriend.SendChatMessage(ChatMessage,Contact)==true)
        //        {
        //            SendData("Me: \n"+ChatMessage);
        //        }
        //    }

        //    public string Contact { get; set; }
        //    // TODO: Optionally add callback methods for async calls to the service agent

        //    public void SendData(string msg)
        //    {
        //        ChatData.Add(msg);
        //        StringBuilder con = new StringBuilder();
        //        foreach (var chatmsg in ChatData)
        //        {
        //            con.AppendLine(chatmsg);
        //        }
        //        AllChat = con.ToString();
        //        ChatMessage = "";

        //    }

        //    // Helper method to notify View of an error
        //    private void NotifyError(string message, Exception error)
        //    {
        //        // Notify view of an error
        //        Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        //    }
    }
}

