using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;


namespace Backgammon
{

    public partial class MainWindow : Window
    {

        internal bool canMoveWindow = true;
       
        internal int[] dice = new int[4];
		internal player spelare = player.two;
		internal triangel [] gameBoard;
		internal BackgammonModel _model = new BackgammonModel();

		internal Triangle [] selectedTriangles = new Triangle[2];
		internal int pickedUp = 0;
		
		internal BrickHolder[] utslagna;
        private int [] playercheckers = new int[]{15,15};
		

		//mainwindow bilder
        private BitmapImage[,] _dices = new BitmapImage[4,7];
		private BitmapImage[] _waitingdices = new BitmapImage[4];
		private BitmapImage[] _diceshaker = new BitmapImage[2];
		internal Cursor [,] plockadbricka = new Cursor[2,4];
		internal ImageBrush[] _background = new ImageBrush[8];

		//mainwindow ljud
		private SoundPlayer shake = new SoundPlayer(Properties.Resources.ShakeSound);
        private SoundPlayer throwThem = new SoundPlayer(Properties.Resources.diceSound);
        internal MediaPlayer song = new MediaPlayer();

		//triangel bilder
		internal ImageBrush[] singleBrick = new ImageBrush[4], doubleBrick = new ImageBrush[4], tripleBrick = new ImageBrush[4];
		internal ImageBrush[] _triangelIsClicked = new ImageBrush[2];

		//brickholder bilder
		internal ImageBrush[] brick = new ImageBrush[4];
		

        public MainWindow()
        {
            InitializeComponent();
			init();
            updateView();
        }

