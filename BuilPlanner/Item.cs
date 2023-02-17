using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Text;
using System.Threading.Tasks;

namespace BuilPlanner
{
    public class Item
    {
        public Item() { }

        public enum ItemType { Normal, Blessed, Cursed, Runic, Artifact };
        public enum ItemQuality { Novice, Journeyman, Expert, Master}

        public enum Affixes {None, Mending, Thoughness, Poise, Resilence, Power, Concentration, Perspicacious, Penetration, Alacrity, Precision, Ferocity, Life, LifeRegen, LifeSteal, Mana, ManaRegen, Stamina, StaminaRegen, Haste, Quickness, Efficiency, Block, Dodge, Defence }

        //all stat values are in percentages
        public static readonly Dictionary<Affixes, double> itemAffixValues = new Dictionary<Affixes, double>()
        {
            { Affixes.Mending, 2.8 }, { Affixes.Thoughness, 2.8 }, { Affixes.Poise, 3 }, { Affixes.Resilence, 2.8 }, { Affixes.Power, 2 },
            { Affixes.Concentration, 3 }, { Affixes.Perspicacious, 3 }, { Affixes.Penetration, 2.2 }, { Affixes.Alacrity, 2.7 },
            { Affixes.Precision, 3 }, { Affixes.Ferocity, 3 }, { Affixes.Life, 3.5 }, { Affixes.LifeRegen, 0.2 },
            { Affixes.LifeSteal, 1.2 }, { Affixes.Mana, 3.2 }, { Affixes.ManaRegen, 0.4 }, { Affixes.Stamina, 3.2 },
            { Affixes.StaminaRegen, 0.4 }, { Affixes.Haste, 2 }, { Affixes.Quickness, 3 }, { Affixes.Efficiency, 2.7 },
            { Affixes.Block, 2.8 }, { Affixes.Dodge, 2.8 }, { Affixes.Defence, 2.9 },
            {Affixes.None, 0 }
        };

        protected Dictionary<Affixes, double> itemAffixes = new Dictionary<Affixes, double>();
        protected List<Affixes> affixKeyOrder = new List<Affixes>();
        protected List<Rune> runeWords = new List<Rune>();
        protected ItemQuality quality = ItemQuality.Novice;
        protected int affixCount = 6;
        protected int runeCount = 0;
        protected string name;
        protected ItemType it;
        public RuneSet runeSet = new RuneSet();
        protected double weight;

        public Artifact artifact;
        public Blessed blessed;
        public Cursed cursed;

        public Brush GetItemColor()
        {
            switch(it)
            {
                case ItemType.Normal:
                    return Brushes.LightGreen;
                case ItemType.Runic:
                    return Brushes.LightSkyBlue;
                case ItemType.Blessed:
                    return Brushes.Orange;
                case ItemType.Cursed:
                    return Brushes.MediumOrchid;
                case ItemType.Artifact:
                    return Brushes.Red;
            }
            return null;
        }

        protected void CreateRuneWordList()
        {
            if (runeCount >= 1)
            {
                Rune rune = new Rune();
                runeWords.Add(rune);
            }
            if (runeCount >= 2)
            {
                Rune rune = new Rune();
                runeWords.Add(rune);
            }
            if (runeCount >= 3)
            {
                Rune rune = new Rune();
                runeWords.Add(rune);
            }
            if (runeCount >= 4)
            {
                Rune rune = new Rune();
                runeWords.Add(rune);
            }
            if (runeCount >= 5)
            {
                Rune rune = new Rune();
                runeWords.Add(rune);
            }
            if (runeCount >= 6)
            {
                Rune rune = new Rune();
                runeWords.Add(rune);
            }
        }

        public string GetItemName()
        {
            return name;
        }

        public ItemType GetItemType()
        {
            return it;
        }

        public double GetWeight()
        {
            return weight;
        }
        public int GetAffixCount()
        {
            return affixCount;
        }

