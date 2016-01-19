﻿using System;
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
        public Boolean _isGlowing = false, _isHovered = false; 
        private player _brickColor;
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
				
                singleBrick[0].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/whiteChip1.png"));
                doubleBrick[0].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/whiteChip2.png"));
                tripleBrick[0].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/whiteChip3.png"));

                singleBrick[1].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/darkChip1.png"));
                doubleBrick[1].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/darkChip2.png"));
                tripleBrick[1].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/darkChip3.png"));
            }
            catch (Exception ex) { Console.WriteLine("brickInit: " + ex.Message); }
            
        }
        private void init() {
            _size = 0;

            if (_state == STATE.UPPER)
                _background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/isClickedUpper.png"));
            else
                _background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/isClickedLower.png"));
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


        private void renderBricks() {
            int theme;
			Canvas [] Brickor;
            if (_brickColor == player.one) theme = 1;
            else theme = 0;
                
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
            _mainWindow.playGame((Triangle)sender);
        }
        private void triangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
			if((Triangle)sender != _mainWindow.selectedTriangles[0] && _mainWindow.pickedUp != 0) _mainWindow.playGame((Triangle)sender);
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
            init();
        }
        public bool getGlowing() {
            return _isGlowing;
        }
        public void setGlowing(bool glow) {
            _isGlowing = false; //temporär fix för att få bort glow.
        }
        // GETTERS OCH SETTERS SLUT //
    }
}
