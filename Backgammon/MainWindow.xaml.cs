using System;
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

    public enum COLOR
    {
        BLACK, WHITE, LIGHT, DARK
    };

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

        private Triangle getTriangle(int grid, int pos) {
            if (grid <= 0 || grid > 4 || pos < 0 ||pos > 26)
                return null;

            int p;
            Triangle triangle;
            if (grid == 1) { triangle = gridOne.Children[pos] as Triangle; }
            else if (grid == 2) { triangle = gridTwo.Children[pos] as Triangle; }
            else if (grid == 3) { triangle = gridThree.Children[pos] as Triangle; }
            else { triangle = gridFour.Children[pos] as Triangle; }
            
            return triangle;
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
