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
using Microsoft.Win32;

namespace Backgammon
{
    /// <summary>
    /// Interaction logic for Sidebar.xaml
    /// </summary>
    public partial class Sidebar : UserControl
    {
        MainWindow _mainWindow = null;
		private int[] _backgroundnumbers = new int []{1,2,4,5,7};



        public Sidebar()
        {
            InitializeComponent();
            BlackWhite.Background.Opacity = 1;
			howtoplay.Content = Properties.Resources.rules;
        }

		private void Swap<T>(ref T obj1, ref T obj2)
		{
			var temp = obj1;
			obj1 = obj2;
			obj2 = temp;
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
			_mainWindow.setCursor();
            _mainWindow.updateView();
        }

        private void WhiteBlue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BlackWhite.Background.Opacity = 0.5;
            RedBlack.Background.Opacity = 0.5;
            Settings.playerTheme = new int[] { 2, 0 };
			_mainWindow.setCursor();
            _mainWindow.updateView();
        }

        private void RedBlack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WhiteBlue.Background.Opacity = 0.5;
            BlackWhite.Background.Opacity = 0.5;
            Settings.playerTheme = new int[] { 1, 3 };
			_mainWindow.setCursor();
            _mainWindow.updateView();
        }
        private void HowToPlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
			if(info.Width == new GridLength(0))
			{
			_mainWindow.Sidebar.Width = 290+600;
			info.Width = new GridLength(600);
			}
			else
			{
				_mainWindow.Sidebar.Width = 290;
				info.Width = new GridLength(0);
			}
			
			
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

        private void BackgroundPrev_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.setBackground(_backgroundnumbers[1]);
			BackgroundActive.Background = _mainWindow._background[_backgroundnumbers[1]];
			BackgroundPrev.Background = _mainWindow._background[_backgroundnumbers[0]];
			Swap(ref _backgroundnumbers[0],ref _backgroundnumbers[1]);
        }

        private void BackgroundPrev2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.setBackground(_backgroundnumbers[2]);
			BackgroundActive.Background = _mainWindow._background[_backgroundnumbers[2]];
			BackgroundPrev2.Background = _mainWindow._background[_backgroundnumbers[0]];
			Swap(ref _backgroundnumbers[0],ref _backgroundnumbers[2]);
        }

        private void BackgroundNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.setBackground(_backgroundnumbers[3]);
			BackgroundActive.Background = _mainWindow._background[_backgroundnumbers[3]];
			BackgroundNext.Background = _mainWindow._background[_backgroundnumbers[0]];
			Swap(ref _backgroundnumbers[0],ref _backgroundnumbers[3]);
        }

        private void BackgroundNext2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.setBackground(_backgroundnumbers[4]);
			BackgroundActive.Background = _mainWindow._background[_backgroundnumbers[4]];
			BackgroundNext2.Background = _mainWindow._background[_backgroundnumbers[0]];
			Swap(ref _backgroundnumbers[0],ref _backgroundnumbers[4]);
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
			_mainWindow.Sidebar.Width = 290;
			info.Width = new GridLength(0);

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
		private void PrevSong_Click(object sender, RoutedEventArgs e)
		{
			_mainWindow.song.Play();
		}

		private void NextSong_Click(object sender, RoutedEventArgs e)
		{
			_mainWindow.song.Pause();
		}

		private void NewGameImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_mainWindow.gameBoard = _mainWindow._model.newGame();
			_mainWindow.dice = new int[4];
			_mainWindow.pickedUp = 0;
			_mainWindow.setCursor();
			_mainWindow.spelare = player.two;
			_mainWindow.utslagna[0].setSize(0);
			_mainWindow.utslagna[1].setSize(0);
			_mainWindow.updateView();
			_mainWindow.btnDice.Visibility = System.Windows.Visibility.Visible;
            _mainWindow.diceTop.Visibility = System.Windows.Visibility.Visible;
            _mainWindow.diceBot.Visibility = System.Windows.Visibility.Visible;
			
		}
    }
}
