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
    public enum COLOR
    {
        BLACK, WHITE
    };
    public enum STATE {
        UPPER, LOWER
    };

    public partial class Triangle : UserControl
    {
        private int _size = 0;
        public Boolean _isClicked = false, _isGlowing = false,
                       _isHovered = false;
        private COLOR _brickColor;
        private STATE _state;
        private ImageBrush _background = new ImageBrush();
        private ImageBrush[] whiteBrick = new ImageBrush[3], 
                             blackBrick = new ImageBrush[3],
                             singleBrick = new ImageBrush[2], doubleBrick = new ImageBrush[2], tripleBrick = new ImageBrush[2];

        public Triangle()
        {
            InitializeComponent();

            try {
                // Behövde göras
                for (int i = 0; i < 3; i++) {
                    whiteBrick[i] = new ImageBrush();
                    blackBrick[i] = new ImageBrush();
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
            // TEMP TESTAR //
                _size = 0;
                _brickColor = COLOR.BLACK;
            // TEMP TESTAR END //

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
                    renderBricksUpper();
                else
                    renderBricksLower();
                
                if (_isGlowing)
                    background.Opacity = 1;
                else
                    background.Opacity = 0;
            }
            catch (Exception ex) { Console.WriteLine("renderUpdate: " + ex.Message); }
        }

        public void SetSize(int size) {
            _size = size;
            Update();
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

        private void renderBricksLower() {
            if (_brickColor == COLOR.WHITE)
            {
                // Brickplats fem
                if (_size > 0 && _size < 6)
                    brickSpaceFive.Background = whiteBrick[0];
                else if (_size >= 6 && _size < 11)
                    brickSpaceFive.Background = whiteBrick[1];
                else if (_size >= 11)
                    brickSpaceFive.Background = whiteBrick[2];
                else
                    brickSpaceFive.Background = null;
                // Brickplats fyra
                if (_size > 1 && _size < 7)
                    brickSpaceFour.Background = whiteBrick[0];
                else if (_size >= 7 && _size < 12)
                    brickSpaceFour.Background = whiteBrick[1];
                else if (_size >= 12)
                    brickSpaceFour.Background = whiteBrick[2];
                else
                    brickSpaceFour.Background = null;
                // Brickplats tre
                if (_size > 2 && _size < 8)
                    brickSpaceThree.Background = whiteBrick[0];
                else if (_size >= 8 && _size < 13)
                    brickSpaceThree.Background = whiteBrick[1];
                else if (_size >= 13)
                    brickSpaceThree.Background = whiteBrick[2];
                else
                    brickSpaceThree.Background = null;
                // Brickplats två
                if (_size > 3 && _size < 9)
                    brickSpaceTwo.Background = whiteBrick[0];
                else if (_size >= 9 && _size < 14)
                    brickSpaceTwo.Background = whiteBrick[1];
                else if (_size >= 14)
                    brickSpaceTwo.Background = whiteBrick[2];
                else
                    brickSpaceTwo.Background = null;
                // Brickplats ett
                if (_size > 4 && _size < 10)
                    brickSpaceOne.Background = whiteBrick[0];
                else if (_size >= 10 && _size < 15)
                    brickSpaceOne.Background = whiteBrick[1];
                else if (_size >= 15)
                    brickSpaceOne.Background = whiteBrick[2];
                else
                    brickSpaceOne.Background = null;
            }
            else
            {
                // Brickplats fem
                if (_size > 0 && _size < 6)
                    brickSpaceFive.Background = blackBrick[0];
                else if (_size >= 6 && _size < 11)
                    brickSpaceFive.Background = blackBrick[1];
                else if (_size >= 11)
                    brickSpaceFive.Background = blackBrick[2];
                else
                    brickSpaceFive.Background = null;
                // Brickplats fyra
                if (_size > 1 && _size < 7)
                    brickSpaceFour.Background = blackBrick[0];
                else if (_size >= 7 && _size < 12)
                    brickSpaceFour.Background = blackBrick[1];
                else if (_size >= 12)
                    brickSpaceFour.Background = blackBrick[2];
                else
                    brickSpaceFour.Background = null;
                // Brickplats tre
                if (_size > 2 && _size < 8)
                    brickSpaceThree.Background = blackBrick[0];
                else if (_size >= 8 && _size < 13)
                    brickSpaceThree.Background = blackBrick[1];
                else if (_size >= 13)
                    brickSpaceThree.Background = blackBrick[2];
                else
                    brickSpaceThree.Background = null;
                // Brickplats två
                if (_size > 3 && _size < 9)
                    brickSpaceTwo.Background = blackBrick[0];
                else if (_size >= 9 && _size < 14)
                    brickSpaceTwo.Background = blackBrick[1];
                else if (_size >= 14)
                    brickSpaceTwo.Background = blackBrick[2];
                else
                    brickSpaceTwo.Background = null;
                // Brickplats ett
                if (_size > 4 && _size < 10)
                    brickSpaceOne.Background = blackBrick[0];
                else if (_size >= 10 && _size < 15)
                    brickSpaceOne.Background = blackBrick[1];
                else if (_size >= 15)
                    brickSpaceOne.Background = blackBrick[2];
                else
                    brickSpaceOne.Background = null;
            }
        }

        private void renderBricksUpper() {
            if (_brickColor == COLOR.WHITE)
            {
                // Brickplats ett
                if (_size > 0 && _size < 6)
                    brickSpaceOne.Background = whiteBrick[0];
                else if (_size >= 6 && _size < 11)
                    brickSpaceOne.Background = whiteBrick[1];
                else if (_size >= 11)
                    brickSpaceOne.Background = whiteBrick[2];
                else
                    brickSpaceOne.Background = null;
                // Brickplats två
                if (_size > 1 && _size < 7)
                    brickSpaceTwo.Background = whiteBrick[0];
                else if (_size >= 7 && _size < 12)
                    brickSpaceTwo.Background = whiteBrick[1];
                else if (_size >= 12)
                    brickSpaceTwo.Background = whiteBrick[2];
                else
                    brickSpaceTwo.Background = null;
                // Brickplats tre
                if (_size > 2 && _size < 8)
                    brickSpaceThree.Background = whiteBrick[0];
                else if (_size >= 8 && _size < 13)
                    brickSpaceThree.Background = whiteBrick[1];
                else if (_size >= 13)
                    brickSpaceThree.Background = whiteBrick[2];
                else
                    brickSpaceThree.Background = null;
                // Brickplats fyra
                if (_size > 3 && _size < 9)
                    brickSpaceFour.Background = whiteBrick[0];
                else if (_size >= 9 && _size < 14)
                    brickSpaceFour.Background = whiteBrick[1];
                else if (_size >= 14)
                    brickSpaceFour.Background = whiteBrick[2];
                else
                    brickSpaceFour.Background = null;
                // Brickplats fem
                if (_size > 4 && _size < 10)
                    brickSpaceFive.Background = whiteBrick[0];
                else if (_size >= 10 && _size < 15)
                    brickSpaceFive.Background = whiteBrick[1];
                else if (_size >= 15)
                    brickSpaceFive.Background = whiteBrick[2];
                else
                    brickSpaceFive.Background = null;
            }
            else
            {
                // Brickplats ett
                if (_size > 0 && _size < 6)
                    brickSpaceOne.Background = blackBrick[0];
                else if (_size >= 6 && _size < 11)
                    brickSpaceOne.Background = blackBrick[1];
                else if (_size >= 11)
                    brickSpaceOne.Background = blackBrick[2];
                else
                    brickSpaceOne.Background = null;
                // Brickplats två
                if (_size > 1 && _size < 7)
                    brickSpaceTwo.Background = blackBrick[0];
                else if (_size >= 7 && _size < 12)
                    brickSpaceTwo.Background = blackBrick[1];
                else if (_size >= 12)
                    brickSpaceTwo.Background = blackBrick[2];
                else
                    brickSpaceTwo.Background = null;
                // Brickplats tre
                if (_size > 2 && _size < 8)
                    brickSpaceThree.Background = blackBrick[0];
                else if (_size >= 8 && _size < 13)
                    brickSpaceThree.Background = blackBrick[1];
                else if (_size >= 13)
                    brickSpaceThree.Background = blackBrick[2];
                else
                    brickSpaceThree.Background = null;
                // Brickplats fyra
                if (_size > 3 && _size < 9)
                    brickSpaceFour.Background = blackBrick[0];
                else if (_size >= 9 && _size < 14)
                    brickSpaceFour.Background = blackBrick[1];
                else if (_size >= 14)
                    brickSpaceFour.Background = blackBrick[2];
                else
                    brickSpaceFour.Background = null;
                // Brickplats fem
                if (_size > 4 && _size < 10)
                    brickSpaceFive.Background = blackBrick[0];
                else if (_size >= 10 && _size < 15)
                    brickSpaceFive.Background = blackBrick[1];
                else if (_size >= 15)
                    brickSpaceFive.Background = blackBrick[2];
                else
                    brickSpaceFive.Background = null;
            }
        }

        private void triangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddBrick();
            _isClicked = true;
        }
        private void triangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isClicked = false;
        }

        private void triangle_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            RemoveBrick();
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
        public int getSize() {
            return _size;
        }
        public void setSize(int size) {
            this._size = size;
            Update();
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
