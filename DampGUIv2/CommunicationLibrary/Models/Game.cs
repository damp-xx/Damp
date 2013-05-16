using System.Collections.ObjectModel;
using SimpleMvvmToolkit;
using System.Linq;

namespace DampGUI
{
   
    public class Game: IGame
    {
        public ObservableCollection<string> AchivementsGame { get; set; }

        public IPhotoCollection PhotoCollection { get; set; }

        public string Title { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public string Developer { get; set; }

        public string Mode { get; set; }
    }
}

