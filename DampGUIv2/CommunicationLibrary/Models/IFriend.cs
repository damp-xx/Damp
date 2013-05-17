using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DampGUI
{
    public interface IFriend
    {
        string Country { get; set; }
        IPhoto Photo { get; set; }
        string Id { get; set; }
        string Name { get; set; }
        ObservableCollection<string> AchivementsComplete { get; set; }
        string Language { get; set; }
        string Description { get; set; }
        string Gender { get; set; }
        string City { get; set; }
        string RealName { get; set; }

    }
}
