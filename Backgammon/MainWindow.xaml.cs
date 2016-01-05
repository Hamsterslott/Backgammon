﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

    public partial class MainWindow : Window
    {

        private Random rand = new Random();
        private int[] dices = new int[4];

        public MainWindow()
        {
            InitializeComponent();

            // Initierar allt vid start
            dices = rollDices();

            // Grid one
            foreach (Triangle t in gridOne.Children)
                t.setState(STATE.UPPER);
             
            // Grid two
            foreach (Triangle t in gridTwo.Children) 
                t.setState(STATE.UPPER);
             
            // Grid three
            foreach (Triangle t in gridThree.Children) 
                t.setState(STATE.LOWER);
             
            // Grid four
            foreach (Triangle t in gridFour.Children) 
                t.setState(STATE.LOWER);

            //Placerar ut brickor uppe till vänster först, uppe höger sen, nere höger, nere vänster.
			//getTriangle(1, 0).setColor(COLOR.BLACK);
			//getTriangle(1, 0).setSize(2);

			//getTriangle(1, 5).setColor(COLOR.WHITE);
			//getTriangle(1, 5).setSize(5);

			//getTriangle(2, 1).setColor(COLOR.WHITE);
			//getTriangle(2, 1).setSize(3);

			//getTriangle(2, 5).setColor(COLOR.BLACK);
			//getTriangle(2, 5).setSize(5);

			//getTriangle(3, 5).setColor(COLOR.WHITE);
			//getTriangle(3, 5).setSize(5);

			//getTriangle(3, 1).setColor(COLOR.BLACK);
			//getTriangle(3, 1).setSize(3);

			//getTriangle(4, 5).setColor(COLOR.BLACK);
			//getTriangle(4, 5).setSize(5);

<<<<<<< HEAD
 
=======
			//getTriangle(4, 0).setColor(COLOR.WHITE);
			//getTriangle(4, 0).setSize(2);


			for(int i = 1; i < 25; i++)
				{
				getTriangle(i).setColor(COLOR.BLACK);
				getTriangle(i).setSize(2);
				}
            
>>>>>>> origin/master
            update();
        }

        private int[] rollDices()
        {
            int[] d = new int[4];
            d[2] = d[3] = 0;
            d[0] = rand.Next(1, 7);
            d[1] = rand.Next(1, 7);
            if (d[0] == d[1]) d[3] = d[2] = d[1];

            if (d[3] == 0) return (new int[2] { d[0], d[1] });
            return d;
        }

        private void update() {
            // TODO:    
            updateScale();
        }

        // Håller en 16:9 ratio på spelplanen
		private void updateScale()
		{
			spelPlan.Height = Width / 1.77;
            if (spelPlan.Height >= duk.Height)
                spelPlan.Height = duk.Height;

			spelPlan.Width = spelPlan.Height*(1.77);
		}


		//den ska gå till 26 sen.
        private Triangle getTriangle(int pos) {
            if (pos < 0 ||pos > 24)
                return null;

            Triangle triangle;
            if (pos < 7) { return triangle = gridOne.Children[pos-1] as Triangle; }
            else if (pos < 13) { return triangle = gridTwo.Children[pos-(6+1)] as Triangle; }
            else if (pos < 19) { return triangle = gridThree.Children[pos-(12+1)] as Triangle; }
            else { return triangle = gridFour.Children[pos-(18+1)] as Triangle; }

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try { DragMove(); }
            catch(Exception ex){}  
        }
        
        private void maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            updateScale();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {

        }

    }
}
