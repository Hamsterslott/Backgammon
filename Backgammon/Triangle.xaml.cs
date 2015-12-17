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
    public partial class Triangle : UserControl
    {
        private int _size = 0;
        public Boolean _isClicked = false, _isGlowing = false,
                       _isHovered = false;
        private COLOR _brickColor;
        private COLOR _triangleColor;
        private ImageBrush _lightBackground, _darkBackground, _glowing;
        private ImageBrush[] whiteBrick = new ImageBrush[3], 
                             blackBrick = new ImageBrush[3];

        public Triangle()
        {
            InitializeComponent();
            init();
        }

        private void init() {
            _triangleColor = COLOR.LIGHT;

            // TODO: Fixa länkar till bilder
            /*  
              _lightBackground = new ImageBrush(new BitmapImage(new Uri(@"", UriKind.Relative)));
              _darkBackground = new ImageBrush(new BitmapImage(new Uri(@"", UriKind.Relative))); 
               
              whiteBrick[0]..
              whiteBrick[1]..
              osv...
             */

            Update();
        }

        public void Update() {
            try
            {
                if (_triangleColor == COLOR.LIGHT)
                    this.Background = _lightBackground;
                
                else 
                    this.Background = _darkBackground;
                
                // TODO: Visa glow bild ifall _isGlowing
                if (_isGlowing) { MessageBox.Show("test"); }

                renderBricks();

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
            MessageBox.Show("Test");
            _isClicked = true;
        }
        private void triangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isClicked = false;
        }

        private void triangle_MouseEnter(object sender, MouseEventArgs e)
        {
            theGrid.Opacity = 0.2;
            _isHovered = true;
        }

        private void triangle_MouseLeave(object sender, MouseEventArgs e)
        {
            theGrid.Opacity = 0.01;
            _isHovered = false;
        }

        // GETTERS OCH SETTERS START //
        public int getSize() {
            return _size;
        }
        public void setSize(int size) {
            this._size = size;
        }
        public COLOR getColor() {
            return _brickColor;
        }
        public void setColor(COLOR color) {
            this._brickColor = color;
        }
        public COLOR getTriangleColor() {
            return _triangleColor;
        }
        public void setTriangleColor(COLOR color) {
            this._triangleColor = color;
        }
        // GETTERS OCH SETTERS SLUT //
    }
}
