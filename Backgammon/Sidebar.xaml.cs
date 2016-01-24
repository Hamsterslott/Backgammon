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
using Microsoft.Win32;

namespace Backgammon
{
    /// <summary>
    /// Interaction logic for Sidebar.xaml
    /// </summary>
    public partial class Sidebar : UserControl
    {
        MainWindow _mainWindow = null;
        public bool closedMeny = false;

        public Sidebar()
        {
            InitializeComponent();
        }

        private void CloseMeny_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnOn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnOn.Background = Brushes.LawnGreen;
            btnOff.Background = Brushes.Transparent;
            btnOn.Foreground = Brushes.Black;
            btnOff.Foreground = Brushes.White;
            Settings.playSound = true;
        }

        private void btnOff_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnOn.Background = Brushes.Transparent;
            btnOff.Background = Brushes.LawnGreen;
            btnOn.Foreground = Brushes.White;
            btnOff.Foreground = Brushes.Black;
            Settings.playSound = false;
        }

        private void SettingsWindow_MouseEnter(object sender, MouseEventArgs e)
        {
            _mainWindow.canMoveWindow = false;
        }

        private void SettingsWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            _mainWindow.canMoveWindow = true;
        }

        public void setLink(MainWindow mw) {
            _mainWindow = mw;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _mainWindow.song.Volume = e.NewValue;
        }

        private void getSource_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void NameOfTheSong_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                NameOfTheSong.Content = openFileDialog.SafeFileName;
                _mainWindow.song.Open(new Uri(openFileDialog.FileName));
                //_mainWindow.song.Play();
                _mainWindow.song.Stop();
            }
        }

        private void BlackWhite_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void WhiteBlue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RedBlack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
