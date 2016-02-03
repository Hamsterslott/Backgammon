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

namespace Backgammon
{

    public partial class BrickHolder : UserControl
    {

		private MainWindow _mainWindow = null;
        private player _color;
        private int _size = 0;
		private int _pos;

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

         private void brickHolder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
         {
			 if(_mainWindow.pickedUp != 0) _mainWindow.playGame(new Triangle(_pos));
         }

		private void brickHolder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			 if(_mainWindow.pickedUp != 0) _mainWindow.playGame(new Triangle(_pos));
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

        public void setPos(int pos) {
            this._pos = pos;
        }

		
        // GETTERS AND SETTERS END //
    }
}
