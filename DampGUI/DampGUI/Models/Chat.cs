using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMvvmToolkit;

namespace DampGUI.Models
{
    class Chat: ModelBase<Chat>
    {
        private string msg;

        public string Message
        {
            get { return msg; }
            set
            {
                msg = value;
                NotifyPropertyChanged(m => m.Message);
            }
        }
    }
}
