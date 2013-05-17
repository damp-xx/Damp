using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DampGUI;
using SimpleMvvmToolkit;

namespace DampGUI
{
    public class Friends : ModelBase<Friends>, IFriends
    {
        private int currentFriendIndex = -1;
        private int totalFriends = 0;
        private ObservableCollection<IFriend> friends = new ObservableCollection<IFriend>();


        public void Add(IFriend aFriend)
        {
            totalFriends++;
            friends.Add(aFriend);
        }

        public IFriend Get(int i)
        {
            return friends[i];
        }

        public IFriend CurrentFriend
        {
            get
            {
                if (CurrentFriendIndex >= 0)
                {
                    if (friends[currentFriendIndex].Photo.IsMade == false)
                    {
                        friends[currentFriendIndex].Photo.LoadPicture();
                        friends[currentFriendIndex].Photo.Create();
                    }

                    return friends[CurrentFriendIndex];
                }

                return null;
            }
            set
            {
                CurrentFriendIndex = friends.IndexOf(value);
                NotifyPropertyChanged(vm => vm.CurrentFriend);
            }
        }

        public int CurrentFriendIndex
        {
            get { return currentFriendIndex; }
            set
            {
                if (currentFriendIndex != value)
                {
                    currentFriendIndex = value;
                    NotifyPropertyChanged(vm => vm.CurrentFriend);
                    NotifyPropertyChanged(vm => vm.CurrentFriendIndex);
                }
            }
        }

        public int TotalFriends
        {
            get { return totalFriends; }
            set
            {
                totalFriends = value;
                NotifyPropertyChanged(vm => vm.TotalFriends);
            }
        }
    }

}
