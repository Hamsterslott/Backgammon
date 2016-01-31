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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace Backgammon
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Message : UserControl
    {
        public Message()
        {
            InitializeComponent();
        }

        public void show(string text)
        {
            Text.Content = text;
            this.Visibility = System.Windows.Visibility.Visible;
            DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(5));
            this.BeginAnimation(System.Windows.Controls.Canvas.OpacityProperty, animation); 
        }
    }
}
