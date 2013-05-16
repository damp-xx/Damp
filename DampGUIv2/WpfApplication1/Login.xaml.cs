using System;
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
using DampGUI;
using Login;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Login.MainWindow W = new Login.MainWindow();
        private DampGUI.MainWindow M = new DampGUI.MainWindow();
        public MainWindow()
        {
            this.Close();
            W.Show();
            W.Login += new Login.MainWindow.LoggedIn(LoginListener);
            InitializeComponent();
        }

        private void LoginListener(object sender, EventArgs e)
        {
            W.Close();
            M.Show();
        }

    }
}
