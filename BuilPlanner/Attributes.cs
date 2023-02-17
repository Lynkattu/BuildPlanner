using System;
using System.Collections.Generic;
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
        private static Dictionary<ItemSlot, Weapon> MainWeaponSet = new Dictionary<ItemSlot, Weapon>();
        private static Dictionary<ItemSlot, Weapon> SecondaryWeaponSet = new Dictionary<ItemSlot, Weapon>();

        public static Dictionary<ItemSlot, Armor> EquipArmor = new Dictionary<ItemSlot, Armor>();
        public static Dictionary<ItemSlot, Jewelry> EquipJewelry = new Dictionary<ItemSlot, Jewelry>();

        private Skills.SkillList skillList = new Skills.SkillList();
        private static List<Skill> skills = new List<Skill>()
        {
            null,
            null,
            null,
            null,
            null,
            null
        };

        public enum Attribute { Strength, Dexterity, Intelligence, Willpower, Constitution, Endurance, Agility, Wisdom, Charisma }
        public enum Origin { Artist, Believer, Criminal, Occultist, Primitive, Scholar }
        public enum AttributeStat { Equipload, Mending, Thoughness, Poise, Resilence, Concentration, Perspicacious, Penetration, Alacrity, Precision, Life, Mana, Stamina, Haste, Quickness, Efficiency, Block, Dodge }
        public enum Profession { Warrior, Hunter, Magician, Knight, Gladiator, Bandit, Archeologist, Thief, Elementalist, Warden, Cleric }

        private Dictionary<AttributeStat, double> attributeStats = new Dictionary<AttributeStat, double>
        {
            { AttributeStat.Equipload, 0 },
            { AttributeStat.Thoughness, 0 },
            { AttributeStat.Dodge, 0 },
            { AttributeStat.Precision, 0 },
            { AttributeStat.Mending, 0 },
            { AttributeStat.Mana, 0 },
            { AttributeStat.Efficiency, 0 },
            { AttributeStat.Concentration, 0 },
            { AttributeStat.Life, 0 },
            { AttributeStat.Block, 0 },
            { AttributeStat.Stamina, 0 },
            { AttributeStat.Poise, 0 },
            { AttributeStat.Haste, 0 },
            { AttributeStat.Quickness, 0 },
            { AttributeStat.Penetration, 0 },
            { AttributeStat.Alacrity, 0 },
            { AttributeStat.Resilence, 0 },
            { AttributeStat.Perspicacious, 0 },

        };

        public enum Background { Bard, Dancer, Jester, Mobed, Monk, Priest, Assassin, Pirate, Trickster, Necromancer, Warlock, Sorcerer, Druid, Hofgothi, Shaman, Artificer, Alchemist, Mage }

        private readonly ItemList itemList = new ItemList();
        private static Origin origin = Origin.Artist;
        private static Background background = Background.Bard;
        private static bool init = true;
        private static Profession profession1 = Profession.Warrior;
        private static Profession profession2 = Profession.Hunter;

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

        public Dictionary<Attribute, int> GetBaseAttributes(Background background)
        {
            Dictionary<Attribute, int> baseAttributeDict = new Dictionary<Attribute, int>();
            switch (background)
            {
                case Background.Bard:
                    baseAttributeDict = new Dictionary<Attribute, int>()
                    {
                        { Attribute.Strength, 11 },
                        { Attribute.Dexterity, 15 },
                        { Attribute.Intelligence, 12 },
                        { Attribute.Willpower, 10 },
                        { Attribute.Constitution, 11 },
                        { Attribute.Endurance, 10 },
                        { Attribute.Agility, 11 },
                        { Attribute.Wisdom, 15 },
                        { Attribute.Charisma, 18 },
                    };
            break;
    }
            return baseAttributeDict;
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

        public void SetOrigin(Origin originName )
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

        public Origin GetOrigin()
        {
            return origin;
        }

        public void SetBackground(Background bground)
        {
            background = bground;
        }

        public Background GetBackground()
        {
            return background;
        }

        public Profession GetProfession(bool isProfession1)
        {
            return isProfession1 ? profession1 : profession2;
        }

        private void UpdateAttributeStats()
        {
            attributeStats[AttributeStat.Thoughness] = attributes[Attribute.Strength] * 0.06;
            attributeStats[AttributeStat.Equipload] = attributes[Attribute.Strength] * 0.07;
            attributeStats[AttributeStat.Dodge] = attributes[Attribute.Dexterity] * 0.05;
            attributeStats[AttributeStat.Precision] = attributes[Attribute.Dexterity] * 0.05;
            attributeStats[AttributeStat.Mending] = attributes[Attribute.Intelligence] * 0.06;
            attributeStats[AttributeStat.Mana] = attributes[Attribute.Intelligence] * 0.07;
            attributeStats[AttributeStat.Efficiency] = attributes[Attribute.Willpower] * 0.05;
            attributeStats[AttributeStat.Concentration] = attributes[Attribute.Willpower] * 0.06;
            attributeStats[AttributeStat.Life] = attributes[Attribute.Constitution] * 0.06;
            attributeStats[AttributeStat.Block] = attributes[Attribute.Constitution] * 0.05;
            attributeStats[AttributeStat.Stamina] = attributes[Attribute.Endurance] * 0.07;
            attributeStats[AttributeStat.Poise] = attributes[Attribute.Endurance] * 0.06;
            attributeStats[AttributeStat.Haste] = attributes[Attribute.Agility] * 0.04;
            attributeStats[AttributeStat.Quickness] = attributes[Attribute.Agility] * 0.05;
            attributeStats[AttributeStat.Penetration] = attributes[Attribute.Wisdom] * 0.05;
            attributeStats[AttributeStat.Alacrity] = attributes[Attribute.Wisdom] * 0.04;
            attributeStats[AttributeStat.Resilence] = attributes[Attribute.Charisma] * 0.06;
            attributeStats[AttributeStat.Perspicacious] = attributes[Attribute.Charisma] * 0.05;
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
        
        public Dictionary<string, int> GetBaseAttributes(string background)
        {
            //40 attrib difference
            switch(background)
            {
                case "Bard":
                    return GetAttribDict(1,10,2,1,2,0,2,5,15);

                case "Painter":
                    return GetAttribDict(0, 8, 3, 8, 1, 0, 2, 16, 2);
            }
            return null;
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
