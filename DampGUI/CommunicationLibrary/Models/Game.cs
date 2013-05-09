using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DampGUI;
using SimpleMvvmToolkit;
using System.Linq;

namespace DampGUI
{
    public class Games : ModelBase<Games>
    {
        private int currentIndex = -1; //skal være -1 normalt
        private int totalGames = 0;
        private ObservableCollection<Game> games = new ObservableCollection<Game>();

        public void Add(Game aGame)
        {
            totalGames++;
            games.Add(aGame);
        }

        public Game Get(int i)
        {
            return games[i];
        }



        public Game CurrentGame
        {
            get
            {
                if (currentIndex >= 0)
                {

                    if (games[currentIndex].PhotoCollection.IsMade == false)
                    {

                        //    foreach(var photo in games[currentIndex].PhotoCollection)
                        //{

                        //    if (photo.IsMade == false)
                        //    {
                        //        photo.LoadPicture();
                        //    }
                        //    games[currentIndex].PhotoCollection.IsMade = true;
                        //}
                        games[currentIndex].PhotoCollection.ToList().AsParallel().ForAll(photo =>
                            {

                                if (photo.IsMade == false)
                                {
                                    photo.LoadPicture();

                                }
                                  games[currentIndex].PhotoCollection.IsMade = true;
                            });
                    }
                    return
                    games[CurrentIndex];
            
                }
                else
                    return null;
            }
            set
            {
                CurrentIndex = games.IndexOf(value);
                NotifyPropertyChanged(vm => vm.CurrentGame);
            }
        }

        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                if (currentIndex != value)
                {
                    currentIndex = value;
                    NotifyPropertyChanged(vm => vm.CurrentGame);
                    NotifyPropertyChanged(vm => vm.CurrentIndex);
                }
            }
        }

        public int TotalGames
        {
            get { return totalGames; }
            set
            {

                totalGames = value;
                NotifyPropertyChanged(vm => vm.totalGames);
            }
        }
}


    public class Game: IGame
    {
        public Game()
        {
        }

        public ObservableCollection<string> AchivementsGame { get; set; }

        public PhotoCollection PhotoCollection { get; set; }

        public string Title { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public string Developer { get; set; }

        public string Mode { get; set; }
    }

}

