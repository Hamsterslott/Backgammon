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
using System.Windows.Media.Animation;

namespace Backgammon
{
    /// <summary>
    /// Interaction logic for Triangle.xaml
    /// </summary>

    public enum STATE {
        UPPER, LOWER
    };

    public partial class Triangle : UserControl
    {
        private MainWindow _mainWindow = null;
        private int _size = 0;
		private int _pos;
		private player _brickColor;
        private STATE _state;


        public Triangle()
        {
            InitializeComponent();
        }

		public Triangle(int pos)
		{
			InitializeComponent();
			_pos = pos;
		}


        public void Update() {
            try {
                renderBricks();
            }
            catch (Exception ex) { Console.WriteLine("renderUpdate: " + ex.Message); }
        }

		public void wrongMove()
		{
			try {
			if (_state == STATE.UPPER)
                background.Background = _mainWindow._triangelIsClicked[2];
            else
                background.Background = _mainWindow._triangelIsClicked[3];

			DoubleAnimation animation = new DoubleAnimation(0.75, 0, TimeSpan.FromSeconds(0.5));
            this.background.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, animation);
			}
			catch {Console.WriteLine("exception i triangel wrongMove()");};
		}

        public void possibleMove(int state)
        {
            try {
                if (_state == STATE.UPPER)
                    background.Background = _mainWindow._triangelIsClicked[0];
                else
                    background.Background = _mainWindow._triangelIsClicked[1];

                if (state == 0)
                {
                    DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
                    this.background.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, animation); 
                }
                else if (state == 1) {
                    DoubleAnimation animation = new DoubleAnimation(this.background.Opacity, 0, TimeSpan.FromSeconds(1));
                    this.background.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, animation); 
                }
				else ;

                
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace.ToString()); }
        }

        public Triangle[] availableMoves(Triangle clickedTriangle, int[] dice)
        {
            Triangle[] availablemoves = new Triangle[4];
            availablemoves[0] = _mainWindow.getTriangle(_mainWindow._model.correctPos(clickedTriangle.getPos()) + dice[0]);
            availablemoves[1] = _mainWindow.getTriangle(_mainWindow._model.correctPos(clickedTriangle.getPos()) + dice[1]);

            return availablemoves;
        }

        private void renderBricks() {

			if(_pos> 0 && _pos < 27)
			{ 
			Canvas [] Brickor;
			if(_state == STATE.UPPER) Brickor = new Canvas[]{brickSpaceOne,brickSpaceTwo,brickSpaceThree,brickSpaceFour,brickSpaceFive};
			else Brickor = new Canvas[]{brickSpaceFive,brickSpaceFour,brickSpaceThree,brickSpaceTwo,brickSpaceOne};

			for (int i = 0; i < Brickor.Length; i++)
			{
				if(_size == i) 
					{
					for(int j = i; j < Brickor.Length; j++) Brickor[j].Background = null;
					break;
					}

				else if (_size > i && _size <= 5+i) Brickor[i].Background = _mainWindow.singleBrick[Settings.playerTheme[(int)_brickColor]];
				
				else if (_size > 5+i && _size <= 10+i) Brickor[i].Background = _mainWindow.doubleBrick[Settings.playerTheme[(int)_brickColor]];
                
				else if (_size > 10+i) Brickor[i].Background = _mainWindow.tripleBrick[Settings.playerTheme[(int)_brickColor]];
                
			}
			}
		}

        private void triangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.playGame((Triangle)sender);
        }

        private void triangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
			if((Triangle)sender != _mainWindow.selectedTriangles[0] && _mainWindow.pickedUp != 0) _mainWindow.playGame((Triangle)sender);
        }


        // GETTERS OCH SETTERS START //
        public void setLink(MainWindow mw) {
            _mainWindow = mw;
        }
        public int getSize() {
            return _size;
        }
        public void setSize(int size) {
            this._size = size;
        }

		public int getPos() {
            return _pos;
        }
        public void setPos(int pos) {
            _pos = pos;
        }


        public player getColor() {
            return _brickColor;
        }
        public void setColor(player color) {
            this._brickColor = color;
        }
        public STATE getState() {
            return _state;
        }
        public void setState(STATE state) {
            _state = state;
        }
        // GETTERS OCH SETTERS SLUT //
    }
}
