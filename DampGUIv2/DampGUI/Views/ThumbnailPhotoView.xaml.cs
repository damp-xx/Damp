using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DampGUI
{
    /// <summary>
    /// Interaction logic for ThumbnailPhotoView.xaml
    /// </summary>
    public partial class ThumbnailPhotoView : UserControl
    {
        /**
         * ThumbnailPhotoView
         * 
         * @brief Creates the ThumbnailPhotoView 
         */
        public ThumbnailPhotoView()
        {
            InitializeComponent();
        }

        /**
         * ImageSource
         * 
         * @brief ImageSource Property 
         * @param set(ImageSource);
         */
        public ImageSource ImageSource
        {
            set
            {
                img.Source = value;
                this.Tag = value;
            }
        }
        /**
         * ImageWidth
         * 
         * @brief ImageWidth Property 
         * @param set(double); 
         * @return 
         */
        public double ImageWidth
        {
            set { this.Width = value; }
        }

        /**
         * ImageHeight
         * 
         * @brief ImageHeight Property 
         * @param set(double); 
         */
        public double ImageHeight
        {
            set { this.Height = value; }
        }

        public event EventHandler<ThumbnailEventArgs> ThumbnailClick;

        /**
         * RadioButton_Checked
         * 
         * @brief this function activates the thumbnailevent
         * @param object sender, RoutedEventArgs e
         * @return 
         */
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (ThumbnailClick != null)
            {
                ThumbnailClick(sender, new ThumbnailEventArgs() {ImageSource = this.Tag as ImageSource});
            }
        }

        /**
         * IsSelected
         * 
         * @brief IsSelected Property
         * @param set(bool);
         * @return get(bool);
         */
        public bool IsSelected
        {
            set { rdo.IsChecked = value; }
            get { return rdo.IsChecked.HasValue && rdo.IsChecked.Value; }
        }
    }

    /// <summary>
    /// ThumbnailEventArgs class is used when you make a thumbnail, as an event
    /// </summary>
    public class ThumbnailEventArgs : EventArgs
    {
        /**
         * ImageSource
         * 
         * @brief ImageSource Property 
         * @param set(ImageSource);
         * @return get(ImageSource);
         */
        public ImageSource ImageSource { get; set; }
    }
}
