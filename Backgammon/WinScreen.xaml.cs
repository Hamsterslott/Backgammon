using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
	public partial class WinScreen : Window
	{

		public MainWindow mainWindow;
		private bool spaceUsed = false;
		private SoundPlayer victory = new SoundPlayer(Properties.Resources.victory);

		public WinScreen()
		{
			InitializeComponent();
			if (Settings.playSound)
				victory.Play();
		}

		public void setLink(MainWindow mw)
		{
			mainWindow = mw;
			this.Width = mw.Width;
			this.Height = mw.Height;
			this.Left = mw.Left;
			this.Top = mw.Top;
			if (mainWindow.WindowState == System.Windows.WindowState.Maximized)
				this.WindowState = System.Windows.WindowState.Maximized;
		}

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try { DragMove(); }
			catch (Exception) { }
		}

		private void maximize_Click(object sender, RoutedEventArgs e)
		{
			if (WindowState == WindowState.Maximized)
			{
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
			if (e.Key == Key.Space && !spaceUsed)
			{
				DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(2));
				fadeOut.Completed += new EventHandler(fadeOut_Completed);
				this.WinningScreenCanvas.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, fadeOut);
				spaceUsed = true;
			}
		}

		private void fadeOut_Completed(Object sender, EventArgs e)
		{
			victory.Stop();
			mainWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
			mainWindow.Width = this.Width;
			mainWindow.Height = this.Height;
			mainWindow.Left = this.Left;
			mainWindow.Top = this.Top;
			if (this.WindowState == System.Windows.WindowState.Maximized)
				mainWindow.WindowState = System.Windows.WindowState.Maximized;
			mainWindow.Sidebar.NewGameImg_MouseLeftButtonDown(new object(), new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left));
			mainWindow.Visibility = System.Windows.Visibility.Visible;
			DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
			fadeIn.Completed += new EventHandler(fadeIn_Completed);
			mainWindow.spelPlan.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, fadeIn);
		}

		private void fadeIn_Completed(Object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
