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
    public class Games : ModelBase<Games>, IGames
    {
        private int currentIndex = -1;
        private int totalGames = 0;
        private ObservableCollection<IGame> games = new ObservableCollection<IGame>();

        /**
          *  Add
          * 
          * @brief this function Add the Game to the ObservableCollection
          * @param IGame aGame
          */
        public void Add(IGame aGame)
        {
            totalGames++;
            games.Add(aGame);
        }

        /**
          *  Get
          * 
          * @brief this function get the game at the selected index(i)
          * @param int i
          * @return IGame
          */
        public IGame Get(int i)
        {
            return games[i];
        }

        /**
        *  CurrentGame
        * 
        * @brief this is a property that gets the current game
        * @param set(IGame)
        * @return get(IGame)
        */
        public IGame CurrentGame
        {
            get
            {
                if (currentIndex >= 0)
                {
                    if (games[currentIndex].PhotoCollection.IsMade == false)
                    {
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

                return null;
            }
            set
            {
                CurrentIndex = games.IndexOf(value);
                NotifyPropertyChanged(vm => vm.CurrentGame);
            }
        }

        /**
          *  CurrentIndex
          * 
          * @brief this is a property gets the current games index
          * @param set(int)
          * @return get(int)
          */
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

        /**
          *  TotalGames
          * 
          * @brief this is a property that gets the total number of games
          * @param set(int)
          * @return get(int)
          */
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
}
