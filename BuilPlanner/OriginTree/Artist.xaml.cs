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

namespace BuilPlanner.OriginTree
{
    /// <summary>
    /// Interaction logic for Artist.xaml
    /// </summary>
    public partial class Artist : Page
    {
        bool isEntered = false;
        TextBlock tooltip = new TextBlock();
        public Artist()
        {
            InitializeComponent();
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isEntered)
            {
                tooltip.Text = "Creative\nIncrease 0.5% cooldown rate per inspiration stack";
                tooltip.Foreground = Brushes.White;

                Canvas.SetLeft(tooltip, Mouse.GetPosition(OriginCanvas).X + 20);
                Canvas.SetTop(tooltip, Mouse.GetPosition(OriginCanvas).Y - 10);
                OriginCanvas.Children.Add(tooltip);
                isEntered = true;
            }


        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            isEntered = false;
            OriginCanvas.Children.Remove(tooltip);
        }

        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            Canvas.SetLeft(tooltip, Mouse.GetPosition(OriginCanvas).X + 20);
            Canvas.SetTop(tooltip, Mouse.GetPosition(OriginCanvas).Y - 10);
        }

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TbTalent_Creative.Text = "1/5";
        }
    }
}
