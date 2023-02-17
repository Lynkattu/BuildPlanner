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

namespace BuilPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Attributes att = new Attributes();
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new AttributePage(att.GetAttributes());
        }

        private void BtAttributes_Click(object sender, RoutedEventArgs e)
        {

            Main.Content = new AttributePage(att.GetAttributes());

        }

        private void BtOrigin_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new OriginPage();
        }

        private void BtSkills_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new SkillsPage();
        }

        private void BtStat_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new StatPage();
        }

        private void BtItems_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ItemPage();
        }

    }
}
