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
    /// Interaction logic for Skills.xaml
    /// </summary>
    public partial class SkillsPage : Page
    {
        Attributes att = new Attributes();
        ItemTooltip itemTooltip = new ItemTooltip();
        SkillToolTip skillTooltip = new SkillToolTip();
        bool isEntered = false;
        public SkillsPage()
        {
            InitializeComponent();

            ImageBrush background = new ImageBrush();

            tb_Origin.Text += att.GetOrigin().ToString();
            tb_Background.Text += att.GetBackground().ToString();
            tb_Profession1.Text += att.GetProfession(true).ToString();
            tb_Profession2.Text += att.GetProfession(false).ToString();

            switch (att.GetBackground())
            {
                case Attributes.Background.Assassin:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Assassin.jpg"));
                    break;
                case Attributes.Background.Pirate:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Pirate.jpg"));
                    break;
                case Attributes.Background.Trickster:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Trickster.jpg"));
                    break;
                case Attributes.Background.Necromancer:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Necromancer.jpg"));
                    break;
                case Attributes.Background.Sorcerer:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Sorcerer.jpg"));
                    break;
                case Attributes.Background.Warlock:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Warlock.jpg"));
                    break;
                case Attributes.Background.Mobed:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Mobed.jpg"));
                    break;
                case Attributes.Background.Monk:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Monk.jpg"));
                    break;
                case Attributes.Background.Priest:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Priest.jpg"));
                    break;
                case Attributes.Background.Druid:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Druid.jpg"));
                    break;
                case Attributes.Background.Hofgothi:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Hofgothi.jpg"));
                    break;
                case Attributes.Background.Shaman:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Shaman.jpg"));
                    break;
                case Attributes.Background.Alchemist:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Alchemist.jpg"));
                    break;
                case Attributes.Background.Artificer:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Artificer.jpg"));
                    break;
                case Attributes.Background.Mage:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Mage.jpg"));
                    break;
                case Attributes.Background.Bard:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Bard.jpg"));
                    break;
                case Attributes.Background.Jester:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Painter.png"));
                    break;
                case Attributes.Background.Dancer:
                    background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/BuilPlanner;component/Images/Dancer.jpg"));
                    break;
            }
            Can_skills.Background = background;
        }

        private void Label_MainWeaponSet_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isEntered)
            {
                bool isNull = true;
                Label srcLabel = e.Source as Label;
                Weapon weapon;

                switch (srcLabel.Name)
                {
                    case "lb_main":
                        weapon = att.GetWeapon(Attributes.ItemSlot.MainHand, true);
                        if (weapon != null)
                        {
                            itemTooltip.WeaponTooltip(weapon, true);
                            isNull = false;
                        }
                        break;

                    case "lb_off":
                        weapon = att.GetWeapon(Attributes.ItemSlot.OffHand, true);
                        if (weapon != null)
                        {
                            itemTooltip.WeaponTooltip(weapon, true);
                            isNull = false;
                        }
                        break;
                }
                if (!isNull)
                {

                    Canvas.SetLeft(itemTooltip.GetItemTooltip(), Mouse.GetPosition(Can_skills).X + 20);
                    Canvas.SetTop(itemTooltip.GetItemTooltip(), Mouse.GetPosition(Can_skills).Y - 10);
                    Can_skills.Children.Add(itemTooltip.GetItemTooltip());
                }

                isEntered = true;
            }
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            isEntered = false;
            if (itemTooltip != null)
            {
                itemTooltip.ClearTooltipChildens();
                Can_skills.Children.Remove(itemTooltip.GetItemTooltip());
            }

        }

        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            if (itemTooltip != null)
            {
                Canvas.SetLeft(itemTooltip.GetItemTooltip(), Mouse.GetPosition(Can_skills).X + 20);
                Canvas.SetTop(itemTooltip.GetItemTooltip(), Mouse.GetPosition(Can_skills).Y - 10);
            }
        }

        private void Label_SkillSearch_MouseLeftClick(object sender, MouseEventArgs e)
        {
            Label srcLabel = e.Source as Label;
            
            switch (srcLabel.Name)
            {
                case "lb_main_skill_1":
                    SkillBrowser.Content = new SkillSearchPage(1);
                    break;
                case "lb_main_skill_2":
                    SkillBrowser.Content = new SkillSearchPage(2);
                    break;
                case "lb_main_skill_3":
                    SkillBrowser.Content = new SkillSearchPage(3);
                    break;
                case "lb_main_skill_4":
                    SkillBrowser.Content = new SkillSearchPage(4);
                    break;
                case "lb_main_skill_5":
                    SkillBrowser.Content = new SkillSearchPage(5);
                    break;
                case "lb_main_skill_6":
                    SkillBrowser.Content = new SkillSearchPage(6);
                    break;
            }
        }

        private void Label_skillSlot_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isEntered)
            {
                Label srcLabel = e.Source as Label;
                bool isNull = true;
                switch (srcLabel.Name)
                {
                    case "lb_main_skill_1":
                        if (att.GetSkill(1) != null)
                        {
                            skillTooltip.CreateTooltip(att.GetSkill(1).GetName());
                            isNull = false;
                        }
                        break;
                    case "lb_main_skill_2":
                        if (att.GetSkill(2) != null)
                        {
                            skillTooltip.CreateTooltip(att.GetSkill(2).GetName());
                            isNull = false;
                        }
                        break;
                    case "lb_main_skill_3":
                        if (att.GetSkill(3) != null)
                        {
                            skillTooltip.CreateTooltip(att.GetSkill(3).GetName());
                            isNull = false;
                        }
                        break;
                    case "lb_main_skill_4":
                        if (att.GetSkill(4) != null)
                        {
                            skillTooltip.CreateTooltip(att.GetSkill(4).GetName());
                            isNull = false;
                        }
                        break;
                    case "lb_main_skill_5":
                        if (att.GetSkill(5) != null)
                        {
                            skillTooltip.CreateTooltip(att.GetSkill(5).GetName());
                            isNull = false;
                        }
                        break;
                    case "lb_main_skill_6":
                        if (att.GetSkill(6) != null)
                        {
                            skillTooltip.CreateTooltip(att.GetSkill(6).GetName());
                            isNull = false;
                        }
                        break;
                }

                if (!isNull)
                {
                    Canvas.SetLeft(skillTooltip.GetTooltip(), Mouse.GetPosition(Can_skills).X + 20);
                    Canvas.SetTop(skillTooltip.GetTooltip(), Mouse.GetPosition(Can_skills).Y - 10);
                    Can_skills.Children.Add(skillTooltip.GetTooltip());
                }

                isEntered = true;
            }
        }

        private void Label_skillSlot_MouseMove(object sender, MouseEventArgs e)
        {
            if (itemTooltip != null)
            {
                Canvas.SetLeft(skillTooltip.GetTooltip(), Mouse.GetPosition(Can_skills).X + 20);
                Canvas.SetTop(skillTooltip.GetTooltip(), Mouse.GetPosition(Can_skills).Y - 10);
            }
        }

        private void Label_skillSLot_MouseLeave(object sender, MouseEventArgs e)
        {
            isEntered = false;
            if (itemTooltip != null)
            {
                skillTooltip.ClearTooltipChildens();
                Can_skills.Children.Remove(skillTooltip.GetTooltip());
            }

        }
    }
}
