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
        private int[] dices = new int[3];

        public MainWindow()
        {
            InitializeComponent();
           
            // Initierar allt vid start
            dices = rollDices();
            update();
        }

        private int[] rollDices() {
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
        private void updateScale() {
            spelPlan.Height = Width / 1.77;
            if (spelPlan.Height >= duk.Height)
                spelPlan.Height = duk.Height;

            spelPlan.Width = spelPlan.Height * (1.77);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
    }
}
