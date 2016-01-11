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
		private int pos;
        public Boolean _isClicked = false, _isGlowing = false,
                       _isHovered = false;
        private COLOR _brickColor;
        private STATE _state;
        private ImageBrush _background = new ImageBrush();
        private ImageBrush[] singleBrick = new ImageBrush[2], doubleBrick = new ImageBrush[2], tripleBrick = new ImageBrush[2];

        public Triangle()
        {
            InitializeComponent();

            try {
                // Behövde göras
				
                for (int i = 0; i < singleBrick.Length; i++) {
                    singleBrick[i] = new ImageBrush();
                    doubleBrick[i] = new ImageBrush();
                    tripleBrick[i] = new ImageBrush();
                }
				
                singleBrick[0].ImageSource = new BitmapImage(new Uri("../../Resources/whiteChip1.png", UriKind.Relative));
                doubleBrick[0].ImageSource = new BitmapImage(new Uri("../../Resources/whiteChip2.png", UriKind.Relative));
                tripleBrick[0].ImageSource = new BitmapImage(new Uri("../../Resources/whiteChip3.png", UriKind.Relative));

                singleBrick[1].ImageSource = new BitmapImage(new Uri("../../Resources/darkChip1.png", UriKind.Relative));
                doubleBrick[1].ImageSource = new BitmapImage(new Uri("../../Resources/darkChip2.png", UriKind.Relative));
                tripleBrick[1].ImageSource = new BitmapImage(new Uri("../../Resources/darkChip3.png", UriKind.Relative));
            }
            catch (Exception ex) { Console.WriteLine("brickInit: " + ex.Message); }
            
        }
        private void init() {
            _size = 0;

            if (_state == STATE.UPPER)
                _background.ImageSource = new BitmapImage(new Uri("../../Resources/isClickedUpper.png", UriKind.Relative));
            else
                _background.ImageSource = new BitmapImage(new Uri("../../Resources/isClickedLower.png", UriKind.Relative));
            background.Background = _background;
            Update();
        }

        public void Update() {
            try {
                renderBricks();
                
                if (_isGlowing)
                    background.Opacity = 1;
                else
                    background.Opacity = 0;
            }
            catch (Exception ex) { Console.WriteLine("renderUpdate: " + ex.Message); }
        }

        public void AddBrick() {
            _size++;
            if (_size > 15)
                _size = 15;

            Update();
        }
        public void RemoveBrick() {
            _size--;
            if (_size <= 0)
                _size = 0;

            Update();
        }

        private void renderBricks() {
            int theme;
			Canvas [] Brickor;
            if (_brickColor == COLOR.WHITE) theme = 0;
            else theme = 1;
                
			if(_state == STATE.UPPER) Brickor = new Canvas[]{brickSpaceOne,brickSpaceTwo,brickSpaceThree,brickSpaceFour,brickSpaceFive};
			else Brickor = new Canvas[]{brickSpaceFive,brickSpaceFour,brickSpaceThree,brickSpaceTwo,brickSpaceOne};

			for (int i = 0; i < Brickor.Length; i++)
			{
				if(_size == i) 
					{
					for(int j = i; j < Brickor.Length; j++) Brickor[j].Background = null;
					break;
					}

				else if (_size > i && _size <= 5+i) Brickor[i].Background = singleBrick[theme];
				
				else if (_size > 5+i && _size <= 10+i) Brickor[i].Background = doubleBrick[theme];
                
				else if (_size > 10+i) Brickor[i].Background = tripleBrick[theme];
                
			}

		}

        private void triangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.selectTriangle((Triangle)sender);
            _isClicked = true;
        }
        private void triangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isClicked = false;
        }

        private void triangle_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void triangle_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void triangle_MouseEnter(object sender, MouseEventArgs e)
        {
            _isHovered = true;
        }

        private void triangle_MouseLeave(object sender, MouseEventArgs e)
        {
            _isHovered = false;
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
            return pos;
        }
        public void setPos(int pos) {
            this.pos = pos;
        }


        public COLOR getColor() {
            return _brickColor;
        }
        public void setColor(COLOR color) {
            this._brickColor = color;
        }
        public STATE getState() {
            return _state;
        }
        public void setState(STATE state) {
            _state = state;
            init();
        }
        public bool getGlowing() {
            return _isGlowing;
        }
        public void setGlowing(bool glow) {
            _isGlowing = glow;
            Update();
        }
        // GETTERS OCH SETTERS SLUT //
    }
}
