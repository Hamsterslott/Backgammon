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

        private COLOR _color;
        private ImageBrush[] brick = new ImageBrush[2];
        private int _size = 0;

        public BrickHolder()
        {
            InitializeComponent();

            brick[0] = new ImageBrush();
            brick[1] = new ImageBrush();
            brick[0].ImageSource = new BitmapImage(new Uri("../../Resources/vitKnappLigga.png", UriKind.Relative));
            brick[1].ImageSource = new BitmapImage(new Uri("../../Resources/svartKnappLigga.png", UriKind.Relative));
        }

         private void update() {
            int theme;
            if (_color == COLOR.WHITE)
                theme = 0;
            else
                theme = 1;

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
        public void addBrick() {
            _size++;
            if (_size >= 15)
                _size = 15;
            update();
        }
        public void removeBrick() {
            _size--;
            if (_size <= 0)
                _size = 0;
            update();
        }

        // GETTERS AND SETTERS //
        public void setColor(COLOR color) {
            _color = color;
            update();
        }
        public void setSize(int size) {
            _size = size;
            update();
        }
        // GETTERS AND SETTERS END //
    }
}
