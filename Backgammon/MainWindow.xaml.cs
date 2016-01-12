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
using Backgammon;

namespace Backgammon
{

    public partial class MainWindow : Window
    {

        private int[] dice;
        private BrickHolder utslagnaVit, utslagnaSvart;
        private BackgammonModel _model = new BackgammonModel();
		private triangel [] gameBoard;
		private Triangle [] selectedTriangles = new Triangle[2];
		private int trianglePos = 0;
        private ImageBrush[] _background = new ImageBrush[8];
		private BitmapImage[] _dices = new BitmapImage[7];

        public MainWindow()
        {
            InitializeComponent();
			init();
            updateView();
			renderDices();
        }

		private void init()
		{
			dice = new int[4]{1,1,1,1};
			gameBoard = _model.newGame();

            utslagnaVit = utslagnaEtt.Children[0] as BrickHolder;
            utslagnaVit.setColor(COLOR.WHITE);
            utslagnaVit.setSize(14);
            utslagnaSvart = utslagnaTvå.Children[0] as BrickHolder;
            utslagnaSvart.setColor(COLOR.BLACK);
            utslagnaSvart.setSize(7);

			initimages();

			for(int i = 1; i < 27; i++)
				{
					Triangle t = getTriangle(i);
                    t.setLink(this);
					if(i < 13) t.setState(STATE.UPPER);
					else if (i < 25) t.setState(STATE.LOWER);
					else if (i == 25) t.setState(STATE.UPPER);
					else t.setState(STATE.LOWER);
				}

		}

		private void initimages()
		{
			for (int i = 0; i < _background.Length; i++)
                _background[i] = new ImageBrush();

			/* verkade inte behöva denna till datatypen BitmapImage
			 * for (int i = 0; i < _dices.Length; i++)
                _dices[i] = new BitmapImage();
			*/



            // Hämtar alla bakgrunder
            try {
                _background[0].ImageSource = new BitmapImage(new Uri("../../Resources/greenFelt.png", UriKind.Relative));
                _background[1].ImageSource = new BitmapImage(new Uri("../../Resources/greenishFelt.png", UriKind.Relative));
                _background[2].ImageSource = new BitmapImage(new Uri("../../Resources/blueFelt.png", UriKind.Relative));
                _background[3].ImageSource = new BitmapImage(new Uri("../../Resources/greenOrangeFelt.png", UriKind.Relative));
                _background[4].ImageSource = new BitmapImage(new Uri("../../Resources/orangeFelt.png", UriKind.Relative));
                _background[5].ImageSource = new BitmapImage(new Uri("../../Resources/purpleFelt.png", UriKind.Relative));
                _background[6].ImageSource = new BitmapImage(new Uri("../../Resources/redFelt.png", UriKind.Relative));
                _background[7].ImageSource = new BitmapImage(new Uri("../../Resources/darkRedFelt.png", UriKind.Relative));
				_dices[0] = null;
				_dices[1] = new BitmapImage(new Uri("../../Resources/tärningLjus1.png",UriKind.Relative));
				_dices[2] = new BitmapImage(new Uri("../../Resources/tärningLjus2v2.png",UriKind.Relative));
				_dices[3] = new BitmapImage(new Uri("../../Resources/tärningLjus3.png",UriKind.Relative));
				_dices[4] = new BitmapImage(new Uri("../../Resources/tärningLjus4v2.png",UriKind.Relative));
				_dices[5] = new BitmapImage(new Uri("../../Resources/tärningLjus5.png",UriKind.Relative));
				_dices[6] = new BitmapImage(new Uri("../../Resources/tärningLjus6.png",UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

		}


		private void renderDices() {

			Image [] dices;
			dices = new Image[]{dice1,dice2,dice3,dice4};
			

			for (int i = 0; i < dices.Length; i++)
			{
                dices[i].Source = _dices[dice[i]];
			}

		}



		public void selectTriangle(Triangle t)
		{
			selectedTriangles[trianglePos++] = t;
			if(trianglePos == 2)
				{

				if (_model.move(gameBoard, selectedTriangles[0].getPos(),selectedTriangles[1].getPos(),dice,COLOR.WHITE)) 
					{
					updateSelectedTriangles();
					renderDices();
					}
				else {MessageBox.Show("felaktigt move"); }
				trianglePos = 0;
				}
		}


		private void updateSelectedTriangles()
		{
			if(selectedTriangles[1].getSize() == 1 && selectedTriangles[0].getColor() != selectedTriangles[1].getColor())
				{
				int bar;
				if (selectedTriangles[0].getColor() == COLOR.WHITE) bar=26;
				else bar=25;
 				updateTriangle(getTriangle(bar));
				}

			for(int i = 0; i < 2; i++) updateTriangle(selectedTriangles[i]);

		}

		private void updateTriangle(Triangle t)
		{
			int index = _model.correctPos(t.getPos());
			t.setSize(gameBoard[index].antal);
			t.setColor(gameBoard[index].color);
			t.Update();
		}


        private void setBackground(int index) {
            duk.Background = _background[index];
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

        private void updateView() 
		{

			//Tänker mig att denna funktionen tar variabler från sidebar
			// och sedan uppdaterar baserat på vad man valt.
			setBackground(2);
            alignRight();

			getTriangle(25).setPos(25);
			getTriangle(26).setPos(26);

			for (int i = 0; i<26; i++) updateTriangle(getTriangle(i+1));

        }

        // Håller en 16:9 ratio på spelplanen
		private void updateScale()
		{
			spelPlan.Height = Width / 1.77;
			if (duk.Height >= 768 && duk.Width >= 1360) 
			{
				spelPlan.Height = 768;
				spelPlan.Width = 1360;
				return;
			}
            if (spelPlan.Height >= duk.Height)
                spelPlan.Height = duk.Height;

			spelPlan.Width = spelPlan.Height*(1.77);
		}


		//den ska gå till 26 sen.
        private Triangle getTriangle(int pos) {
            if (pos < 1 ||pos > 26)
                return null;

            Triangle triangle;
            if (pos < 7) { return triangle = gridOne.Children[pos-1] as Triangle; }
            else if (pos < 13) { return triangle = gridTwo.Children[pos-(6+1)] as Triangle; }
            else if (pos < 19) { return triangle = gridFour.Children[pos-(12+1)] as Triangle; }
            else if (pos < 25) { return triangle = gridThree.Children[pos-(18+1)] as Triangle; }
			else if (pos == 25){ return triangle = gridMiddle.Children[1] as Triangle; }
			else return triangle = gridMiddle.Children[0] as Triangle;

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

        private void sidebar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           if (Sidebar.Visibility == System.Windows.Visibility.Collapsed)
            {
                Sidebar.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Sidebar.Visibility = System.Windows.Visibility.Collapsed;
            } 
        }

        private void btnDice_MouseEnter(object sender, MouseEventArgs e)
        {
            btnDice.Source = new BitmapImage(new Uri("../../Resources/icon.png", UriKind.Relative));
        }

        private void btnDice_MouseLeave(object sender, MouseEventArgs e)
        {
            btnDice.Source = new BitmapImage(new Uri("../../Resources/redDice.png", UriKind.Relative));
        }

        private void btnDice_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
