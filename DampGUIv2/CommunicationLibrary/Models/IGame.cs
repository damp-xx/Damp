using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DampGUI
{
    public interface IGame
    {
         ObservableCollection<string> AchivementsGame { get; set; }

         IPhotoCollection PhotoCollection { get; set; }

         string Title { get; set; }

         string Language { get; set; }

         string Description { get; set; }

         string Genre { get; set; }

         string Developer { get; set; }

         string Mode { get; set; }

         string GameId { get; set; }
    }
}
