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

		private BackgammonModel _model = new BackgammonModel();
        private int[] dice = new int[4];
		private triangel [] gameBoard;
        private BitmapImage[] _dices = new BitmapImage[7];
		private ImageBrush[] _background = new ImageBrush[8];

		private Triangle [] selectedTriangles = new Triangle[2];
		private int trianglePos = 0;
        
        private COLOR spelare = COLOR.BLACK;
        private int [] playercheckers = new int[]{15,15};
		private BrickHolder[] utslagna;

        public MainWindow()
        {
            InitializeComponent();
			init();
            updateView();
        }

		private void init()
		{
			gameBoard = _model.newGame();

            initimages();

			utslagna = new BrickHolder[]{(BrickHolder)utslagnaEtt.Children[0],(BrickHolder)utslagnaTvå.Children[0]};

			for(int i = 0; i<2; i++)
			{
			utslagna[i].setColor((COLOR)i);
			utslagna[i].setSize(20-playercheckers[i]);
			utslagna[i].setLink(this);
			}
        

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

			Image [] dices = new Image[]{dice1,dice2,dice3,dice4};

			for (int i = 0; i < dices.Length; i++) dices[i].Source = _dices[dice[i]];

		}



		public void selectTriangle(Triangle t)
		{
			selectedTriangles[trianglePos++] = t;
			if (trianglePos == 1)
			{
				if(t.getSize()>=1 && t.getColor() == spelare)
					{ 
					t.setSize(t.getSize()-1);
					t.Update();
					}
			}

			if(trianglePos == 2)
				{
                    int status = _model.canMove(gameBoard, spelare, dice);
                    if (status == -1)
                    {
                        if (selectedTriangles[0].getPos() != 25 && selectedTriangles[0].getPos() != 26)
                        {
                            MessageBox.Show("Du måste placera ut din utslagna bricka");
                        }
                        else if (!_model.move(gameBoard, selectedTriangles[0].getPos(), selectedTriangles[1].getPos(), dice, spelare))
                        {
                            MessageBox.Show("felaktigt move");
                        }
                    }
                    else
                    {
                        if (status == 2 && (selectedTriangles[1].getPos() == 0 || selectedTriangles[1].getPos() == 25))
                        {
                            if (_model.moveGoal(gameBoard, selectedTriangles[0].getPos(), dice, spelare))
                            {
                                playercheckers[(int)spelare]--;
								utslagna[(int)spelare].setSize(15-playercheckers[0]);
                            }
                            else
                            {
                                MessageBox.Show("felaktigt move");
                            }
                        }
                        else if (!_model.move(gameBoard, selectedTriangles[0].getPos(), selectedTriangles[1].getPos(), dice, spelare))
                        {
                            MessageBox.Show("felaktigt move");
                        }
                    }
                    status = _model.canMove(gameBoard, spelare, dice);
                    if (status == 0)
                    {
                        btnDice.Visibility = System.Windows.Visibility.Visible;
                    }
                updateSelectedTriangles();
                renderDices();
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
            alignLeft();

			getTriangle(25).setPos(25);
			getTriangle(26).setPos(26);

			for (int i = 0; i<26; i++) updateTriangle(getTriangle(i+1));

        }

        // Håller en 16:9 ratio på spelplanen
		private void updateScale()
		{
			spelPlan.Height = Width / 1.77;
			if (duk.Height >= 900 && duk.Width >= 1600) 
			{
				spelPlan.Height = 900;
				spelPlan.Width = 1600;
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
            btnDice.Source = new BitmapImage(new Uri("../../Resources/diceShakerDown.png", UriKind.Relative));
        }

        private void btnDice_MouseLeave(object sender, MouseEventArgs e)
        {
            btnDice.Source = new BitmapImage(new Uri("../../Resources/diceShaker.png", UriKind.Relative));
        }

        private void btnDice_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (spelare == COLOR.WHITE) spelare = COLOR.BLACK;
            else spelare = COLOR.WHITE;
            dice = _model.letsRollTheDice();
            renderDices();
            if (_model.canMove(gameBoard, spelare, dice) != 0)
            {
                btnDice.Visibility = System.Windows.Visibility.Collapsed;
            }
               
        }

    }
}
