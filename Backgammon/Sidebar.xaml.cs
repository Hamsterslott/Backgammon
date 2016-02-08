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
	public partial class Sidebar : UserControl
	{
		MainWindow _mainWindow = null;

		public Sidebar()
		{
			InitializeComponent();
			BlackWhite.Background.Opacity = 1;
			howtoplay.Content = Properties.Resources.ruleseng;
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

		public void setLink(MainWindow mw)
		{
			_mainWindow = mw;
		}

		private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_mainWindow.song.Volume = e.NewValue / 10;
		}

		private void NameOfTheSong_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_mainWindow.canMoveWindow = true;
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|WAV files (*.wav)|*.wav";
			if (openFileDialog.ShowDialog() == true)
			{
				NameOfTheSong.Content = openFileDialog.SafeFileName;
				_mainWindow.song.Open(new Uri(openFileDialog.FileName));
				slider.Value = 5;
				_mainWindow.song.Volume = slider.Value / 10;
				_mainWindow.song.Play();
				Settings.isSongPlaying = true;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/btnPause.png"));
				btnPlayPause.Background = brush;
			}
			_mainWindow.canMoveWindow = false;
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
			if (info.Width == new GridLength(0))
			{
				_mainWindow.Sidebar.Width = 290 + 600;
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
			if (Settings.playerTheme[0] != 1 || Settings.playerTheme[1] != 0)
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
			if (Settings.playerTheme[1] != 3)
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
			_mainWindow.setBackground(Settings.backgroundpos[1]);
			BackgroundActive.Background = _mainWindow._background[Settings.backgroundpos[1]];
			BackgroundPrev.Background = _mainWindow._background[Settings.backgroundpos[0]];
			Swap(ref Settings.backgroundpos[0], ref Settings.backgroundpos[1]);
		}

		private void BackgroundPrev2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_mainWindow.setBackground(Settings.backgroundpos[2]);
			BackgroundActive.Background = _mainWindow._background[Settings.backgroundpos[2]];
			BackgroundPrev2.Background = _mainWindow._background[Settings.backgroundpos[0]];
			Swap(ref Settings.backgroundpos[0], ref Settings.backgroundpos[2]);
		}

		private void BackgroundNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_mainWindow.setBackground(Settings.backgroundpos[3]);
			BackgroundActive.Background = _mainWindow._background[Settings.backgroundpos[3]];
			BackgroundNext.Background = _mainWindow._background[Settings.backgroundpos[0]];
			Swap(ref Settings.backgroundpos[0], ref Settings.backgroundpos[3]);
		}

		private void BackgroundNext2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_mainWindow.setBackground(Settings.backgroundpos[4]);
			BackgroundActive.Background = _mainWindow._background[Settings.backgroundpos[4]];
			BackgroundNext2.Background = _mainWindow._background[Settings.backgroundpos[0]];
			Swap(ref Settings.backgroundpos[0], ref Settings.backgroundpos[4]);
		}

		private void CloseMenyText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_mainWindow.sidebar.Visibility = System.Windows.Visibility.Visible;
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

		internal void NewGameImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (_mainWindow.pickedUp == 1) _mainWindow.glowTriangles(1);
			_mainWindow.gameBoard = _mainWindow._model.newGame();
			_mainWindow.dice = new int[4];
			_mainWindow.pickedUp = 0;
			_mainWindow.setCursor();
			_mainWindow.spelare = player.two;
			_mainWindow.utslagna[0].setSize(0);
			_mainWindow.utslagna[1].setSize(0);
			_mainWindow.updateView();
			_mainWindow.btnDice.Visibility = Visibility.Visible;
			_mainWindow.diceTop.Visibility = Visibility.Visible;
			_mainWindow.diceBot.Visibility = Visibility.Visible;
			_mainWindow.btnHelp.Visibility = Visibility.Collapsed;
		}

		private void btnPlayPause_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (Settings.isSongPlaying)
			{
				_mainWindow.song.Pause();
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/btnPlay.png"));
				btnPlayPause.Background = brush;
				Settings.isSongPlaying = false;
			}
			else
			{
				_mainWindow.song.Play();
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/btnPause.png"));
				btnPlayPause.Background = brush;
				Settings.isSongPlaying = true;
			}
		}

		private void btnPlayPause_MouseEnter(object sender, MouseEventArgs e)
		{
			btnPlayPause.Opacity = 1;
		}

		private void btnPlayPause_MouseLeave(object sender, MouseEventArgs e)
		{
			btnPlayPause.Opacity = 0.5;
		}

		private void NameOfTheSong_MouseEnter(object sender, MouseEventArgs e)
		{
			NameOfTheSong.Opacity = 1;
		}

		private void NameOfTheSong_MouseLeave(object sender, MouseEventArgs e)
		{
			NameOfTheSong.Opacity = 0.5;
		}

		private void btnHelp_MouseEnter(object sender, MouseEventArgs e)
		{
			btnHelp.Opacity = 1;
		}

		private void btnHelp_MouseLeave(object sender, MouseEventArgs e)
		{
			btnHelp.Opacity = 0.5;
		}

		private void btnHelp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (Settings.helpActive)
			{
				Settings.helpActive = false;
				btnHelp.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/off.png")));
				if (_mainWindow.pickedUp == 1) _mainWindow.glowTriangles(1);
			}
			else
			{
				Settings.helpActive = true;
				btnHelp.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/on.png")));
				_mainWindow.btnHelp.Visibility = Visibility.Collapsed;
				if (_mainWindow.pickedUp == 1) _mainWindow.glowTriangles(0);
			}
		}

		private void btnSoundEffects_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (Settings.playSound)
			{
				Settings.playSound = false;
				btnSoundEffects.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/off.png")));
			}
			else
			{
				Settings.playSound = true;
				btnSoundEffects.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/on.png")));
			}
		}

		private void btnSoundEffects_MouseLeave(object sender, MouseEventArgs e)
		{
			btnSoundEffects.Opacity = 0.5;
		}

		private void btnSoundEffects_MouseEnter(object sender, MouseEventArgs e)
		{
			btnSoundEffects.Opacity = 1;
		}

		private void changeSize(Canvas c, double left, double top, int width, int height)
		{
			Thickness margin = c.Margin;
			margin.Left = left;
			margin.Top = top;
			c.Margin = margin;
			c.Width = width;
			c.Height = height;
		}

		private void BackgroundPrev2_MouseEnter(object sender, MouseEventArgs e)
		{
			changeSize(BackgroundPrev2, 18, 9.5, 39, 24);
		}

		private void BackgroundPrev2_MouseLeave(object sender, MouseEventArgs e)
		{
			changeSize(BackgroundPrev2, 20, 7.5, 35, 20);
		}

		private void BackgroundPrev_MouseEnter(object sender, MouseEventArgs e)
		{
			changeSize(BackgroundPrev, 58, 2.5, 44, 34);
		}

		private void BackgroundPrev_MouseLeave(object sender, MouseEventArgs e)
		{
			changeSize(BackgroundPrev, 60, 0.5, 40, 30);
		}

		private void BackgroundActive_MouseEnter(object sender, MouseEventArgs e)
		{
			changeSize(BackgroundActive, -2, 2, 84, 44);
		}

		private void BackgroundActive_MouseLeave(object sender, MouseEventArgs e)
		{
			changeSize(BackgroundActive, 0, 0, 80, 40);
		}

		private void BackgroundNext_MouseEnter(object sender, MouseEventArgs e)
		{
			changeSize(BackgroundNext, -2, 62.5, 44, 34);
		}

		private void BackgroundNext_MouseLeave(object sender, MouseEventArgs e)
		{
			changeSize(BackgroundNext, 0, 60.5, 40, 30);
		}

		private void BackgroundNext2_MouseEnter(object sender, MouseEventArgs e)
		{
			changeSize(BackgroundNext2, -2, 9.5, 39, 24);
		}

		private void BackgroundNext2_MouseLeave(object sender, MouseEventArgs e)
		{
			changeSize(BackgroundNext2, 0, 7.5, 35, 20);
		}

	}
}
