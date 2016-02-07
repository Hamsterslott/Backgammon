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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Backgammon
{

    public partial class BrickHolder : UserControl
    {
        private WinScreen winScreen = null;
		private MainWindow _mainWindow = null;
        private player _color;
        private int _size = 0;
		private int _pos = 0;

        public BrickHolder()
        {
            InitializeComponent();
        }

         internal void update() {

            for (int i = 0; i < _size; i++) {
                Image image = brickor.Children[14 - i] as Image;
                image.Source = _mainWindow.brick[Settings.playerTheme[(int)_color]].ImageSource;    
            }
            if (_size < 15) {
                for (int i = _size; i < 15; i++) {
                    Image image = brickor.Children[14 - i] as Image;
                    image.Source = null;
                }
            }
        }

		public void wrongMove()
		{
            //Röd
			background.Background = _mainWindow._goalIsClicked[1];  
			DoubleAnimation animation = new DoubleAnimation(0.75, 0, TimeSpan.FromSeconds(0.5));
            this.background.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, animation);
		}

		public void possibleMove(int state)
        {
            //Grön            
			background.Background = _mainWindow._goalIsClicked[0];           
                if (state == 0)
                {
                    DoubleAnimation animation = new DoubleAnimation(0.15, 0.75, TimeSpan.FromSeconds(1));
                    this.background.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, animation); 
                }
                else {
                    DoubleAnimation animation = new DoubleAnimation(this.background.Opacity, 0, TimeSpan.FromSeconds(0.2));
                    this.background.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, animation); 
                }
        }


         private void brickHolder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
         {
			 if(_mainWindow.pickedUp != 0) _mainWindow.playGame(new Triangle(_pos));
         }

		private void brickHolder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			 if(_mainWindow.pickedUp != 0) _mainWindow.playGame(new Triangle(_pos));
		}

		public void addOne()
		{
			_size++;
			if (_size >= 15)
            {
                DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(2));
                fadeOut.Completed += new EventHandler(fadeOut_Completed);
                _mainWindow.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, fadeOut);
            }
            update();
		}
        private void fadeOut_Completed(Object sender, EventArgs e) {
            DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
            fadeIn.Completed += new EventHandler(fadeIn_Completed);
            winScreen = new WinScreen();
            if (_color == 0)
                winScreen.setWinner("PLAYER 1");
            else
                winScreen.setWinner("PLAYER 2");
            winScreen.setLink(_mainWindow);
            winScreen.Show();
            winScreen.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, fadeIn);
        }
        private void fadeIn_Completed(Object sender, EventArgs e) {
            Settings.isSongPlaying = false;
            _mainWindow.song.Stop();
            _mainWindow.Close();
        }

        // GETTERS AND SETTERS //
		public void setLink(MainWindow mw) {
            _mainWindow = mw;
        }

        public void setColor(player color) {
            _color = color;
        }
        public void setSize(int size) {
            _size = size;
            if (_size >= 15)
            {
                WinScreen winScreen = new WinScreen();
                if(_color == 0)
                    winScreen.setWinner("PLAYER 1");
                else
                    winScreen.setWinner("PLAYER 2");
                winScreen.setLink(_mainWindow);
                winScreen.Show();
                _mainWindow.Close();
            }
            update();
        }
		
        // GETTERS AND SETTERS END //
    }
}
