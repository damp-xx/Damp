using System.Collections.ObjectModel;
using SimpleMvvmToolkit;

namespace DampGUI
{
    public class Friends : ModelBase<Friends>
    {
        private int currentFriendIndex = -1;
        private int totalFriends = 0;
        private ObservableCollection<Friend> friends = new ObservableCollection<Friend>();


        public void Add(Friend aFriend)
        {
            totalFriends++;
            friends.Add(aFriend);
        }

        public Friend Get(int i)
        {
            return friends[i];
        }

        public Friend CurrentFriend
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


    public class Friend : ModelBase<Friend>
    {
        private string name;
        private string description;
        private string gender;
        private string city;
        private string language;
        private string country;
        private ObservableCollection<string> achivementsComplete;
        private Photo photo;
        private string id;

        public string RealName { get; set; }

        public Photo Photo
        {
            get { return photo; }
            set
            {
                photo = value;
                NotifyPropertyChanged(m => m.Photo);
            }
        }


        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged(m => m.Id);
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged(m => m.Name);
            }
        }

        public ObservableCollection<string> AchivementsComplete
        {
            get { return achivementsComplete; }
            set
            {
                achivementsComplete = value;
                NotifyPropertyChanged(vm => vm.AchivementsComplete);
            }
        }

        public string Language
        {
            get { return language; }
            set
            {
                language = value;
                NotifyPropertyChanged(vm => vm.Language);
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                NotifyPropertyChanged(vm => vm.Description);
            }
        }

        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                NotifyPropertyChanged(vm => vm.Gender);
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                NotifyPropertyChanged(vm => vm.City);
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                NotifyPropertyChanged(vm => vm.Country);
            }
        }
    }
}