using System;
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
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Media.Animation;

namespace Backgammon
{
    public partial class Splash : Window
    {

        MainWindow backGammon = new MainWindow();

        public Splash()
        {
            InitializeComponent();
            DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(5));
            animation.Completed += new EventHandler(animation_Completed);
            this.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, animation);
        }

        private void animation_Completed(object sender, EventArgs e)
        {
            backGammon.Show();
            this.Close();
        }
    }
}
