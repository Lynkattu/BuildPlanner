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
    /// Interaction logic for SkillCustomizePage.xaml
    /// </summary>
    public partial class SkillCustomizePage : Page
    {
        readonly Skills.SkillList sl = new Skills.SkillList();
        private readonly Attributes att = new Attributes();
        private SkillToolTip skillTooltip = new SkillToolTip();
        private bool isEntered = false;
        private Skills.SkillList skillList = new Skills.SkillList();
        private SkillSearchPage ssp = new SkillSearchPage();
        private int skillSlot;
        public SkillCustomizePage(string skillName)
        {
            InitializeComponent();
            List<string> skillAffList = sl.GetSkill(skillName).GetAffixList();

            foreach (string s in skillAffList)
            {
                TextBlock skillAffixes = new TextBlock
                {
                    Text = s,
                    FontSize = 16,
                    Foreground = Brushes.White
                };
                sp_skillCustomize.Children.Add(skillAffixes);
            }
            
        }
        private void Tb_SkillTooltip_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isEntered)
            {
                TextBlock t = e.Source as TextBlock;
                skillTooltip.CreateTooltip(t.Text);
                Canvas.SetLeft(skillTooltip.GetTooltip(), Mouse.GetPosition(ssp.Can_SkillSearch).X + 20);
                Canvas.SetTop(skillTooltip.GetTooltip(), Mouse.GetPosition(ssp.Can_SkillSearch).Y - 10);
                ssp.Can_SkillSearch.Children.Add(skillTooltip.GetTooltip());
                isEntered = true;
            }
        }

        private void Tb_SkillTooltip_MouseMove(object sender, MouseEventArgs e)
        {
            if (isEntered)
            {
                Canvas.SetLeft(skillTooltip.GetTooltip(), Mouse.GetPosition(ssp.Can_SkillSearch).X + 20);
                Canvas.SetTop(skillTooltip.GetTooltip(), Mouse.GetPosition(ssp.Can_SkillSearch).Y - 10);
            }
        }

        private void Tb_SkillTooltip_MouseLeave(object sender, MouseEventArgs e)
        {
            isEntered = false;
            skillTooltip.ClearTooltipChildens();
            ssp.Can_SkillSearch.Children.Remove(skillTooltip.GetTooltip());
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
