using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

namespace DampGUI
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class GameViewModel : ViewModelBase<GameViewModel>
    {
        // TODO: Add a member for IXxxServiceAgent
        private IGameServiceAgent serviceAgent;
        Games games;
        
        // Default ctor
        public GameViewModel()
        {
        }

        // TODO: ctor that accepts IXxxServiceAgent
        public GameViewModel(IGameServiceAgent serviceAgent,Games aGames)
        {
            games = aGames;
            PlayIns = "Play";
            this.serviceAgent = serviceAgent;
        }
        

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        // TODO: Add properties using the mvvmprop code snippet
        
        public Games Games
        {
            get { return games; }
            set { games = value;
     //       NotifyPropertyChanged(vm => vm.CurrentGame);
            }
        }

        public int GrdViewWidth

        {
            get { return GrdViewWidth; }    
        }

        
        public Game CurrentGame
        {
            get
            {
                if (games.CurrentIndex >= 0)
                    return games.CurrentGame;
                else
                    return null;
            }
        }

        public string PlayIns { get; set; }
        

        // TODO: Add methods that will be called by the view
        
        public void Playbutton()
        {
            if (CurrentGame.Playins == true)
            {
                
            }
            else
            {
                
            }
        }

        // TODO: Optionally add callback methods for async calls to the service agent
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
    }
}