        public Dictionary<Affixes, double> GetAffixDict()
        {
            return itemAffixes;
        }

        public void UpdateAffixKeyOrder(List<Affixes> list)
        {
            affixKeyOrder = list;
        }

        public List<Affixes> GetAffixKeyOrder()
        {

            return affixKeyOrder;
        }

        public int GetRuneCount()
        {
            return runeCount;
        }
        public int GetCurrentAffixCount()
        {
            return itemAffixes.Count;
        }
        public void AddAffix(Affixes affix, Affixes oldAffix)
        {
            if (GetCurrentAffixCount() <= GetAffixCount())
            {
                if (affix != oldAffix && oldAffix != Affixes.None)
                {
                    if(itemAffixes.ContainsKey(oldAffix))
                    {
                        itemAffixes.Remove(oldAffix);
                        if (affix != Affixes.None)
                        {
                            double affixValue = itemAffixValues[affix];
                            itemAffixes.Add(affix, affixValue);
                            SetItemQuality(quality);
                        }
                        
                    }
                }
                else if (affix != Affixes.None && oldAffix == Affixes.None && !itemAffixes.ContainsKey(affix))
                {
                    double affixValue = itemAffixValues[affix];
                    itemAffixes.Add(affix, affixValue);
                    SetItemQuality(quality);
                }
            }
        }

        public void SetItemQuality(ItemQuality itemQuality)
        {
            quality = itemQuality;

            List<Affixes> tmp = new List<Affixes>();
            foreach (KeyValuePair<Affixes, double> itemValue in itemAffixes)
            {
                tmp.Add(itemValue.Key);                
            }
            for (int i = 0; i < tmp.Count();i++)
            {
                switch (quality)
                {
                    case ItemQuality.Novice:
                        itemAffixes[tmp[i]] = itemAffixValues[tmp[i]];
                        break;
                    case ItemQuality.Journeyman:
                        itemAffixes[tmp[i]] = itemAffixValues[tmp[i]] * 1.1;
                        break;
                    case ItemQuality.Expert:
                        itemAffixes[tmp[i]] = itemAffixValues[tmp[i]] * 1.2;
                        break;
                    case ItemQuality.Master:
                        itemAffixes[tmp[i]] = itemAffixValues[tmp[i]] * 1.3;
                        break;
                }
            }
        }

        public void AddRune(Rune.RuneWord rune, int RuneWordSlot)
        {
            if (runeCount > 0)
            {
                Rune r = new Rune(rune);
                runeWords[RuneWordSlot] = r;
                runeSet.SetRuneSet(runeWords);
            }
            
        }

        public List<Rune> GetRuneWords()
        {
            return runeWords;
        }

        public void SetBlessing(Blessed.Blessing bless)
        {
            blessed = new Blessed(bless);
        }

        public ItemQuality GetQuality()
        {
            return quality;
        }

    }

    public class Armor : Item
    {
        private double stability;
        public enum ArmorSlot { Chest, Hands, Legs, Head, Shoulder, Feet }
        private readonly ArmorSlot armorSlot;
        private readonly int armor; // 0.01% redduction per armor up to 75%
        public Armor() { }

        //Normal
        public Armor(string itemName, ArmorSlot slot, int armorValue, double weightValue)
        {
            name = itemName;
            it = ItemType.Normal;
            armorSlot = slot;
            armor = armorValue;           
            weight = weightValue;
            SetStability();

        }

        //Artifact
        public Armor(string itemName, ArmorSlot slot, int armorValue, double weightValue, Artifact.ArtifactPower ap)
        {
            name = itemName;
            it = ItemType.Artifact;
            armorSlot = slot;
            armor = armorValue;
            weight = weightValue;
            affixCount = 6;
            artifact = new Artifact(ap);
            CreateRuneWordList();
            SetStability();

        }

