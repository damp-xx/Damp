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

        /**
          *  Add
          * 
          * @brief this function Add the friend to the ObservableCollection
          * @param IFriend aFriend
          */
        public void Add(IFriend aFriend)
        {
            totalFriends++;
            friends.Add(aFriend);
        }

        /**
          *  Get
          * 
          * @brief this function get the friend at the selected index(i)
          * @param int i
          * @return IFriend
          */
        public IFriend Get(int i)
        {
            return friends[i];
        }

        /**
        *  CurrentFriend
        * 
        * @brief this is a property that gets the current friend
        * @param set(IFriend)
        * @return get(IFriend)
        */
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

        /**
          *  CurrentFriendIndex
          * 
          * @brief this is a property gets the current friends index
          * @param set(int)
          * @return get(int)
          */
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

        /**
          *  TotalFriends
          * 
          * @brief this is a property that gets the total number of friends
          * @param set(int)
          * @return get(int)
          */
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
