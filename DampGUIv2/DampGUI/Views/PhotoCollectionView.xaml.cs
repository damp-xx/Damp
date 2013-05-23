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

        /**
         * ThumbnailPhotoView
         * 
         * @brief Creates the  PhotoCollectionView
         * @param IPhotoCollection
         */
        public PhotoCollectionView(IPhotoCollection aPhotoCollection)
        {
            InitializeComponent();
            _photoCollection = aPhotoCollection;
            UserControl_Loaded();
            mainPhoto.Width = 100;
        }

        /**
         * ThumbnailWidth
         * 
         * @brief ThumbnailWidth Property is used for determining the width on the thumbnails  
         * @return get(double) 
         */
        private double ThumbnailWidth
        {
            get { return (600)/4; }
        }

        /**
         * UserControl_Loaded
         * 
         * @brief this function loads the Photos in to the PhotoCollectionView GUI 
         */
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

        /**
         * AddPhotoToStack
         * 
         * @brief this function loads the Photos in to the PhotoCollectionView GUI 
         * @param IPhoto photo, double thumbWidth, bool setFirstItemChecked, bool isInsert = false 
         */
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

        /**
         * tp_ThumbnailClick
         * 
         * @brief this click event takes the selected thumbnail and sets it as the mainPhoto
         * @param object sender, ThumbnailEventArgs e
         */
        private void tp_ThumbnailClick(object sender, ThumbnailEventArgs e)
        {
            mainPhoto.Source = e.ImageSource;
            mainPhoto.Width = this.ActualWidth/2.0;
            eb.ImageSource = e.ImageSource;
        }

        /**
         * mainPhoto_MouseDown
         * 
         * @brief this click event creates a BigPictureView, which takes the selected mainPhoto and makes a bigger picture
         * @param object sender, MouseButtonEventArgs e
         */
        private void mainPhoto_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var other = new BigPictureView();
            other.BigMainPhoto.Source = eb.ImageSource;
            other.Show();
        }
    }
}
