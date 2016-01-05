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

                if (_state == STATE.UPPER)
                    renderBricksUpper(_brickColor);
                else
                    renderBricksLower(_brickColor);
                
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

        private void renderBricksLower(COLOR color) {
            int theme;
            if (color == COLOR.WHITE)
                theme = 0;
            else
                theme = 1;

            // Brickplats fem
            if (_size > 0 && _size < 6)
                brickSpaceFive.Background = singleBrick[theme];
            else if (_size >= 6 && _size < 11)
                brickSpaceFive.Background = doubleBrick[theme];
            else if (_size >= 11)
                brickSpaceFive.Background = tripleBrick[theme];
            else
                brickSpaceFive.Background = null;
            // Brickplats fyra
            if (_size > 1 && _size < 7)
                brickSpaceFour.Background = singleBrick[theme];
            else if (_size >= 7 && _size < 12)
                brickSpaceFour.Background = doubleBrick[theme];
            else if (_size >= 12)
                brickSpaceFour.Background = tripleBrick[theme];
            else
                brickSpaceFour.Background = null;
            // Brickplats tre
            if (_size > 2 && _size < 8)
                brickSpaceThree.Background = singleBrick[theme];
            else if (_size >= 8 && _size < 13)
                brickSpaceThree.Background = doubleBrick[theme];
            else if (_size >= 13)
                brickSpaceThree.Background = tripleBrick[theme];
            else
                brickSpaceThree.Background = null;
            // Brickplats två
            if (_size > 3 && _size < 9)
                brickSpaceTwo.Background = singleBrick[theme];
            else if (_size >= 9 && _size < 14)
                brickSpaceTwo.Background = doubleBrick[theme];
            else if (_size >= 14)
                brickSpaceTwo.Background = tripleBrick[theme];
            else
                brickSpaceTwo.Background = null;
            // Brickplats ett
            if (_size > 4 && _size < 10)
                brickSpaceOne.Background = singleBrick[theme];
            else if (_size >= 10 && _size < 15)
                brickSpaceOne.Background = doubleBrick[theme];
            else if (_size >= 15)
                brickSpaceOne.Background = tripleBrick[theme];
            else
                brickSpaceOne.Background = null;  
        }

        private void renderBricksUpper(COLOR color) {
            int theme;
            if (color == COLOR.WHITE)
                theme = 0;
            else
                theme = 1;
            // Brickplats ett
            if (_size > 0 && _size < 6)
                brickSpaceOne.Background = singleBrick[theme];
            else if (_size >= 6 && _size < 11)
                brickSpaceOne.Background = doubleBrick[theme];
            else if (_size >= 11)
                brickSpaceOne.Background = tripleBrick[theme];
            else
                brickSpaceOne.Background = null;
            // Brickplats två
            if (_size > 1 && _size < 7)
                brickSpaceTwo.Background = singleBrick[theme];
            else if (_size >= 7 && _size < 12)
                brickSpaceTwo.Background = doubleBrick[theme];
            else if (_size >= 12)
                brickSpaceTwo.Background = tripleBrick[theme];
            else
                brickSpaceTwo.Background = null;
            // Brickplats tre
            if (_size > 2 && _size < 8)
                brickSpaceThree.Background = singleBrick[theme];
            else if (_size >= 8 && _size < 13)
                brickSpaceThree.Background = doubleBrick[theme];
            else if (_size >= 13)
                brickSpaceThree.Background = tripleBrick[theme];
            else
                brickSpaceThree.Background = null;
            // Brickplats fyra
            if (_size > 3 && _size < 9)
                brickSpaceFour.Background = singleBrick[theme];
            else if (_size >= 9 && _size < 14)
                brickSpaceFour.Background = doubleBrick[theme];
            else if (_size >= 14)
                brickSpaceFour.Background = tripleBrick[theme];
            else
                brickSpaceFour.Background = null;
            // Brickplats fem
            if (_size > 4 && _size < 10)
                brickSpaceFive.Background = singleBrick[theme];
            else if (_size >= 10 && _size < 15)
                brickSpaceFive.Background = doubleBrick[theme];
            else if (_size >= 15)
                brickSpaceFive.Background = tripleBrick[theme];
            else
                brickSpaceFive.Background = null;
        }

        private void triangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(pos.ToString());
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
            Update();
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
            Update();
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
