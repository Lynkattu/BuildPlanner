using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilPlanner
{
    public class ItemList
    {
        private List<Armor> armorList = new List<Armor>()
        {
           new Armor("Dancer top", Armor.ArmorSlot.Chest, 450, 2),
           new Armor("Bishop Robe", Armor.ArmorSlot.Chest, 600, 4, Blessed.Blessing.None),
           new Armor("Paladin Boots",Armor.ArmorSlot.Feet, 700, 6, Blessed.Blessing.None),
           new Armor("Captain's hat", Armor.ArmorSlot.Head, 280, 1.5, Cursed.Curse.Pirate),
           new Armor("Vampire Vestment",Armor.ArmorSlot.Chest, 840, 6, Cursed.Curse.Bloodfiend),
           new Armor("Druid Kilt", Armor.ArmorSlot.Legs, 600, 4, 3),
           new Armor("Runic Skirt", Armor.ArmorSlot.Legs, 405, 1.8, 6),
           new Armor("Druid Robe",Armor.ArmorSlot.Chest, 750, 5, 3),
           new Armor("Archangel Sorel's Cuirass", Armor.ArmorSlot.Chest, 1890, 14, Artifact.ArtifactPower.WrathOfSorel),

        };

        private List<Weapon> weaponList = new List<Weapon>()
        {
            new Weapon("Trident of the Ocean", Weapon.WeaponSlot.TwoHand, Weapon.WeaponType.Spear, 1800, 0.9, 5, Artifact.ArtifactPower.TidalMaster),
            new Weapon("Captain's Saber", Weapon.WeaponSlot.OneHand, Weapon.WeaponType.Sword, 1350, 1, 2, Cursed.Curse.Pirate)
        };

        private List<Jewelry> jewelryList = new List<Jewelry>()
        {
            new Jewelry("Amulet of Vampire Lord", Jewelry.JewelrySlot.Necklace, Cursed.Curse.Bloodfiend),
            new Jewelry("Priest Pendant", Jewelry.JewelrySlot.Necklace, Blessed.Blessing.None),
            new Jewelry("Silver Necklace", Jewelry.JewelrySlot.Necklace),
            new Jewelry("Engraved Amulet", Jewelry.JewelrySlot.Necklace, 2),

        };

        public List<Armor> GetArmorList()
        {
            return armorList;
        }
        public List<Weapon> GetWeaponList()
        {
            return weaponList;
        }
        public List<Jewelry> GetJewelryList()
        {
            return jewelryList;
        }

        public Armor FindArmor(string name)
        {
            foreach (Armor armor in armorList)
            {              
                if (armor.GetItemName().ToLower() == name.ToLower())
                {
                    return armor;
                }
            }
            return null;
        }

        public Weapon FindWeapon(string name)
        {
            foreach (Weapon weapon in weaponList)
            {
                if (weapon.GetItemName().ToLower() == name.ToLower())
                {
                    return weapon;
                }
            }
            return null;
        }

        public Jewelry FindJewelry(string name)
        {
            foreach (Jewelry jewelry in jewelryList)
            {
                if (jewelry.GetItemName().ToLower() == name.ToLower())
                {
                    return jewelry;
                }
            }
            return null;
        }
    }
}