		private void init()
		{
			gameBoard = _model.newGame();
            //gameBoard = _model.endGame();
            //gameBoard = _model.highStack();
            //gameBoard = _model.bricksInMiddle();


            initimages();
            Sidebar.setLink(this);
			setBackground(Settings.background);
            

			
			utslagna = new BrickHolder[]{(BrickHolder)utslagnaEtt.Children[0],(BrickHolder)utslagnaTvå.Children[0]};

			for(int i = 0; i<2; i++)
			{
			utslagna[i].setColor((player)i);
			utslagna[i].setLink(this);
			if(i==0)utslagna[i].setPos(i);
			else utslagna[i].setPos(25);
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

			for (int i = 0; i < singleBrick.Length; i++) 
				{
                    singleBrick[i] = new ImageBrush();
                    doubleBrick[i] = new ImageBrush();
                    tripleBrick[i] = new ImageBrush();
					brick[i] = new ImageBrush();
                }

			
			for (int i = 0; i < _triangelIsClicked.Length; i++)
                _triangelIsClicked[i] = new ImageBrush();



            // Hämtar alla bakgrunder
            try {

                _background[0].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/greenFelt.png"));
                _background[1].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/greenishFelt.png"));
                _background[2].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/blueFelt.png"));
                _background[3].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/greenOrangeFelt.png"));
                _background[4].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/orangeFelt.png"));
                _background[5].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/purpleFelt.png"));
                _background[6].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/redFelt.png"));
                _background[7].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/darkRedFelt.png"));

				
				plockadbricka[0,0] = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/lightHandleSmall.cur")).Stream);
				plockadbricka[0,1] = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/darkHandleSmall.cur")).Stream);
				plockadbricka[0,2] = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/blueHandleSmall.cur")).Stream);
				plockadbricka[0,3] = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/redHandleSmall.cur")).Stream);
				plockadbricka[1,0] = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/lightHandle.cur")).Stream);
				plockadbricka[1,1] = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/darkHandle.cur")).Stream);
				plockadbricka[1,2] = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/blueHandle.cur")).Stream);
				plockadbricka[1,3] = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/redHandle.cur")).Stream);

				_diceshaker[0] = new BitmapImage(new Uri("pack://application:,,,/Resources/diceShaker.png"));
				_diceshaker[1] = new BitmapImage(new Uri("pack://application:,,,/Resources/diceShakerDown.png"));

				_dices[0, 0] = null; 
                _dices[0, 1] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice1Light.png"));
                _dices[0, 2] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice2Light.png"));
                _dices[0, 3] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice3Light.png"));
                _dices[0, 4] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice4Light.png"));
                _dices[0, 5] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice5Light.png"));
                _dices[0, 6] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice6Light.png"));
                _dices[1, 0] = null;
                _dices[1, 1] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice1Black.png"));
                _dices[1, 2] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice2Black.png"));
                _dices[1, 3] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice3Black.png"));
                _dices[1, 4] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice4Black.png"));
                _dices[1, 5] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice5Black.png"));
                _dices[1, 6] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice6Black.png"));
				_dices[2, 0] = null;
				_dices[2, 1] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice1Blue.png"));
				_dices[2, 2] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice2Blue.png"));
				_dices[2, 3] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice3Blue.png"));
				_dices[2, 4] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice4Blue.png"));
				_dices[2, 5] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice5Blue.png"));
				_dices[2, 6] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice6Blue.png"));
				_dices[3, 0] = null;
				_dices[3, 1] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice1Red.png"));
				_dices[3, 2] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice2Red.png"));
				_dices[3, 3] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice3Red.png"));
				_dices[3, 4] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice4Red.png"));
				_dices[3, 5] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice5Red.png"));
				_dices[3, 6] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice6Red.png"));


				_waitingdices[0] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice11Light.png"));
				_waitingdices[1] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice11Black.png"));
				_waitingdices[2] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice11Blue.png"));
				_waitingdices[3] = new BitmapImage(new Uri("pack://application:,,,/Resources/dice11Red.png"));

				_triangelIsClicked[0].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/isClickedUpper.png"));
				_triangelIsClicked[1].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/isClickedLower.png"));
				
                singleBrick[0].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/whiteChip1.png"));
                doubleBrick[0].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/whiteChip2.png"));
                tripleBrick[0].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/whiteChip3.png"));

                singleBrick[1].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/darkChip1.png"));
                doubleBrick[1].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/darkChip2.png"));
                tripleBrick[1].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/darkChip3.png"));

				singleBrick[2].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/BlåKnapp.png"));
                doubleBrick[2].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/2BlåKnapp.png"));
                tripleBrick[2].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/3BlåKnapp.png"));

				singleBrick[3].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/RödKnapp.png"));
                doubleBrick[3].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/2RödKnapp.png"));
                tripleBrick[3].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/3RödKnapp.png"));

				brick[0].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/vitKnappLigga.png"));
				brick[1].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/svartKnappLigga.png"));
				brick[2].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/blåKnappLigga.png"));
				brick[3].ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/rödKnappLigga.png"));

            }


            
            catch (Exception ex) 
			{ 
				MessageBox.Show(ex.Message);
 				Environment.Exit(0);
			}

		}


		private void renderDices() {
			Image [] dices = new Image[]{dice1,dice2,dice3,dice4};
			for (int i = 0; i < dices.Length; i++) dices[i].Source = _dices[Settings.playerTheme[(int)spelare],dice[i]];
		}



		public void playGame(Triangle t)
		{
			int status = _model.canMove(gameBoard, spelare, dice);
			if (pickedUp == 0 && status != 0 && t.getColor() == spelare && t.getSize() >= 1) selectedTriangles[pickedUp++] = t;
			else if (pickedUp == 1) selectedTriangles[pickedUp++] = t;

			if (pickedUp == 1)
			{
				if(t.getSize()>=1 && t.getColor() == spelare)
					{ 
					t.setSize(t.getSize()-1);
					if(t.getPos() < 25) t.setGlowing(true);
					t.Update();
					setCursor();
					}
			}

			if(pickedUp == 2)
				{
                    
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
								utslagna[(int)spelare].setSize(15-playercheckers[(int)spelare]);
                                
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
						for(int i = 0; i < 4; i++) dice[i] = 0;
						renderDices();
                        btnDice.Visibility = System.Windows.Visibility.Visible;
                        diceTop.Visibility = System.Windows.Visibility.Visible;
                        diceBot.Visibility = System.Windows.Visibility.Visible;
                    }

				selectedTriangles[0].setGlowing(false);
                updateSelectedTriangles();
                renderDices();
				pickedUp = 0;
                setCursor();
				}
		}


