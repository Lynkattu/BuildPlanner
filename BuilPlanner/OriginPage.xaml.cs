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
        
        public OriginPage()
        {
            InitializeComponent();
            SetOriginItemList();
            CbOrigin.SelectedItem = attrib.GetOrigin();
            SetBackgroundItemList(attrib.GetOrigin());
            CbBackground.SelectedItem = attrib.GetBackground();
            SetProfessionItemList();
        }
        private void SetBackgroundItemList(Attributes.Origin origin)
        {
            List<Attributes.Background> backgroundItems = new List<Attributes.Background>();

            switch (origin)
            {
                case Attributes.Origin.Artist:
                    backgroundItems.Add(Attributes.Background.Bard);
                    backgroundItems.Add(Attributes.Background.Dancer);
                    backgroundItems.Add(Attributes.Background.Jester);
                    //CbBackground.SelectedItem = subjectsList.First(i => i.Property == "DefaultChatSubject");
                    break;
                case Attributes.Origin.Believer:
                    backgroundItems.Add(Attributes.Background.Mobed);
                    backgroundItems.Add(Attributes.Background.Monk);
                    backgroundItems.Add(Attributes.Background.Priest);
                    break;
                case Attributes.Origin.Criminal:
                    backgroundItems.Add(Attributes.Background.Assassin);
                    backgroundItems.Add(Attributes.Background.Pirate);
                    backgroundItems.Add(Attributes.Background.Trickster);
                    break;
                case Attributes.Origin.Occultist:
                    backgroundItems.Add(Attributes.Background.Necromancer);
                    backgroundItems.Add(Attributes.Background.Sorcerer);                    
                    backgroundItems.Add(Attributes.Background.Warlock);
                    break;
                case Attributes.Origin.Primitive:
                    backgroundItems.Add(Attributes.Background.Druid);
                    backgroundItems.Add(Attributes.Background.Hofgothi);
                    backgroundItems.Add(Attributes.Background.Shaman);
                    break;
                case Attributes.Origin.Scholar:
                    backgroundItems.Add(Attributes.Background.Alchemist);
                    backgroundItems.Add(Attributes.Background.Artificer);
                    backgroundItems.Add(Attributes.Background.Mage);
                    break;
            }
            CbBackground.ItemsSource = backgroundItems;
        }

        private void SetOriginItemList()
        {
            List<Attributes.Origin> originList = new List<Attributes.Origin>();

            foreach (Attributes.Origin rw in (Attributes.Origin[])Enum.GetValues(typeof(Attributes.Origin)))
            {
                originList.Add(rw);
            }

            CbOrigin.ItemsSource = originList;
        }

        private void SetProfessionItemList()
        {
            List<Attributes.Profession> professionList = new List<Attributes.Profession>();

            foreach (Attributes.Profession profession in (Attributes.Profession[])Enum.GetValues(typeof(Attributes.Profession)))
            {
                professionList.Add(profession);
            }

            CbProfession1.ItemsSource = professionList;
            CbProfession1.SelectedIndex = 0;

            CbProfession2.ItemsSource = professionList;
            CbProfession2.SelectedIndex = 1;
        }

        private void CbOrigin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = e.Source as ComboBox;
            Attributes.Origin origin = (Attributes.Origin)cb.SelectedValue;
            ImageBrush background = new ImageBrush();


            if ( origin != attrib.GetOrigin())
            {
                attrib.SetOrigin(origin);
                SetBackgroundItemList(origin);
                CbBackground.SelectedIndex = 0;
            }
            switch (origin)
            {
                case Attributes.Origin.Criminal:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Criminal_Background.jpg"));
                    break;
                case Attributes.Origin.Occultist:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Occultist_Background.jpg"));
                    break;
                case Attributes.Origin.Artist:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Artist_Background.jpg"));
                    break;
                case Attributes.Origin.Believer:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Believer_Background.jpg"));
                    break;
                case Attributes.Origin.Primitive:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Primitive_Background.jpg"));
                    break;
                case Attributes.Origin.Scholar:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Scholar_Background.jpg"));
                    break;
            }
            grid_origin.Background = background;
        }

        private void CbBackground_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = e.Source as ComboBox;
            if (cb.SelectedValue != null)
            {
                Attributes.Background background = (Attributes.Background)cb.SelectedValue;
                attrib.SetBackground(background);
            }

        }

        private void BtShowOrigin_Click(object sender, RoutedEventArgs e)
        {
            switch(attrib.GetOrigin())
            {
                case Attributes.Origin.Artist:
                    Tree.Content = new OriginTree.Artist();
                    break;
            }
        }
    }
}