        //Runic
        public Armor(string itemName, ArmorSlot slot, int armorValue, double weightValue, int runecount)
        {
            name = itemName;
            it = ItemType.Runic;
            armorSlot = slot;
            armor = armorValue;
            weight = weightValue;
            runeCount = runecount;
            affixCount = 6 - runecount;
            CreateRuneWordList();
            SetStability();

        }

        //cursed
        public Armor(string itemName, ArmorSlot slot, int armorValue, double weightValue, Cursed.Curse curse)
        {
            name = itemName;
            it = ItemType.Cursed;
            armorSlot = slot;
            armor = armorValue;
            weight = weightValue;
            affixCount = 5;
            cursed = new Cursed(curse);
            CreateRuneWordList();
            SetStability();
        }

        //blessed
        public Armor(string itemName, ArmorSlot slot, int armorValue, double weightValue, Blessed.Blessing blessing)
        {
            name = itemName;
            it = ItemType.Blessed;
            armorSlot = slot;
            armor = armorValue;
            weight = weightValue;
            affixCount = 3;
            blessed = new Blessed(blessing);
            SetStability();
        }

        private void SetStability()
        {
            stability = weight * 2.5;
        }

        public ArmorSlot GetArmorSlot()
        {
            return armorSlot;
        }
        public int GetArmorValue()
        {
            return armor;
        }

        public double GetStability()
        {
            return stability;
        }

    }
    public class Weapon : Item
    {
        public enum WeaponSlot { OneHand, TwoHand, OffHand, MainHand }
        private WeaponSlot weaponSlot;
        public enum WeaponType { Sword, Mace, Axe, Bow, Spear, Staff, Polearm, Pistol, Rifle, Wand, Focus, Shield, Dagger }
        private double attack;
        private double attackSpeed;
        private WeaponType weaponClass;

        public Weapon() { }

        //Artifact item
        public Weapon(string itemName, WeaponSlot slot, WeaponType type, double attackValue, double attackPerSecond, double weightValue, Artifact.ArtifactPower ap)
        {
            name = itemName;
            it = ItemType.Artifact;
            weaponSlot = slot;
            attack = attackValue;
            attackSpeed = attackPerSecond;
            weight = weightValue;
            weaponClass = type;
            artifact = new Artifact(ap);
            affixCount = 6;
            CreateRuneWordList();

        }

        //Normal item
        public Weapon(string itemName, WeaponSlot slot, WeaponType type, double attackValue, double attackPerSecond, double weightValue)
        {
            name = itemName;
            it = ItemType.Normal;
            weaponSlot = slot;
            attack = attackValue;
            attackSpeed = attackPerSecond;
            weight = weightValue;
            weaponClass = type;
            
        }

        //Runic item
        public Weapon(string itemName, WeaponSlot slot, WeaponType type, double attackValue, double attackPerSecond, double weightValue, int runecount)
        {
            name = itemName;
            it = ItemType.Runic;
            weaponSlot = slot;
            attack = attackValue;
            attackSpeed = attackPerSecond;
            weight = weightValue;
            weaponClass = type;
            runeCount = runecount;
            affixCount -= runecount;
            CreateRuneWordList();

        }

        //Blessed item
        public Weapon(string itemName, WeaponSlot slot, WeaponType type, double attackValue, double attackPerSecond, double weightValue, Blessed.Blessing bless)
        {
            name = itemName;
            it = ItemType.Blessed;
            weaponSlot = slot;
            attack = attackValue;
            attackSpeed = attackPerSecond;
            weight = weightValue;
            weaponClass = type;
            affixCount = 3;
            blessed = new Blessed(bless);
        }

        //Cursed item
        public Weapon(string itemName, WeaponSlot slot, WeaponType type, double attackValue, double attackPerSecond, double weightValue, Cursed.Curse curse)
        {
            name = itemName;
            it = ItemType.Cursed;
            weaponSlot = slot;
            attack = attackValue;
            attackSpeed = attackPerSecond;
            weight = weightValue;
            weaponClass = type;
            affixCount = 5;
            cursed = new Cursed(curse);
            CreateRuneWordList();

        }