		private void updateSelectedTriangles()
		{
			if(selectedTriangles[1].getSize() == 1 && selectedTriangles[0].getColor() != selectedTriangles[1].getColor())
				{
				int bar;
				if (selectedTriangles[0].getColor() == player.one) bar=26;
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


        public void setBackground(int index) {
            duk.Background = _background[index];
        }


		private void alignLeft()
		{
			for(int i = 1; i < 25; i++)
				{
					if(i < 13) getTriangle(i).setPos(25-i);
					else getTriangle(i).setPos(i-12);
				}

			getTriangle(25).setPos(25);
			getTriangle(26).setPos(26);
		}

		private void alignRight()
		{
			for(int i = 1; i < 25; i++)
				{
					if(i < 13) getTriangle(i).setPos(12+i);
					else getTriangle(i).setPos(25-i);
				}

			getTriangle(25).setPos(25);
			getTriangle(26).setPos(26);
		}

        internal void updateView() 
		{

			//Tänker mig att denna funktionen tar variabler från sidebar
			// och sedan uppdaterar baserat på vad man valt.
			alignLeft();
			foreach(BrickHolder u in utslagna) u.update();
			renderDices();

			diceTop.Source = _waitingdices[Settings.playerTheme[0]];
			diceBot.Source = _waitingdices[Settings.playerTheme[1]];

			for (int i = 0; i<26; i++) updateTriangle(getTriangle(i+1));

			if (pickedUp == 1) 
			{
				selectedTriangles[0].setSize(selectedTriangles[0].getSize()-1);
				selectedTriangles[0].Update();
			}
        }

		internal void setCursor()
		{
			if (pickedUp == 1) 
			{
				if(Width*Height < 850000) this.Cursor = plockadbricka[0,Settings.playerTheme[(int)spelare]];
				else this.Cursor = plockadbricka[1,Settings.playerTheme[(int)spelare]];
			}
			else this.Cursor = Cursors.Arrow;
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
            try{
                if (canMoveWindow)
                {
                    if (Sidebar.Visibility == System.Windows.Visibility.Visible)
						{
                        Sidebar.Visibility = System.Windows.Visibility.Collapsed;
						Sidebar.Width = 290;
						Sidebar.info.Width = new GridLength(0);
						}

                    else
                        DragMove();
                }
            }
			catch (Exception ) { Console.WriteLine("Messageboxarna gör så man får System.InvalidOperationException här"); }
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
                Sidebar.Visibility = System.Windows.Visibility.Visible;
                Sidebar.Opacity = 0;
                DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
                Sidebar.BeginAnimation(Sidebar.OpacityProperty, animation);
        }

       
        
            

        private void btnDice_MouseEnter(object sender, MouseEventArgs e)
        {
            btnDice.Source = _diceshaker[1];


            if (Settings.playSound)
                shake.Play();
           
            
            if (spelare == player.one) 
            {
                diceBot.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                diceTop.Visibility = System.Windows.Visibility.Collapsed;
            }
            
            
        }

        private void btnDice_MouseLeave(object sender, MouseEventArgs e)
        {
            btnDice.Source = _diceshaker[0];
            
            if (spelare == player.one)
            {
                diceBot.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                diceTop.Visibility = System.Windows.Visibility.Visible;
            }
            if (Settings.playSound)
                shake.Stop();
        }

        private void btnDice_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsEnabled = false;
            if (spelare == player.one) spelare = player.two;
            else spelare = player.one;
            dice = _model.letsRollTheDice();            
            renderDices();
            if (_model.canMove(gameBoard, spelare, dice) != 0)
            {
                btnDice.Visibility = System.Windows.Visibility.Collapsed;
            } 
            else
			{
				MessageBox.Show("Inga tillgängliga moves");
				for(int i = 0; i < 4; i++) dice[i] = 0;
				renderDices();
				btnDice.Visibility = System.Windows.Visibility.Visible;
                diceTop.Visibility = System.Windows.Visibility.Visible;
                diceBot.Visibility = System.Windows.Visibility.Visible;
			}
            if (Settings.playSound)
            {
                throwThem.Stop();
                throwThem.PlaySync();
            }
                this.IsEnabled = true;
        }

        private void spelPlan_MouseEnter(object sender, MouseEventArgs e)
        {
            canMoveWindow = false;
        }

        private void spelPlan_MouseLeave(object sender, MouseEventArgs e)
        {
            canMoveWindow = true;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void sidebar_MouseEnter(object sender, MouseEventArgs e)
        {
            canMoveWindow = false;
        }

        private void sidebar_MouseLeave(object sender, MouseEventArgs e)
        {
            canMoveWindow = true;
        }

        private void spelPlan_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sidebar.Visibility = System.Windows.Visibility.Collapsed;
			Sidebar.Width = 290;
			Sidebar.info.Width = new GridLength(0);
        }

    }
}
