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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Backgammon
{
    /// <summary>
    /// Interaction logic for WinScreen.xaml
    /// </summary>
    public partial class WinScreen : Window
    {

        public MainWindow mainWindow;
        private bool spaceUsed = false;

        public WinScreen()
        {
            InitializeComponent();
        }

        public void setWinner(string winner) {
            Winner.Content = winner + " WON!!!";
        }
        public void setLink(MainWindow mw) {
            mainWindow = mw;
            this.Width = mw.Width;
            this.Height = mw.Height;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try { DragMove(); }
            catch(Exception){}
        }

        private void maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized) {
                WindowState = WindowState.Normal;
                Width = 1280;
                Height = 720;
            }
            else
                WindowState = WindowState.Maximized;
        }

        private void minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && !spaceUsed) {
                DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(2));
                fadeOut.Completed += new EventHandler(fadeOut_Completed);
                this.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, fadeOut);
                spaceUsed = true;
            }
        }
        private void fadeOut_Completed(Object sender, EventArgs e) {
            mainWindow = new MainWindow();
            mainWindow.Show();
            DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
            fadeIn.Completed += new EventHandler(fadeIn_Completed);
            mainWindow.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, fadeIn);
        }
        private void fadeIn_Completed(Object sender, EventArgs e) {
            this.Close();
        }
    }
}
