using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using DampGUI;
using Microsoft.Expression.Interactivity.Core;

namespace DampGUI
{
    /// <summary>
    /// Interaction logic for GameControlView.xaml
    /// </summary>
    public partial class GameControlView : UserControl
    {
        private PhotoCollection _photoCollection;
        private ThumbnailEventArgs eb = new ThumbnailEventArgs();
        private Storyboard sb;
        public GameControlView(PhotoCollection aPhotoCollection)
        {
            InitializeComponent();
            _photoCollection = aPhotoCollection;
            
            //UserControl_Loaded();
           
            ThreadPhoto();
        }

        public void ThreadPhoto()
        {
            PhotoStuff.Content = new PhotoCollectionView(_photoCollection);
        }

        private double ThumbnailWidth
        {
            //grd.ActualWidth
            get { return (600)/4; }
        }

        //private void UserControl_Loaded()
        //{
        //    double thumbWidth = ThumbnailWidth;
        //    bool setFirstItemChecked = true;
        //    int count = 0;

        //    foreach (var photo in _photoCollection)
        //    {
        //        if (photo.IsMade == false)
        //        {
        //            photo.create();
        //        }
        //        AddPhotoToStack(photo, thumbWidth, setFirstItemChecked);
        //        setFirstItemChecked = false;
        //        count++;
        //    }

        //}

        //private void AddPhotoToStack(Photo photo, double thumbWidth, bool setFirstItemChecked, bool isInsert = false)
        //{

        //        this.Dispatcher.Invoke(new Action(() =>
        //        {            
        //        ThumbnailPhotoView tp = new ThumbnailPhotoView();    
        //        tp.ThumbnailClick += new EventHandler<ThumbnailEventArgs>(tp_ThumbnailClick);
                
        //            tp.ImageSource = photo.Image;
        //            tp.ImageHeight = thumbWidth / 1.3;
        //            tp.ImageWidth = thumbWidth;
                
        //        if (isInsert)
        //        {
        //            photos.Children.Insert(0, tp);
        //        }
        //        else
        //        {
        //            photos.Children.Add(tp);
        //        }
        //        tp.IsSelected = setFirstItemChecked;
        //        UpdateLayout();
     
        //    }));
        //}

        //private void tp_ThumbnailClick(object sender, ThumbnailEventArgs e)
        //{
        //    mainPhoto.Source = e.ImageSource;
        //    mainPhoto.Width = this.ActualWidth/2.0;
        //    eb.ImageSource = e.ImageSource;
        //}

        //private void mainPhoto_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    var other = new BigPictureView();
        //    other.BigMainPhoto.Source = eb.ImageSource;
        //    other.Show();
        //}
        private void GameControlView_OnLoaded(object sender, RoutedEventArgs e)
        {
//ThreadPhoto();
        }
    }
}
