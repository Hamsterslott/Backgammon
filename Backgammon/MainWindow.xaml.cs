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

    public partial class MainWindow : Window
    {

        int[] dices;
		BackgammonModel _model = new BackgammonModel();
		triangel [] spelplan;
		Triangle [] valdaTrianglar = null;
		private int trianglePos = 0;

        public MainWindow()
        {
            InitializeComponent();
            // Initierar allt vid start
            dices = _model.letsRollTheDice();
			spelplan = _model.newGame();

			//alignLeft();
			alignRight();

			for(int i = 1; i < 25; i++)
				{
					if(i < 13) getTriangle(i).setState(STATE.UPPER);
					else getTriangle(i).setState(STATE.LOWER);
				}


			for(int i = 1; i < 25; i++)
			{
				Triangle t = getTriangle(i);
				int index = _model.correctPos(t.getPos());
				t.setSize(spelplan[index].antal);
				t.setColor(spelplan[index].color);
			}
            
            update();
        }

		public void setTriangle(Triangle t)
		{
			valdaTrianglar[trianglePos++] = t;
			if(trianglePos == 2)
				{
				if (_model.move(spelplan, valdaTrianglar[0].getPos(),valdaTrianglar[1].getPos(),dices,COLOR.WHITE)) foreach (Triangle tri in valdaTrianglar) tri.Update();
				trianglePos = 0;
				}
		}

		private void alignLeft()
		{
			for(int i = 1; i < 25; i++)
				{
					if(i < 13) getTriangle(i).setPos(25-i);
					else getTriangle(i).setPos(i-12);
				}
		}

		private void alignRight()
		{
			for(int i = 1; i < 25; i++)
				{
					if(i < 13) getTriangle(i).setPos(12+i);
					else getTriangle(i).setPos(25-i);
				}
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
            else if (pos < 19) { return triangle = gridFour.Children[pos-(12+1)] as Triangle; }
            else { return triangle = gridThree.Children[pos-(18+1)] as Triangle; }

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try { DragMove(); }
			catch (Exception) {}
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (rect.Visibility == System.Windows.Visibility.Collapsed)
            {
                rect.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                rect.Visibility = System.Windows.Visibility.Collapsed;
            } 
        }

        private void sidebar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (rect.Visibility == System.Windows.Visibility.Collapsed)
            {
                rect.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                rect.Visibility = System.Windows.Visibility.Collapsed;
            } 
        } 

    }
}