        public WeaponSlot GetWeaponSlot()
        {
            return weaponSlot;
        }
        public double GetAttack()
        {
            return attack;
        }
        public double GetAttackSpeed()
        {
            return attackSpeed;
        }

        public WeaponType GetWeaponClass()
        {
            return weaponClass;
        }

        public new void AddAffix(Affixes affix, Affixes oldAffix)
        {
            if (GetCurrentAffixCount() <= GetAffixCount())
            {
                if (affix != oldAffix && oldAffix != Affixes.None)
                {
                    if (itemAffixes.ContainsKey(oldAffix))
                    {
                        itemAffixes.Remove(oldAffix);
                        if (affix != Affixes.None)
                        {
                            double affixValue = itemAffixValues[affix];
                            itemAffixes.Add(affix, affixValue);
                            SetItemQuality(quality);
                        }

                    }
                }
                else if (affix != Affixes.None && oldAffix == Affixes.None && !itemAffixes.ContainsKey(affix))
                {
                    double affixValue = itemAffixValues[affix];
                    itemAffixes.Add(affix, affixValue);
                    SetItemQuality(quality);
                }
            }
        }

        new public void SetItemQuality(ItemQuality itemQuality)
        {
            quality = itemQuality;

            List<Affixes> tmp = new List<Affixes>();
            foreach (KeyValuePair<Affixes, double> itemValue in itemAffixes)
            {
                tmp.Add(itemValue.Key);
            }
            for (int i = 0; i < tmp.Count(); i++)
            {
                switch (quality)
                {
                    case ItemQuality.Novice:
                        itemAffixes[tmp[i]] = weaponSlot == WeaponSlot.TwoHand ? itemAffixValues[tmp[i]] * 2 : itemAffixValues[tmp[i]];
                        break;
                    case ItemQuality.Journeyman:
                        itemAffixes[tmp[i]] = weaponSlot == WeaponSlot.TwoHand ? itemAffixValues[tmp[i]] * 2.2 : itemAffixValues[tmp[i]] * 1.1;
                        break;
                    case ItemQuality.Expert:
                        itemAffixes[tmp[i]] = weaponSlot == WeaponSlot.TwoHand ? itemAffixValues[tmp[i]] * 2.4 : itemAffixValues[tmp[i]] * 1.2;
                        break;
                    case ItemQuality.Master:
                        itemAffixes[tmp[i]] = weaponSlot == WeaponSlot.TwoHand ? itemAffixValues[tmp[i]] * 2.6 : itemAffixValues[tmp[i]] * 1.3;
                        break;
                }
            }
        }
    }

    public class Jewelry : Item
    {
        public enum JewelrySlot { Ring, Necklace, Charm }
        JewelrySlot jewelrySlot;
        public Jewelry() { }

        //Normal
        public Jewelry(string itemName, JewelrySlot slot)
        {
            name = itemName;
            it = ItemType.Normal;
            jewelrySlot = slot;
        }

        //Runic
        public Jewelry(string itemName, JewelrySlot slot, int runecount)
        {
            name = itemName;
            it = ItemType.Runic;
            jewelrySlot = slot;
            runeCount = runecount;
            affixCount -= runeCount;
            CreateRuneWordList();

        }

        //Cursed
        public Jewelry(string itemName, JewelrySlot slot, Cursed.Curse curse)
        {
            name = itemName;
            it = ItemType.Cursed;
            jewelrySlot = slot;
            affixCount = 5;
            cursed = new Cursed(curse);
            CreateRuneWordList();

        }

        //Blessed
        public Jewelry(string itemName, JewelrySlot slot, Blessed.Blessing blessing)
        {
            name = itemName;
            it = ItemType.Blessed;
            jewelrySlot = slot;
            affixCount = 3;
            blessed = new Blessed(blessing);
        }

