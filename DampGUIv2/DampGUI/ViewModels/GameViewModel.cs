using System;
using SimpleMvvmToolkit;

namespace DampGUI
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// </summary>
    public class GameViewModel : ViewModelBase<GameViewModel>
    {
        /**
         *  GameViewModel
         * 
         * @brief constructor for GameViewModel
         * @param Games aGames	 
         */

        public GameViewModel(Games aGames)
        {
            Games = aGames;
            PlayIns = "Play";//skal sættes efter tilstand
        }

        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        /**
         *  Games
         * 
         * @brief property for Games 
         * @param set(IGames)	 
         * @return get(IGames)
         */
        public IGames Games { get; set; }

        /**
         *  CurrentGame
         * 
         * @brief this property gets the currentGame, binded to all data
         * @param set(IGame)	 
         * @return get(IGame)
         */
        public IGame CurrentGame
        {
            get
            {
                if (Games.CurrentIndex >= 0)
                    return Games.CurrentGame;
        
                    return null;
            }
        }

        /**
         *  PlayIns
         * 
         * @brief this property is binded to play button
         * @param set(string)	 
         * @return get(string)
         */
        public string PlayIns { get; set; }

        /**
         *  Playbutton
         * 
         * @brief this function starts the selected game, binded to button click event
         */
        public void Playbutton()
        {
            //ToDO: Pierre skal sætte sin play func ind
        }
    }
}