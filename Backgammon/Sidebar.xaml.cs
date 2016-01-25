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
            BlackWhite.Background.Opacity = 1;
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
            _mainWindow.song.Volume = e.NewValue / 10;
        }

        private void getSource_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void NameOfTheSong_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|WAV files (*.wav)|*.wav";
            if (openFileDialog.ShowDialog() == true)
            {
                NameOfTheSong.Content = openFileDialog.SafeFileName;
                _mainWindow.song.Open(new Uri(openFileDialog.FileName));
                slider.Value = 5;
                _mainWindow.song.Volume = slider.Value / 10;
                _mainWindow.song.Play();
            }
        }

        private void BlackWhite_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WhiteBlue.Background.Opacity = 0.5;
            RedBlack.Background.Opacity = 0.5;
            Settings.playerTheme = new int[] { 1, 0 };
            _mainWindow.updateView();
        }

        private void WhiteBlue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BlackWhite.Background.Opacity = 0.5;
            RedBlack.Background.Opacity = 0.5;
            Settings.playerTheme = new int[] { 2, 0 };
            _mainWindow.updateView();
        }

        private void RedBlack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WhiteBlue.Background.Opacity = 0.5;
            BlackWhite.Background.Opacity = 0.5;
            Settings.playerTheme = new int[] { 3, 1 };
            _mainWindow.updateView();
        }
        private void HowToPlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)

        {
            MessageBox.Show(Properties.Resources.rules);
        }

        private void BlackWhite_MouseEnter(object sender, MouseEventArgs e)
        {
            BlackWhite.Background.Opacity = 1;
        }

        private void BlackWhite_MouseLeave(object sender, MouseEventArgs e)
        {
            if(Settings.playerTheme[0] != 1)
                BlackWhite.Background.Opacity = 0.5;
        }

        private void WhiteBlue_MouseEnter(object sender, MouseEventArgs e)
        {
            WhiteBlue.Background.Opacity = 1;
        }

        private void WhiteBlue_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Settings.playerTheme[0] != 2)
                WhiteBlue.Background.Opacity = 0.5;
        }

        private void RedBlack_MouseEnter(object sender, MouseEventArgs e)
        {
            RedBlack.Background.Opacity = 1;
        }

        private void RedBlack_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Settings.playerTheme[0] != 3)
                RedBlack.Background.Opacity = 0.5;
        }

        private void HowToPlay_MouseEnter(object sender, MouseEventArgs e)
        {
            HowToPlay.Background.Opacity = 1;
        }

        private void HowToPlay_MouseLeave(object sender, MouseEventArgs e)
        {
            HowToPlay.Background.Opacity = 0.5;
        }

        private void NewGameImg_MouseEnter(object sender, MouseEventArgs e)
        {
            NewGameImg.Background.Opacity = 1;
        }

        private void NewGameImg_MouseLeave(object sender, MouseEventArgs e)
        {
            NewGameImg.Background.Opacity = 0.5;
        }      

        private void BackgroundActive_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.setBackground(0);
        }

        private void BackgroundPrev_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.setBackground(2);
        }

        private void BackgroundPrev2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.setBackground(4);
        }

        private void BackgroundNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.setBackground(5);
        }

        private void BackgroundNext2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.setBackground(7);
        }

		private void btnOn_Click(object sender, RoutedEventArgs e)
		{
			btnOn.Background = Brushes.LawnGreen;
            btnOff.Background = Brushes.Transparent;
            btnOn.Foreground = Brushes.Black;
            btnOff.Foreground = Brushes.Snow;
            Settings.playSound = true;
		}

		private void btnOff_Click(object sender, RoutedEventArgs e)
		{
			btnOn.Background = Brushes.Transparent;
            btnOff.Background = Brushes.LawnGreen;
            btnOn.Foreground = Brushes.Snow;
            btnOff.Foreground = Brushes.Black;
            Settings.playSound = false;
		}

        private void CloseMenyText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CloseMenyText_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseMenyText.Opacity = 1;
            CloseMeny.Background.Opacity = 1;
        }

        private void CloseMenyText_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseMenyText.Opacity = 0.5;
            CloseMeny.Background.Opacity = 0.5;
        }

    }
}
