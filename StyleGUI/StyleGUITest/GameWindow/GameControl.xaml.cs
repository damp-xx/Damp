using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ListViewItem = System.Windows.Controls.ListViewItem;
using UserControl = System.Windows.Controls.UserControl;

namespace StyleGUITest
{
    public partial class GameControl : UserControl
    {
        
        private const int _maximumPhotoInStack = 25; //husk at tænke på cap
        private PhotoCollection _photoCollection;
        ThumbnailEventArgs eb =new ThumbnailEventArgs();
        
        public GameControl()
        {
            InitializeComponent();

        }

        private double ThumbnailWidth
        {
            get
            {
                return (grd.ActualWidth)/4;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            double thumbWidth = ThumbnailWidth;
            _photoCollection = new PhotoCollection();
            _photoCollection.Path = @".\Game";
            bool setFirstItemChecked = true;
            int count = 0;
            foreach (var photo in _photoCollection)
            {
                AddPhotoToStack(photo, thumbWidth, setFirstItemChecked);
                setFirstItemChecked = false;
                count++;
                if(count == _maximumPhotoInStack)
                {
                    break;
                }
            }
        }

        private void AddPhotoToStack(Photo photo, double thumbWidth, bool setFirstItemChecked, bool isInsert = false)
        {
            ThumbnailPhoto tp = new ThumbnailPhoto();
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
            //tp.IsSelected = setFirstItemChecked;
        }

        void tp_ThumbnailClick(object sender, ThumbnailEventArgs e)
        {
            mainPhoto.Source = e.ImageSource;
            mainPhoto.Width = this.ActualWidth/2.0;
            eb.ImageSource = e.ImageSource;
        }

        private void mainPhoto_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var other = new BigPicture();
            other.BigMainPhoto.Source = eb.ImageSource;

            other.Show();

        }

    }
}
