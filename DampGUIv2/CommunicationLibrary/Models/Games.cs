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

        public void Add(IGame aGame)
        {
            totalGames++;
            games.Add(aGame);
        }

        public IGame Get(int i)
        {
            return games[i];
        }

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
}
