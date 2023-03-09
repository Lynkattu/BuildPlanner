using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilPlanner.Skills
{
    public class SkillList
    {
        private readonly List<Skill> dancerSkills = new List<Skill>()
        {
            new BladeDance()
        };

        private readonly List<Skill> bardSkills = new List<Skill>()
        {
            new PreludeOfHealing(),

        };

        private readonly List<Skill> painterSkills = new List<Skill>()
        {
            new ClayGolem(),
        };

        private readonly List<Skill> mobedSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> monkSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> priestSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> assassinSkills = new List<Skill>()
        {
            new ShadowStrike()
        };

        private readonly List<Skill> pirateSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> tricksterSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> necromancerSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> sorcererSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> warlockSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> druidSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> hofgothiSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> shamanSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> alchemistSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> artificerSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> mageSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> artistSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> believerSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> criminalSkills = new List<Skill>()
        {
            new Stealth()
        };

        private readonly List<Skill> occultistSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> primitiveSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> scholarSkills = new List<Skill>()
        {

        };

        private readonly List<Skill> allSkills = new List<Skill>()
        {
            new BladeDance(),
            new PreludeOfHealing(),
            new ClayGolem(),
            new Stealth(),
            new ShadowStrike()
        };

        public List<Skill> GetSkillList(Lib.Background backgroundSkills)
        {
            switch (backgroundSkills)
            {
                case Lib.Background.Dancer:
                    return dancerSkills;
                case Lib.Background.Bard:
                    return bardSkills;
                case Lib.Background.Jester:
                    return painterSkills;
                case Lib.Background.Mobed:
                    return mobedSkills;
                case Lib.Background.Monk:
                    return monkSkills;
                case Lib.Background.Priest:
                    return priestSkills;
                case Lib.Background.Assassin:
                    return assassinSkills;
                case Lib.Background.Pirate:
                    return pirateSkills;
                case Lib.Background.Thief:
                    return tricksterSkills;
                case Lib.Background.Necromancer:
                    return necromancerSkills;
                case Lib.Background.Warlock:
                    return warlockSkills;
                case Lib.Background.Sorcerer:
                    return sorcererSkills;                    
                case Lib.Background.Druid:
                    return druidSkills;
                case Lib.Background.Hofgothi:
                    return hofgothiSkills;
                case Lib.Background.Shaman:
                    return shamanSkills;
                case Lib.Background.Artificer:
                    return alchemistSkills;
                case Lib.Background.Alchemist:
                    return artificerSkills;
                case Lib.Background.Wizard:
                    return mageSkills;
                default:
                    return allSkills;
            }
        }

        public List<Skill> GetSkillList(Lib.Origin originSkills)
        {
            switch (originSkills)
            {
                case Lib.Origin.Artist:
                    return artistSkills;
                case Lib.Origin.Believer:
                    return believerSkills;
                case Lib.Origin.Criminal:
                    return criminalSkills;
                case Lib.Origin.Occultist:
                    return occultistSkills;
                case Lib.Origin.Primitive:
                    return primitiveSkills;
                case Lib.Origin.Scholar:
                    return scholarSkills;
                default:
                    return null;
            }
        }
        public List<Skill> GetSkillList()
        {
            return allSkills;
        }

        public Skill GetSkill(string name)
        {
            foreach (Skill skill in allSkills)
            {
                if (skill.GetName() == name) return skill;
            }
            return null;
        }

    }


    public class BladeDance : Skill
    {
        int attackCount = 5;
        double swingTime = 0.36;

        double damagePerAttack = 65;

        public BladeDance()
        {
            name = "Blade Dance";
            actionTime = attackCount * swingTime;
            cooldown = 12;
            staminaCost = 35;
            description = "Attack " + attackCount + " times over " + actionTime + " seconds each attack deal " + damagePerAttack + "% of weapon damage to all nearby enemies, block all incoming damage while attacking.";
        }
    }

    public class PreludeOfHealing : Skill
    {


        double healingTickCount = 8;
        double healingTickTime = 1;
        double healingPower = 60;
        

        public PreludeOfHealing(List<string> skillCustomizationList = null)
        {
            name = "Prelude of Healing";
            actionTime = 1.5;
            cooldown = 30;
            manaCost = 50;

            skillAffixes = new List<string>
            {
                "Extended",
                "Channelled",
                "Rejuvenation"
            };

            if (skillCustomizationList != null)
                foreach (string s in skillCustomizationList)
                {
                    switch (s)
                    {
                        case "Extended":
                            healingTickCount += 2;
                            manaCost += 15;
                            break;

                        default:
                            break;
                    }
                }

            description = "Heal caster and all allies in range by " + healingPower + "% of caster's spell power every " + healingTickTime + " seconds for " + (healingTickTime * healingTickCount) + " seconds.";
        }
    }

    public class ClayGolem : Skill
    {     
        public ClayGolem()
        {
            name = "Clay Golem";
            actionTime = 2;
            cooldown = 45;
            manaCost = 100;
            staminaCost = 30;
            description = "Summon clay golem with life equal to 150% of your caster life and damage equal to 40% of caster spell and weapon damage";
        }
    }

    public class Stealth: Skill
    {
        double duration = 4;
        double heal = 2;
        public Stealth()
        {
            name = "Stealth";
            cooldown = 45;
            actionTime = 0.5;
            staminaCost = 40;
            
            description = "Became invisible for " + duration + " seconds, attacking remove invisibility. Resotre " + heal + "% health every second while invisible.";
        }
    }

    public class ShadowStrike: Skill
    {
        double weaponDamage = 145;
        double damageovertimeDamage = 180;
        double duration = 6;
        public ShadowStrike()
        {
            name = "Shadow Strike";
            staminaCost = 15;
            manaCost = 10;
            actionTime = 0.8;

            description = "Deal " + weaponDamage + "% weapon damage to target enemy and " + damageovertimeDamage + "% of spell damage over " + duration + " seconds. Damage over time can stack multiple times.";
        }
    }
}
