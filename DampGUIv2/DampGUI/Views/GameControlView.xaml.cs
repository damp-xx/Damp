using System.Windows.Controls;

namespace DampGUI
{
    /// <summary>
    /// Interaction logic for GameControlView.xaml
    /// </summary>
    public partial class GameControlView : UserControl
    {
        private IPhotoCollection _photoCollection;

        public GameControlView(IPhotoCollection aPhotoCollection)
        {
            InitializeComponent();
            _photoCollection = aPhotoCollection;

            ThreadPhoto();
        }

        public void ThreadPhoto()
        {
            PhotoStuff.Content = new PhotoCollectionView(_photoCollection);
        }
    }
}
