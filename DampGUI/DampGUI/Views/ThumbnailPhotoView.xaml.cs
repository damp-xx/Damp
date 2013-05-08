﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace DampGUI
{
    /// <summary>
    /// Interaction logic for ThumbnailPhotoView.xaml
    /// </summary>
    public partial class ThumbnailPhotoView : UserControl
    {
        public ThumbnailPhotoView()
        {
            InitializeComponent();
        }

  

        public ImageSource ImageSource
        {
            set
            {
                img.Source = value;
                this.Tag = value;
            }
        }
        public double ImageWidth
        {
            set
            {
                this.Width = value;
            }
        }

        public double ImageHeight
        {
            set
            {
                this.Height = value;
            }
        }

        public event EventHandler<ThumbnailEventArgs> ThumbnailClick;

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(ThumbnailClick != null)
            {
                ThumbnailClick(sender, new ThumbnailEventArgs() { ImageSource = this.Tag as ImageSource });
            }
        }
        public bool IsSelected
        {
            set { rdo.IsChecked = value; }
            get { return rdo.IsChecked.HasValue && rdo.IsChecked.Value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void Connect(int connectionId, object target)
        {
        }
    }
    public class ThumbnailEventArgs:EventArgs
    {
        public ImageSource ImageSource { get; set; }
    }
}
