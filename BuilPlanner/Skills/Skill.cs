using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;


namespace BuilPlanner
{
    public class Skill
    {
        protected double cooldown;
        protected double actionTime;
        protected int manaCost = 0;
        protected int staminaCost = 0;
        protected int healthCost = 0;
        protected string description;
        protected string name;
        public enum SkillClassification { None, Curse, Warcry, Banner, Enchantment, Hex, Weapon, Spell, Stance}
        public enum StatusEffect { Burn, Chill, Poison, Blind, Stun, Bleed, Stagger, Interrupt, Slow, Immobilize, Confussion, Hinder, Weakness }
        protected List<SkillClassification> SkillClass = new List<SkillClassification>();

        protected List<string> skillAffixes = new List<string>();

        public string GetDescription()
        {
            return description;
        }

        public string GetName()
        {
            return name;
        }

        public Brush GetSkillColor()
        {
            if (manaCost <= 0 && staminaCost > 0)
            {
                return Brushes.Yellow;
            }
            if (manaCost > 0 && staminaCost <= 0)
            {
                return Brushes.SkyBlue;
            }
            if (healthCost > 0)
            {
                return Brushes.Red;
            }
            if (manaCost > 0 && staminaCost > 0)
            {
                return Brushes.LightGreen;
            }
            return Brushes.White;
        }

        public int GetManaCost()
        {
            return manaCost;
        }

        public int GetStaminaCost()
        {
            return staminaCost;
        }

        public int GetHealthCost()
        {
            return healthCost;
        }

        public double GetCooldown()
        {
            return cooldown;
        }

        public double GetActionTime()
        {
            return actionTime;
        }

        public List<string> GetAffixList()
        {
            return skillAffixes;
        }
            

    }
}
