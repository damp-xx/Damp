using System.Windows.Controls;

namespace DampGUI
{
    /// <summary>
    /// Interaction logic for GameControlView.xaml
    /// </summary>
    public partial class GameControlView : UserControl
    {
        private IPhotoCollection _photoCollection;

        /**
         * GameControlView
         * 
         * @brief Is the constructor that takes a IPhotoCollection in to show the game Photos within it
         * @param IPhotoCollection aPhotoCollection
         */
        public GameControlView(IPhotoCollection aPhotoCollection)
        {
            InitializeComponent();
            _photoCollection = aPhotoCollection;

            ThreadPhoto();
        }

        /**
         * ThreadPhoto
         * 
         * @brief this function loads the PhotoCollectionView usercontrol up, and sets it as the content in the GameControlView 
         */
        public void ThreadPhoto()
        {
            PhotoStuff.Content = new PhotoCollectionView(_photoCollection);
        }
    }
}
