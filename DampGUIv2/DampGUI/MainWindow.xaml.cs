using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CommunicationLibrary;

namespace DampGUI
{
    public struct MainWindowDispatcher
    {
        public Dispatcher MwDispatcher;
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool normalWindow = true;
        public static MainWindowDispatcher _MWDispatcher;

        public MainWindow()
        {
            InitializeComponent();
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth/1.2;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight/1.2;
            _MWDispatcher = new MainWindowDispatcher
                {
                    MwDispatcher = Dispatcher
                };
            ComEvents.Listen();
        }


        private void Drag_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            e.Handled = false;
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
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
                this.Width = System.Windows.SystemParameters.PrimaryScreenWidth/1.2;
                this.Height = System.Windows.SystemParameters.PrimaryScreenHeight/1.2;

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
    }
}
