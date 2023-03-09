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
    /// Interaction logic for OriginPage.xaml
    /// </summary>
    public partial class OriginPage : Page
    {
        private Attributes attrib = new Attributes();
        private Lib lib = new Lib();
        
        public OriginPage()
        {
            InitializeComponent();
            SetOriginItemList();
            CbOrigin.SelectedItem = attrib.GetOrigin();
            SetBackgroundItemList(attrib.GetOrigin());
            CbBackground.SelectedItem = attrib.GetBackground();
        }
        private void SetBackgroundItemList(Lib.Origin origin)
        {
            
            CbBackground.ItemsSource = lib.GetBackgroundItem(origin);
        }

        private void SetOriginItemList()
        {
            CbOrigin.ItemsSource = lib.GetOriginItemList();
        }


        private void CbOrigin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = e.Source as ComboBox;
            Lib.Origin origin = (Lib.Origin)cb.SelectedValue;
            ImageBrush background = new ImageBrush();


            if ( origin != attrib.GetOrigin())
            {
                attrib.SetOrigin(origin);
                SetBackgroundItemList(origin);
                CbBackground.SelectedIndex = 0;
            }
            background.ImageSource = new BitmapImage(new Uri(lib.GetOriginImage(origin)));
            grid_origin.Background = background;
        }

        private void CbBackground_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = e.Source as ComboBox;
            if (cb.SelectedValue != null)
            {
                Lib.Background background = (Lib.Background)cb.SelectedValue;
                attrib.SetBackground(background);
            }
        }

        private void BtShowOrigin_Click(object sender, RoutedEventArgs e)
        {
            switch(attrib.GetOrigin())
            {
                case Lib.Origin.Artist:
                    Tree.Content = new OriginTree.Artist();
                    break;
            }
        }
    }
}