        //Artifact
        public Jewelry(string itemName, JewelrySlot slot, Artifact.ArtifactPower ap)
        {
            name = itemName;
            it = ItemType.Artifact;
            jewelrySlot = slot;
            artifact = new Artifact(ap);
            CreateRuneWordList();

        }

        public JewelrySlot GetJewelrySlot()
        {
            return jewelrySlot;
        }
    }

    public class Artifact
    {
        private string description;
        public enum ArtifactPower { WrathOfSorel, TidalMaster}
        public Artifact(ArtifactPower ap)
        {
            switch(ap)
            {
                case ArtifactPower.WrathOfSorel:
                    description = "Direct physical damage grant one stack of wrath of sorel. After reaching 20 stack next holy magic unleash wrath of sorel for 6 seconds. Convert all physical damage to holy damage and increase holy damage dealt by 25%";
                    break;
                case ArtifactPower.TidalMaster:
                    description = "Water based healing skills summon tidal wave that deals damage equal to healing, water based damage skills heals 2% of you max health, can't occur more than once per second.";
                        break;
            }
        }

       public string GetDescription()
        {
            return description;
        }
    }

    public class Blessed
    {
        private string description;
        public enum Blessing { None, PrayerOfHope, CriticalStorm, ArcaneShield }
        private string name = "Prayer of Hope";
        public Blessed(Blessing bless)
        {
            switch (bless)
            {
                case Blessing.PrayerOfHope:
                    description = "Restore 25% health and stamina when health drop below 35%, this effect can't occur more than once per 10 seconds";
                    name = "Prayer of Hope";
                    break;
                case Blessing.CriticalStorm:
                    description = "Deal lightning damage equal to 70% of your spell power to target enemy, when you deal direct critical strike damage, this effect can't occur more than once per 3 seconds";
                    name = "Critical Storm";
                    break;
                case Blessing.ArcaneShield:
                    description = "Gain absorb equal to 30% of your maximum mana, refresh absorb every 6 seconds";
                    name = "Arcane Shield";
                    break;
            }
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetName()
        {
            return name;
        }
    }

    public class Cursed
    {
        private string description;
        public enum Curse { Bloodfiend, Pirate }
        private Curse curse;
        public Cursed(Curse curseType)
        {
            switch (curseType)
            {
                case Curse.Bloodfiend:
                    description = "Increase life steal and life regeneration by 60%, but reduce healing revive by 75%";
                    break;
                case Curse.Pirate:
                    description = "Weapon skills inflict random curse to your enemies, curse lasts for 6 seconds, but cursed enemies bypass 50% of your armor";
                    break;
            }
            curse = curseType;
        }

        public string GetDescription()
        {
            return description;
        }

        public Curse GetCurse()
        {
            return curse;
        }
    }

    public class Rune
    {
        public Rune() { }
        private Item.Affixes runeAffix;
        private double value;
        private RuneWord runeWord;
        private double runeStatMult;
        public enum RuneWord { Empty, Var, Qre, El, Ire, Ta, Gad, Zel, Xer, Mar, Nef }
        
