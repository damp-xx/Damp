using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace DampGUI
{
    /// <summary>
    /// Interaction logic for PhotoCollectionView.xaml
    /// </summary>
    public partial class PhotoCollectionView : UserControl
    {
        private IPhotoCollection _photoCollection;
        private ThumbnailEventArgs eb = new ThumbnailEventArgs();

        public PhotoCollectionView(IPhotoCollection aPhotoCollection)
        {
            InitializeComponent();
            _photoCollection = aPhotoCollection;
            UserControl_Loaded();
            mainPhoto.Width = 100;
        }


        private double ThumbnailWidth
        {
            //grd.ActualWidth
            get { return (600)/4; }
        }

        private void UserControl_Loaded()
        {
            double thumbWidth = ThumbnailWidth;
            bool setFirstItemChecked = true;
            int count = 0;

            foreach (var photo in _photoCollection)
            {
                if (photo.IsMade == false)
                {
                    photo.Create();
                }
                AddPhotoToStack(photo, thumbWidth, setFirstItemChecked);
                setFirstItemChecked = false;
                count++;
            }
        }

        private void AddPhotoToStack(IPhoto photo, double thumbWidth, bool setFirstItemChecked, bool isInsert = false)
        {
            base.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                {
                    ThumbnailPhotoView tp = new ThumbnailPhotoView();
                    tp.ThumbnailClick += new EventHandler<ThumbnailEventArgs>(tp_ThumbnailClick);

                    tp.ImageSource = photo.Image;
                    tp.ImageHeight = thumbWidth/1.3;
                    tp.ImageWidth = thumbWidth;


                    if (isInsert)
                    {
                        photos.Children.Insert(0, tp);
                    }
                    else
                    {
                        photos.Children.Add(tp);
                    }
                    tp.IsSelected = setFirstItemChecked;
                }));
        }

        private void tp_ThumbnailClick(object sender, ThumbnailEventArgs e)
        {
            mainPhoto.Source = e.ImageSource;
            mainPhoto.Width = this.ActualWidth/2.0;
            eb.ImageSource = e.ImageSource;
        }

        private void mainPhoto_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var other = new BigPictureView();
            other.BigMainPhoto.Source = eb.ImageSource;
            other.Show();
        }
    }
}
