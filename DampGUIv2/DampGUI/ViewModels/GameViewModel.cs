using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Xml;
using SimpleMvvmToolkit;
using CommunicationLibrary;
using System.IO.Compression;
using GTCcomLib;

namespace DampGUI
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// </summary>
    public class GameViewModel : ViewModelBase<GameViewModel>
    {
        private XmlElement gameXml;
        private string installPath;
        public GameViewModel(Games aGames)
        {
            Games = aGames;
            gameXml = ComGame.GetGame(CurrentGame.GameId);
            installPath =
                (gameXml.GetElementsByTagName("Message").Item(0).InnerText).Split('=').Last().Split('.').First();
            PlayIns = Directory.Exists(@installPath) ? "Play" : "Install";
        }

        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        public IGames Games { get; set; }

        public int GrdViewWidth

        {
            get { return GrdViewWidth; }
        }

        public IGame CurrentGame
        {
            get
            {
                if (Games.CurrentIndex >= 0)
                    return Games.CurrentGame;
        
                    return null;
            }
        }

        private string _playIns;
        public string PlayIns
        {
            get { return _playIns; }
            set
            {
                _playIns = value;
                NotifyPropertyChanged(vm => vm.PlayIns);
            }
        }


        public async void Playbutton()
        {
            //ToDO: Pierre skal sætte sin play func ind
            
            if (PlayIns == "Play")
            {
                XmlDocument gameManifest = new XmlDocument();
                gameManifest.Load(installPath+@"\manifest.xml");
                var executableFile = gameManifest.GetElementsByTagName("File").Item(0).InnerText;

                string gameString = installPath + @"\" + executableFile;
                var gameClient = new GTCcomLib.GTCcomLib(gameString);
                gameClient.RunGame();
                gameClient.SendMessage("CPN", "Barge");
            }
            else if (PlayIns == "Install")
            {
                
                Uri downloadUri = new Uri(gameXml.GetElementsByTagName("Message").Item(0).InnerText);
                if (!Directory.Exists( @"temp"))
                {
                    Directory.CreateDirectory(@"temp");
                }
                string tempFile = Directory.GetCurrentDirectory() +  @"\temp\" + CurrentGame.GameId + @".zip";
                InitiateSSLTrust();
                using (WebClient client = new WebClient())
                {
                    PlayIns = "Downloading";
                    Task t = Task.Factory.StartNew(() => client.DownloadFile(downloadUri, tempFile));
                    await t;
                }
                PlayIns = "Installing";
                await unZip(tempFile,@Directory.GetCurrentDirectory());
                File.Delete(tempFile);
                if(IsDirectoryEmpty(@"temp"))
                    Directory.Delete(@"temp");
                PlayIns = "Play";
                
            }
        }

        private bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
        private async Task unZip(string zipPath, string extractPath)
        {
            await Task.Factory.StartNew(() => ZipFile.ExtractToDirectory(zipPath,extractPath));
        }
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }

        public static void InitiateSSLTrust()
        {
            try
            {

                ServicePointManager.ServerCertificateValidationCallback =
                    new RemoteCertificateValidationCallback(
                        delegate { return true; }
                        );
            }
            catch
            {
            }
        }
    }
}