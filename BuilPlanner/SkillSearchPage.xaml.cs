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
    /// Interaction logic for SkillSearchPage.xaml
    /// </summary>
    public partial class SkillSearchPage : Page
    {
        private Attributes att = new Attributes();
        private Skills.SkillList skillList = new Skills.SkillList();
        private SkillToolTip skillTooltip = new SkillToolTip();
        private bool isEntered = false;
        private int skillSlot;
        public SkillSearchPage() { }
        public SkillSearchPage(int slot)
        {
            InitializeComponent();
            skillSlot = slot;
            Thickness skillMargin = new Thickness(25, 0, 0, 0);
            StackPanel sp_origin = new StackPanel()
            {
                Margin = skillMargin
            };
            StackPanel sp_background = new StackPanel()
            {
                Margin = skillMargin
            };

            Expander originExp = new Expander()
            {
                Header = att.GetOrigin(),
                Foreground = Brushes.LightSteelBlue,
                FontSize = 20
            };
          
            foreach (Skill originSkill in skillList.GetSkillList(att.GetOrigin()))
            {
                TextBlock tb = new TextBlock()
                {
                    Name = originSkill.GetName().Replace(" ",""),
                    Text = originSkill.GetName(),
                    Foreground = originSkill.GetSkillColor(),
                    FontSize = 18
                };
                tb.MouseEnter += new MouseEventHandler(Tb_SkillTooltip_MouseEnter);
                tb.MouseMove += new MouseEventHandler(Tb_SkillTooltip_MouseMove);
                tb.MouseLeave += new MouseEventHandler(Tb_SkillTooltip_MouseLeave);
                tb.MouseLeftButtonDown += new MouseButtonEventHandler(Tb_SkillTooltip_MouseLeftClick);
                sp_origin.Children.Add(tb);
            }
            originExp.Content = sp_origin;
            Sp_Skill_Search.Children.Add(originExp);

            Expander backgroundExp = new Expander()
            {
                Header = att.GetBackground(),
                Foreground = Brushes.LightSteelBlue,
                FontSize = 20
            };

            foreach (Skill backgroundSkill in skillList.GetSkillList(att.GetBackground()))
            {
                TextBlock tb = new TextBlock()
                {
                    Name = backgroundSkill.GetName().Replace(" ", ""),
                    Text = backgroundSkill.GetName(),
                    Foreground = backgroundSkill.GetSkillColor(),
                    FontSize = 18
                };
                tb.MouseEnter += new MouseEventHandler(Tb_SkillTooltip_MouseEnter);
                tb.MouseMove += new MouseEventHandler(Tb_SkillTooltip_MouseMove);
                tb.MouseLeave += new MouseEventHandler(Tb_SkillTooltip_MouseLeave);
                tb.MouseLeftButtonDown += new MouseButtonEventHandler(Tb_SkillTooltip_MouseLeftClick);
                sp_background.Children.Add(tb);
            }
            backgroundExp.Content = sp_background;
            Sp_Skill_Search.Children.Add(backgroundExp);
        }

        private void Tb_SkillTooltip_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isEntered)
            {
                TextBlock t = e.Source as TextBlock;
                skillTooltip.CreateTooltip(t.Text);
                Canvas.SetLeft(skillTooltip.GetTooltip(), Mouse.GetPosition(Can_SkillSearch).X + 20);
                Canvas.SetTop(skillTooltip.GetTooltip(), Mouse.GetPosition(Can_SkillSearch).Y - 10);
                Can_SkillSearch.Children.Add(skillTooltip.GetTooltip());
                isEntered = true;
            }
        }

        private void Tb_SkillTooltip_MouseMove(object sender, MouseEventArgs e)
        {
            if (isEntered)
            {
                Canvas.SetLeft(skillTooltip.GetTooltip(), Mouse.GetPosition(Can_SkillSearch).X + 20);
                Canvas.SetTop(skillTooltip.GetTooltip(), Mouse.GetPosition(Can_SkillSearch).Y - 10);
            }
        }

        private void Tb_SkillTooltip_MouseLeave(object sender, MouseEventArgs e)
        {
            isEntered = false;
            skillTooltip.ClearTooltipChildens();
            Can_SkillSearch.Children.Remove(skillTooltip.GetTooltip());
        }

        private void Tb_SkillTooltip_MouseLeftClick(object sender, MouseEventArgs e)
        {
            TextBlock t = e.Source as TextBlock;
            Skill skill = skillList.GetSkill(t.Text);
            if (skill != null)
            {
                att.SetSkillSlot(skill, skillSlot);
                SkillCustomizePage sCustomizePage = new SkillCustomizePage(t.Text);
                NavigationService.Navigate(sCustomizePage);
            }
        }

    }
}
