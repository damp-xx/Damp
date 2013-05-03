using System;
using System.Collections.ObjectModel;
using DampGUI;
using SimpleMvvmToolkit;

namespace DampGUI
{
    public class Games : ModelBase<Games>
    {
        private int currentIndex = -1; //skal være -1 normalt
        private int totalGames = 0;
        private ObservableCollection<Game> games = new ObservableCollection<Game>();

        public Games()
        {
        }

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
                    return games[CurrentIndex];
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


    public class Game
    {
        private string _title;
        private string description;
        private string genre;
        private string developer;
        private string language;
        private string mode;
        private ObservableCollection<string> achivementsGame;
        private PhotoCollection _photoCollection;

        public Game()
        {
        }

        public Game(string aName, string aDescription, string aGenre, string aDeveloper, string aMode,
                    string aLanguage, ObservableCollection<string> aAchivementsGame, PhotoCollection aPhotoCollection)
        {
            Title = aName;
            Description = aDescription;
            Genre = aGenre;
            Developer = aDeveloper;
            Mode = aMode;
            Language = aLanguage;
            AchivementsGame = aAchivementsGame;
            PhotoCollection = aPhotoCollection;
        }

        public ObservableCollection<string> AchivementsGame
        {
            get { return achivementsGame; }
            set { achivementsGame = value; }
        }

        public PhotoCollection PhotoCollection
        {
            get { return _photoCollection; }
            set { _photoCollection = value; }
        }



        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        public string Developer
        {
            get { return developer; }
            set { developer = value; }
        }

        public string Mode
        {
            get { return mode; }
            set { mode = value; }
        }
    }

}

