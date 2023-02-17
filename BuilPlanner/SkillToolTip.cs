using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BuilPlanner
{
    public class SkillToolTip
    {
        private readonly Skills.SkillList skills = new Skills.SkillList();
        private Skill skill = new Skill();

        private readonly StackPanel tooltip;

        public SkillToolTip()
        {
            tooltip = new StackPanel()
            {
                Width = 250
            };
            tooltip.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.6 };
        }

        public void CreateTooltip(string skillName, List<string> skillCustomize = null)
        {
            List<Skill> skillList = skills.GetSkillList();
            foreach (Skill ability in skillList)
            {
                if (ability.GetName() == skillName)
                {
                    skill = ability;
                    break;
                }
            }

            TextBlock tb_skillname = new TextBlock()
            {
                Text = skill.GetName(),
                Margin = new Thickness(5,5,5,-5),
                FontSize = 16,
                TextAlignment = TextAlignment.Center,
                Foreground = skill.GetSkillColor()
            };
            tooltip.Children.Add(tb_skillname);

            Label lb_Resource = new Label()
            {
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            tooltip.Children.Add(lb_Resource);

            StackPanel sp_ResourceCost = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            lb_Resource.Content = sp_ResourceCost;

            if (skill.GetManaCost() > 0)
            {
                TextBlock tb_ManaCost = new TextBlock()
                {
                    Text = "Mana: " + skill.GetManaCost().ToString(),
                    Margin = new Thickness(5, 5, 5, -5),
                    Foreground = Brushes.SkyBlue,
                    FontSize = 14
                };
                sp_ResourceCost.Children.Add(tb_ManaCost);
            }

            if (skill.GetStaminaCost() > 0)
            {
                TextBlock tb_StaminaCost = new TextBlock()
                {
                    Text = "Stamina: " + skill.GetStaminaCost().ToString(),
                    Margin = new Thickness(5, 5, 5, -5),
                    Foreground = Brushes.Yellow,
                    FontSize = 14
                };
                sp_ResourceCost.Children.Add(tb_StaminaCost);
            }

            if (skill.GetHealthCost() > 0)
            {
                TextBlock tb_HealthCost = new TextBlock()
                {
                    Text = "Health: " + skill.GetHealthCost().ToString(),
                    Margin = new Thickness(5, 5, 5, -5),
                    Foreground = Brushes.Red,
                    FontSize = 14
                };
                sp_ResourceCost.Children.Add(tb_HealthCost);
            }

            Label lb_generalInfo = new Label()
            {
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            tooltip.Children.Add(lb_generalInfo);

            StackPanel sp_GeneralInfo = new StackPanel()
            {
                Orientation = Orientation.Horizontal           
            };
            lb_generalInfo.Content = sp_GeneralInfo;

            if (skill.GetCooldown() > 0)
            {
                TextBlock tb_Cooldown = new TextBlock()
                {
                    Text = "Cooldown: " + skill.GetCooldown().ToString(),
                    Margin = new Thickness(5, 0, 5, 0),
                    Foreground = Brushes.MediumSpringGreen,
                    FontSize = 14
                };
                sp_GeneralInfo.Children.Add(tb_Cooldown);
            }

            if (skill.GetActionTime() > 0)
            {
                TextBlock tb_ActionTime = new TextBlock()
                {
                    Text = "Action Time: " + skill.GetActionTime().ToString(),
                    Margin = new Thickness(5, 0, 5, 0),
                    Foreground = Brushes.Orange,
                    FontSize = 14
                };
                sp_GeneralInfo.Children.Add(tb_ActionTime);
            }

            TextBlock tb_Desription = new TextBlock()
            {
                Text = skill.GetDescription(),
                Foreground = Brushes.White,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center
            };
            tooltip.Children.Add(tb_Desription);
        }


        public StackPanel GetTooltip()
        {
            return tooltip;
        }

        public void ClearTooltipChildens()
        {
            tooltip.Children.Clear();
        }
    }
}
