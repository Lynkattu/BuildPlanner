using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilPlanner
{
    public class Attributes
    {
        public enum ItemSlot { Chest, Hands, Legs, Head, Shoulder, Feet, OffHand, MainHand, Ring, Necklace, Charm }

        public static Dictionary<ItemSlot, Weapon> EquipWeapon = new Dictionary<ItemSlot, Weapon>();

        private static bool isMainWeaponSet = true;
        private static readonly Dictionary<ItemSlot, Weapon> MainWeaponSet = new Dictionary<ItemSlot, Weapon>();
        private static readonly Dictionary<ItemSlot, Weapon> SecondaryWeaponSet = new Dictionary<ItemSlot, Weapon>();

        public static Dictionary<ItemSlot, Armor> EquipArmor = new Dictionary<ItemSlot, Armor>();
        public static Dictionary<ItemSlot, Jewelry> EquipJewelry = new Dictionary<ItemSlot, Jewelry>();

        private readonly Skills.SkillList skillList = new Skills.SkillList();
        private static List<Skill> skills = new List<Skill>()
        {
            null,
            null,
            null,
            null,
            null,
            null
        };
        private readonly Dictionary<Lib.Background, string> backgroundImages = new Dictionary<Lib.Background, string>
        {
            {Lib.Background.Alchemist, "BuilPlanner;component/Images/Alchemist.png"},
            {Lib.Background.Bard, "BuilPlanner;component/Images/Bard.png"},
            {Lib.Background.Dancer, "BuilPlanner;component/Images/Dancer.png"},
            {Lib.Background.Jester, "BuilPlanner;component/Images/Jester.png"},
            {Lib.Background.Artificer, "BuilPlanner;component/Images/Artificer.png"},
            {Lib.Background.Wizard, "BuilPlanner;component/Images/Wizard.png"},
            {Lib.Background.Thief, "BuilPlanner;component/Images/Thief.png"},
            {Lib.Background.Pirate, "BuilPlanner;component/Images/Pirate.png"},
            {Lib.Background.Assassin, "BuilPlanner;component/Images/Assassin.png"},
            {Lib.Background.Mobed, "BuilPlanner;component/Images/Mobed.png"},
            {Lib.Background.Monk, "BuilPlanner;component/Images/Monk.png"},
            {Lib.Background.Priest, "BuilPlanner;component/Images/Priest.png"},
            {Lib.Background.Necromancer, "BuilPlanner;component/Images/Necromancer.png"},
            {Lib.Background.Sorcerer, "BuilPlanner;component/Images/Sorcerer.png"},
            {Lib.Background.Warlock, "BuilPlanner;component/Images/Warlock.png"},
            {Lib.Background.Druid, "BuilPlanner;component/Images/Druid.png"},
            {Lib.Background.Hofgothi, "BuilPlanner;component/Images/Hofgothi.png"},
            {Lib.Background.Shaman, "BuilPlanner;component/Images/Shaman.png"},
        };
        public enum Attribute { Strength, Dexterity, Intelligence, Willpower, Constitution, Endurance, Agility, Wisdom, Charisma }
        public enum AttributeStat { Tenacity, Psyche, Vigor, Sturdiness, Fitness, Perseverance, Spirit, Resistance, Ferocity, StrWeaponPower, DexWeaponPower, IntWeaponPower, Lifting, Convalescent, Thoughness, Resilience, Concentration, Perspicacious, Wits, Alacrity, Precision, Life, Mind, Stamina, Quickness, Efficiency, Block }

        private Dictionary<AttributeStat, double> attributeStats = new Dictionary<AttributeStat, double>
        {
            { AttributeStat.StrWeaponPower, 0 },
            { AttributeStat.DexWeaponPower, 0 },
            { AttributeStat.IntWeaponPower, 0 },
            { AttributeStat.Fitness, 0 },
            { AttributeStat.Wits, 0 },
            { AttributeStat.Spirit, 0 },
            { AttributeStat.Lifting, 0 },
            { AttributeStat.Vigor, 0 },
            { AttributeStat.Psyche, 0 },
            { AttributeStat.Sturdiness, 0 },
            { AttributeStat.Thoughness, 0 },
            { AttributeStat.Tenacity, 0 },
            { AttributeStat.Precision, 0 },
            { AttributeStat.Convalescent, 0 },
            { AttributeStat.Mind, 0 },
            { AttributeStat.Efficiency, 0 },
            { AttributeStat.Concentration, 0 },
            { AttributeStat.Life, 0 },
            { AttributeStat.Block, 0 },
            { AttributeStat.Stamina, 0 },
            { AttributeStat.Perseverance, 0 },
            { AttributeStat.Quickness, 0 },
            { AttributeStat.Alacrity, 0 },
            { AttributeStat.Resilience, 0 },
            { AttributeStat.Perspicacious, 0 },
            { AttributeStat.Resistance, 0 },
            { AttributeStat.Ferocity, 0 }
        };

        private readonly ItemList itemList = new ItemList();
        private static Lib.Origin origin = Lib.Origin.Artist;
        private static Lib.Background background = Lib.Background.Bard;
        private static bool init = true;

        private static int level = 1;
        private static int remainAttributes = 0;
        private static Dictionary<Attribute, int> baseAttributes = new Dictionary<Attribute, int>();

        private static Dictionary<Attribute, int> attributes = new Dictionary<Attribute, int>()
        {
            { Attribute.Strength, 10 },
            { Attribute.Dexterity, 10 },
            { Attribute.Intelligence, 10 },
            { Attribute.Willpower, 10 },
            { Attribute.Constitution, 10 },
            { Attribute.Endurance, 10 },
            { Attribute.Agility, 10 },
            { Attribute.Wisdom, 10 },
            { Attribute.Charisma, 10 },
        };
       
        public Attributes()
        {
            baseAttributes = GetBaseAttributes(background);
            if (init)
            {
                ResetAtrributes();
                EquipArmor.Add(ItemSlot.Chest, null);
                EquipArmor.Add(ItemSlot.Hands, null);
                EquipArmor.Add(ItemSlot.Legs, null);
                EquipArmor.Add(ItemSlot.Head, null);
                EquipArmor.Add(ItemSlot.Shoulder, null);
                EquipArmor.Add(ItemSlot.Feet, null);

                EquipWeapon.Add(ItemSlot.MainHand, null);
                EquipWeapon.Add(ItemSlot.OffHand, null);

                MainWeaponSet.Add(ItemSlot.MainHand, null);
                MainWeaponSet.Add(ItemSlot.OffHand, null);

                SecondaryWeaponSet.Add(ItemSlot.MainHand, null);
                SecondaryWeaponSet.Add(ItemSlot.OffHand, null);

                EquipJewelry.Add(ItemSlot.Necklace, null);
                EquipJewelry.Add(ItemSlot.Ring, null);
                EquipJewelry.Add(ItemSlot.Charm, null);

                UpdateAttributeStats();
                init = false;
            }      
        }

        public Dictionary<Attribute, int> GetBaseAttributes(Lib.Background background)
        {
            Dictionary<Attribute, int> baseAttributeDict = new Dictionary<Attribute, int>();
            switch (background)
            {
                case Lib.Background.Bard:
                    baseAttributeDict = new Dictionary<Attribute, int>()
                    {
                        { Attribute.Strength, 11 },
                        { Attribute.Dexterity, 22 },
                        { Attribute.Intelligence, 12 },
                        { Attribute.Willpower, 14 },
                        { Attribute.Constitution, 15 },
                        { Attribute.Endurance, 13 },
                        { Attribute.Agility, 11 },
                        { Attribute.Wisdom, 16 },
                        { Attribute.Charisma, 24 },
                    };
                    break;
                case Lib.Background.Dancer:
                    baseAttributeDict = new Dictionary<Attribute, int>()
                    {
                        { Attribute.Strength, 13 },
                        { Attribute.Dexterity, 18 },
                        { Attribute.Intelligence, 10 },
                        { Attribute.Willpower, 14 },
                        { Attribute.Constitution, 15 },
                        { Attribute.Endurance, 19 },
                        { Attribute.Agility, 24 },
                        { Attribute.Wisdom, 12 },
                        { Attribute.Charisma, 15 },
                    };
                    break;
    }
            return baseAttributeDict;
        }

        public string GetBackgroundImage()
        {
            return backgroundImages[GetBackground()];
        }

        public void UpdateAttributes(Dictionary<Attribute, int> att)
        {
            attributes = att;
            UpdateAttributeStats();
        }

        public Dictionary<Attribute, int> GetAttributes()
        {
            return attributes;
        }

        public Dictionary<Attribute, int> GetBaseAttributes()
        {
            return baseAttributes;
        }

        public void ResetAtrributes()
        {
            foreach (KeyValuePair<Attribute, int> e in baseAttributes)
            {
                attributes[e.Key] = baseAttributes[e.Key];
            }
            level = 1;
            remainAttributes = 0;
            UpdateAttributeStats();
        }

        public int GetLevel()
        {
            return level;
        }

        public int GetRemainAttributes()
        {
            return remainAttributes;
        }

        public void SetLevel(int newLevel)
        {
            level = newLevel;
        }

        public void SetRemainAttributes(int newRemain)
        {
           remainAttributes = newRemain;
        }

        public void SetOrigin(Lib.Origin originName )
        {
            //Remove slotted origin and background skills from character
            List<Skill> originSkills = skillList.GetSkillList(origin);
            List<Skill> backgroundSkills = skillList.GetSkillList(background);

            for ( int i = 0; i < skills.Count(); i++)
            {
                foreach (Skill s in originSkills)
                {
                    if (skills[i] != null)
                        if (s.GetName() == skills[i].GetName()) skills[i] = null;
                }

                foreach (Skill s in backgroundSkills)
                {
                    if (skills[i] != null)
                        if (s.GetName() == skills[i].GetName()) skills[i] = null;
                }
            }
            //set origin
            origin = originName;
        }

        public Lib.Origin GetOrigin()
        {
            return origin;
        }

        public void SetBackground(Lib.Background bground)
        {
            background = bground;
        }

        public Lib.Background GetBackground()
        {
            return background;
        }

        private void UpdateAttributeStats()
        {
            attributeStats[AttributeStat.StrWeaponPower] = attributes[Attribute.Strength] * 0.3;
            attributeStats[AttributeStat.Thoughness] = attributes[Attribute.Strength] * 0.08;
            attributeStats[AttributeStat.Lifting] = attributes[Attribute.Strength] * 0.07;

            attributeStats[AttributeStat.DexWeaponPower] = attributes[Attribute.Dexterity] * 0.3;
            attributeStats[AttributeStat.Tenacity] = attributes[Attribute.Dexterity] * 0.06;
            attributeStats[AttributeStat.Precision] = attributes[Attribute.Dexterity] * 0.05;

            attributeStats[AttributeStat.IntWeaponPower] = attributes[Attribute.Intelligence] * 0.3;
            attributeStats[AttributeStat.Wits] = attributes[Attribute.Intelligence] * 0.08;
            attributeStats[AttributeStat.Mind] = attributes[Attribute.Intelligence] * 0.07;

            attributeStats[AttributeStat.Efficiency] = attributes[Attribute.Willpower] * 0.07;
            attributeStats[AttributeStat.Concentration] = attributes[Attribute.Willpower] * 0.08;
            attributeStats[AttributeStat.Psyche] = attributes[Attribute.Willpower] * 0.1;


            attributeStats[AttributeStat.Life] = attributes[Attribute.Constitution] * 0.06;
            attributeStats[AttributeStat.Block] = attributes[Attribute.Constitution] * 0.05;
            attributeStats[AttributeStat.Convalescent] = attributes[Attribute.Constitution] * 0.09;


            attributeStats[AttributeStat.Stamina] = attributes[Attribute.Endurance] * 0.07;
            attributeStats[AttributeStat.Sturdiness] = attributes[Attribute.Endurance] * 0.06;
            attributeStats[AttributeStat.Vigor] = attributes[Attribute.Endurance] * 0.2;

            attributeStats[AttributeStat.Fitness] = attributes[Attribute.Agility] * 0.2;
            attributeStats[AttributeStat.Perseverance] = attributes[Attribute.Agility] * 0.04;
            attributeStats[AttributeStat.Quickness] = attributes[Attribute.Agility] * 0.05;

            attributeStats[AttributeStat.Spirit] = attributes[Attribute.Wisdom] * 0.05;
            attributeStats[AttributeStat.Alacrity] = attributes[Attribute.Wisdom] * 0.04;
            attributeStats[AttributeStat.Resistance] = attributes[Attribute.Wisdom] * 0.2;


            attributeStats[AttributeStat.Resilience] = attributes[Attribute.Charisma] * 0.06;
            attributeStats[AttributeStat.Perspicacious] = attributes[Attribute.Charisma] * 0.05;
            attributeStats[AttributeStat.Ferocity] = attributes[Attribute.Charisma] * 0.1;

        }

        public double GetAttributeStat(AttributeStat stat)
        {
            return attributeStats[stat];
        }

        private Dictionary<string, int> GetAttribDict(int strength, int dexterity,int intelligence, int willpower, int constitution, int endurance, int agility, int wisdom, int charisma)
        {
            Dictionary<string, int> attributes = new Dictionary<string, int>()
            {
                { "strength", 10 + strength},
                { "dexterity", 10 + dexterity},
                { "intelligence", 10 + intelligence},
                { "willpower", 10 + willpower},
                { "constitution" , 10 + constitution},
                { "endurance", 10 + endurance},
                { "agility", 10 + agility},
                { "wisdom", 10 + wisdom},
                { "charisma", 10 + charisma},
            };
            return attributes;
        }
        

        public void AddEquipment(string itemName, bool isMainHand = false)
        {
            bool isFound = false;
            foreach (Armor i in itemList.GetArmorList())
            {
                if (i.GetItemName() == itemName)
                {
                    Armor armor = i;
                    switch(i.GetArmorSlot())
                    {
                        case Armor.ArmorSlot.Chest:
                            EquipArmor[ItemSlot.Chest] = i;
                            break;
                        case Armor.ArmorSlot.Feet:
                            EquipArmor[ItemSlot.Feet] = i;
                            break;
                        case Armor.ArmorSlot.Legs:
                            EquipArmor[ItemSlot.Legs] = i;
                            break;
                        case Armor.ArmorSlot.Head:
                            EquipArmor[ItemSlot.Head] = i;
                            break;
                        case Armor.ArmorSlot.Hands:
                            EquipArmor[ItemSlot.Hands] = i;
                            break;
                        case Armor.ArmorSlot.Shoulder:
                            EquipArmor[ItemSlot.Shoulder] = i;
                            break;
                    }
                    isFound = true;
                }
            }
            if (!isFound)
            {
                foreach (Weapon i in itemList.GetWeaponList())
                {
                    if (i.GetItemName() == itemName)
                    {
                        Weapon weapon = i;
                        isFound = true;                        
                        if (isMainHand)
                        {
                            EquipWeapon[ItemSlot.MainHand] = i;
                            if (isMainWeaponSet)
                            {
                                MainWeaponSet[ItemSlot.MainHand] = i;
                                if (MainWeaponSet[ItemSlot.MainHand].GetWeaponSlot() == Weapon.WeaponSlot.TwoHand) MainWeaponSet[ItemSlot.OffHand] = null;
                            }
                            else
                            {
                                SecondaryWeaponSet[ItemSlot.MainHand] = i;
                                if (SecondaryWeaponSet[ItemSlot.MainHand].GetWeaponSlot() == Weapon.WeaponSlot.TwoHand) SecondaryWeaponSet[ItemSlot.OffHand] = null;
                            }
                        }
                        else
                        {
                            EquipWeapon[ItemSlot.OffHand] = i;
                            if (isMainWeaponSet)
                            {
                                MainWeaponSet[ItemSlot.OffHand] = i;
                                if (MainWeaponSet[ItemSlot.MainHand].GetWeaponSlot() == Weapon.WeaponSlot.TwoHand) MainWeaponSet[ItemSlot.MainHand] = null;

                            }
                            else
                            {
                                SecondaryWeaponSet[ItemSlot.OffHand] = i;
                                if (SecondaryWeaponSet[ItemSlot.MainHand].GetWeaponSlot() == Weapon.WeaponSlot.TwoHand) SecondaryWeaponSet[ItemSlot.MainHand] = null;
                            }
                        }
                    }
                }
            }

            if (!isFound)
            {
                foreach (Jewelry i in itemList.GetJewelryList())
                {
                    if (i.GetItemName() == itemName)
                    {
                        Jewelry jewelry = i;
                        switch (i.GetJewelrySlot())
                        {
                            case Jewelry.JewelrySlot.Necklace:
                                EquipJewelry[ItemSlot.Necklace] = i;
                                break;
                            case Jewelry.JewelrySlot.Ring:
                                EquipJewelry[ItemSlot.Ring] = i;
                                break;
                            case Jewelry.JewelrySlot.Charm:
                                EquipJewelry[ItemSlot.Charm] = i;
                                break;
                        }
                        isFound = true;
                        break;
                    }
                }
            }
        }

        private Dictionary<ItemSlot, Weapon> WeaponClone(Dictionary<ItemSlot, Weapon> clone)
        {
            Dictionary<ItemSlot, Weapon> newDict = new Dictionary<ItemSlot, Weapon>();
            foreach (KeyValuePair<ItemSlot, Weapon> c in clone)
            {
                newDict.Add(c.Key, c.Value);
            }
            return newDict;
        }

        public void WeaponSwap()
        {
            if (isMainWeaponSet)
            {
                EquipWeapon = WeaponClone(SecondaryWeaponSet);
                isMainWeaponSet = false;
            }
            else
            {
                EquipWeapon = WeaponClone(MainWeaponSet);
                isMainWeaponSet = true;
            }
        }

        public Weapon GetWeapon(ItemSlot itemSlot)
        {
            if (itemSlot == ItemSlot.MainHand || itemSlot == ItemSlot.OffHand)
            {
                return EquipWeapon[itemSlot];
            }
            return null;
        }

        public Weapon GetWeapon(string name, bool isMainHand)
        {
            foreach (KeyValuePair<ItemSlot, Weapon> weapon in EquipWeapon)
            {
                if (weapon.Value != null)
                {
                    if (weapon.Value.GetItemName() == name && weapon.Key == ItemSlot.MainHand && isMainHand)
                    {
                        return weapon.Value;
                    }
                    else if(weapon.Value.GetItemName() == name && weapon.Key == ItemSlot.OffHand && !isMainHand)
                    {
                        return weapon.Value;
                    }
                }
            }
            return null;
        }

        public Weapon GetWeapon(ItemSlot itemSlot, bool isMainWeaponSet)
        {
            if (isMainWeaponSet)
            {
                return MainWeaponSet[itemSlot];
            }
            else
            {
                return SecondaryWeaponSet[itemSlot];
            }
        }

        public void RemoveItem(ItemSlot RemovedItemSlot)
        {
            if (RemovedItemSlot == ItemSlot.MainHand || RemovedItemSlot == ItemSlot.OffHand) EquipWeapon[RemovedItemSlot] = null;
            if (RemovedItemSlot == ItemSlot.Necklace || RemovedItemSlot == ItemSlot.Charm || RemovedItemSlot == ItemSlot.Ring) EquipJewelry[RemovedItemSlot] = null;
            if (RemovedItemSlot == ItemSlot.Chest || RemovedItemSlot == ItemSlot.Feet || RemovedItemSlot == ItemSlot.Shoulder || RemovedItemSlot == ItemSlot.Hands || RemovedItemSlot == ItemSlot.Head || RemovedItemSlot == ItemSlot.Legs) EquipArmor[RemovedItemSlot] = null;
        }

        public Armor GetArmor(ItemSlot itemSlot)
        {
            if (itemSlot == ItemSlot.Chest || itemSlot == ItemSlot.Feet || itemSlot == ItemSlot.Shoulder || itemSlot == ItemSlot.Hands || itemSlot == ItemSlot.Head || itemSlot == ItemSlot.Legs)
            {
                return EquipArmor[itemSlot];
            }
            return null;
        }

        public Armor GetArmor(string name)
        {
            foreach (KeyValuePair<ItemSlot, Armor> armor in EquipArmor)
            {
                if (armor.Value != null)
                {
                    if (armor.Value.GetItemName() == name)
                    {
                        return armor.Value;
                    }
                }
            }
            return null;
        }

        public Dictionary<ItemSlot, Armor> GetAllEquipmentArmor()
        {
            return EquipArmor;
        }

        public Jewelry GetJewelry(ItemSlot itemSlot)
        {
            if ( itemSlot == ItemSlot.Necklace || itemSlot == ItemSlot.Charm || itemSlot == ItemSlot.Ring )
            {
                return EquipJewelry[itemSlot];
            }
            return null;
        }

        public Jewelry GetJewelry(string name)
        {
            foreach (KeyValuePair<ItemSlot, Jewelry> jewelry in EquipJewelry)
            {
                if (jewelry.Value != null)
                {
                    if (jewelry.Value.GetItemName() == name)
                    {
                        return jewelry.Value;
                    }
                }
            }
            return null;
        }

        public Skill GetSkill(int skillSlot)
        {
            if (skillSlot - 1 < skills.Count())
                return skills[skillSlot - 1];
            return null;
        }

        public void SetSkillSlot(Skill skill, int slot, bool isIndex = false)
        {
            int index = slot;
            if (!isIndex)
            {
                index -= 1;
            }
            if (index < skills.Count())
            {
                for (int i = 0; i < skills.Count(); i++)
                {
                    if (skill == skills[i])
                    {
                        skills[i] = null;
                    }
                }
                skills[index] = skill;
            }
        }
    }
}
