using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StyleGUITest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameControl gameW = new GameControl();
        HomeScreen homeW = new HomeScreen();
        FindFriendWindow findFriend = new FindFriendWindow();
        ProfileWindow profileWindow = new ProfileWindow();
        Settings settings = new Settings();
        private bool normalWindow = true;
        
        public MainWindow()
        {
            SplashScreen splashScreen = new SplashScreen("Images/Splashscreen.png");
            splashScreen.Show(true);
            Thread.Sleep(10);
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth/1.2;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight/1.2;
  
            InitializeComponent();
  
            ContentCtrl.Content = homeW;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {         
            ContentCtrl.Content = gameW;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ContentCtrl.Content = homeW;
        }

        private void FindFriendsButton_Click(object sender, RoutedEventArgs e)
        {
            ContentCtrl.Content = findFriend;
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            ContentCtrl.Content = profileWindow;
        }

        private void Drag_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            e.Handled = false;
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindowDamp.Close();
        }

        private void MaximizeNormal_Click(object sender, RoutedEventArgs e)
        {
            if (normalWindow)
            {
                this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                this.Height = System.Windows.SystemParameters.PrimaryScreenHeight - 38;

                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;

                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));
                this.Left = corner.X - this.ActualWidth;
                this.Top = corner.Y - this.ActualHeight;
                normalWindow = false;
               
            }
            else
            {
                
                this.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 1.2;
                this.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 1.2;

                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;

                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));
                this.Left = (corner.X - this.ActualWidth)/2;
                this.Top = (corner.Y - this.ActualHeight)/2;
                normalWindow = true;
            }
        }


        private void Minimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            settings.Owner = this;
            settings.Show();

        }
    }
}
