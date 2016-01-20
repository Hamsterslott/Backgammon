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

    public partial class BrickHolder : UserControl
    {

		private MainWindow _mainWindow = null;
        private player _color;
        private ImageBrush[] brick = new ImageBrush[2];
        private int _size = 0;
		private int _pos;

        public BrickHolder()
        {
            InitializeComponent();
			try {
            brick[0] = new ImageBrush();
            brick[1] = new ImageBrush();
            brick[0].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/vitKnappLigga.png"));
            brick[1].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/svartKnappLigga.png"));
			}
			catch (Exception) {}
        }

         private void update() {
            int theme;
            if (_color == player.one)
                theme = 1;
            else
                theme = 0;

            for (int i = 0; i < _size; i++) {
                Image image = brickor.Children[14 - i] as Image;
                image.Source = brick[theme].ImageSource;    
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
            update();
        }

        public void setPos(int pos) {
            this._pos = pos;
        }

		
        // GETTERS AND SETTERS END //
    }
}
