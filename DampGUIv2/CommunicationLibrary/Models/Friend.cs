using System.Collections.ObjectModel;
using SimpleMvvmToolkit;

namespace DampGUI
{
    /// <summary>
    /// This class contains properties that a Friend have
    /// </summary>
    public class Friend : ModelBase<Friend>, IFriend
    {
        public string RealName { get; set; }

        public IPhoto Photo { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public ObservableCollection<string> AchivementsComplete { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public string Gender { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}