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
        private int _size = 0;
        public Boolean _isClicked = false, _isGlowing = false,
                       _isHovered = false;
        private COLOR _brickColor;
        private STATE _state;
        private ImageBrush _background = new ImageBrush();
        private ImageBrush[] whiteBrick = new ImageBrush[3], 
                             blackBrick = new ImageBrush[3];

        public Triangle()
        {
            InitializeComponent();
        }
        private void init() {
            if (_state == STATE.UPPER)
                _background.ImageSource = new BitmapImage(new Uri("../../Resources/isClickedUpper.png", UriKind.Relative));
            else
                _background.ImageSource = new BitmapImage(new Uri("../../Resources/isClickedLower.png", UriKind.Relative));
            background.Background = _background;
            Update();
        }

        public void Update() {
            try
            {
                if (_isGlowing)
                    background.Opacity = 1;
                else
                    background.Opacity = 0;
            }
            catch (Exception ex) { Console.WriteLine("renderUpdate: " + ex.Message); }
        }

        public void AddBrick() {
            if (_size < 15)
                _size++;
        }
        public void RemoveBrick() {
            if (_size >= 0)
                _size--;
        }

        private void renderBricks() {

        }

        private void triangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_isGlowing)
                _isGlowing = false;
            else
                _isGlowing = true;

            _isClicked = true;
            Update();
        }
        private void triangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isClicked = false;
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