        public Rune(RuneWord rune)
        {
            runeStatMult = 0.75;
            runeWord = rune;
            switch (rune)
            {
                case RuneWord.Var:
                    runeAffix = Item.Affixes.LifeSteal;
                    value = Item.itemAffixValues[Item.Affixes.LifeSteal] * runeStatMult;
                    break;
                case RuneWord.Qre:
                    runeAffix = Item.Affixes.Precision;
                    value = Item.itemAffixValues[Item.Affixes.Precision] * runeStatMult;
                    break;
                case RuneWord.El:
                    runeAffix = Item.Affixes.Life;
                    value = Item.itemAffixValues[Item.Affixes.Life] * runeStatMult;
                    break;
                case RuneWord.Ire:
                    runeAffix = Item.Affixes.LifeRegen;
                    value = Item.itemAffixValues[Item.Affixes.LifeRegen] * runeStatMult;
                    break;
                case RuneWord.Ta:
                    runeAffix = Item.Affixes.Block;
                    value = Item.itemAffixValues[Item.Affixes.Block] * runeStatMult;
                    break;
                case RuneWord.Gad:
                    runeAffix = Item.Affixes.Thoughness;
                    value = Item.itemAffixValues[Item.Affixes.Thoughness] * runeStatMult;
                    break;
                case RuneWord.Zel:
                    runeAffix = Item.Affixes.Alacrity;
                    value = Item.itemAffixValues[Item.Affixes.Alacrity] * runeStatMult;
                    break;
                case RuneWord.Xer:
                    runeAffix = Item.Affixes.Ferocity;
                    value = Item.itemAffixValues[Item.Affixes.Ferocity] * runeStatMult;
                    break;
                case RuneWord.Mar:
                    runeAffix = Item.Affixes.Mana;
                    value = Item.itemAffixValues[Item.Affixes.Mana] * runeStatMult;
                    break;
                case RuneWord.Nef:
                    runeAffix = Item.Affixes.ManaRegen;
                    value = Item.itemAffixValues[Item.Affixes.ManaRegen] * runeStatMult;
                    break;
                default:
                    runeAffix = Item.Affixes.None;
                    value = 0;
                    break;
            }
        }

        public Item.Affixes GetRuneAffix()
        {
            return runeAffix;
        }

        public double GetRuneStatValue()
        {
            return value;
        }

        public RuneWord GetRuneWord()
        {
            return runeWord;
        }
    }

    public class RuneSet
    {
        public enum RuneSets { None, Vampire, Guardian }
        private RuneSets runeSet = RuneSets.None;
        private Dictionary<RuneSets, List<Rune.RuneWord>> runeSetList = new Dictionary<RuneSets, List<Rune.RuneWord>>();
        private string descreption;

        public RuneSet() 
        {
            List<Rune.RuneWord> runeSetVampire = new List<Rune.RuneWord> { Rune.RuneWord.Var, Rune.RuneWord.El, Rune.RuneWord.Ire };
            runeSetList.Add(RuneSets.Vampire, runeSetVampire);

            List<Rune.RuneWord> runeSetGuardian = new List<Rune.RuneWord> { Rune.RuneWord.El, Rune.RuneWord.Ta, Rune.RuneWord.Gad };
            runeSetList.Add(RuneSets.Guardian, runeSetGuardian);
        }

        public void SetRuneSet(List<Rune> rune)
        {
            List<Rune.RuneWord> rw = new List<Rune.RuneWord>();
            foreach (Rune r in rune)
            {
                rw.Add(r.GetRuneWord());
            }
            
            foreach (KeyValuePair<RuneSets, List<Rune.RuneWord>> rs in runeSetList)
            {
                runeSet = RuneSets.None;
                bool itemFound = true;
                if (rw.Count() == rs.Value.Count())
                {
                    for (int i = 0; i < rw.Count(); i++)
                    {
                        if (rw[i] != rs.Value[i])
                        {
                            itemFound = false;
                            break;
                        }
                    }
                    if (itemFound)
                    {
                        runeSet = rs.Key;
                        break;
                    }
                }
            }
            SetDescreption(runeSet);
        }

        private void SetDescreption(RuneSets rs)
        {
            switch(rs)
            {
                case RuneSets.Vampire:
                    descreption = "Steal life from enemy equal to 4% of your max life when dealing single target damage, 6 second cooldown";
                    break;
                case RuneSets.Guardian:
                    descreption = "Damage taken recover block value by 50% of damage taken";
                    break;
            }
        }
      
        public RuneSets GetRuneSet()
        {
            return runeSet;
        }

        public string GetDescription()
        {
            return descreption;
        }
    }
}

