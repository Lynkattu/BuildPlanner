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
    /// Interaction logic for ItemPage.xaml
    /// </summary>
    public partial class ItemPage : Page
    {

        private Attributes att = new Attributes();
        private readonly ItemTooltip itemTooltip = new ItemTooltip();
        private bool isEntered = false;

        private Armor.ArmorSlot armorSlot;

        private Jewelry.JewelrySlot jewelrySlot = Jewelry.JewelrySlot.Necklace;

        

        public ItemPage()
        {
            InitializeComponent();
        }



        private void Label_SearchItem_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            Label srcLb = e.Source as Label;
            if (srcLb != null)
            {
                switch (srcLb.Name)
                {
                    case "lb_chest":
                        armorSlot = Armor.ArmorSlot.Chest;
                        ItemBrowser.Content = new ItemSearchPage(armorSlot);
                        break;

                    case "lb_legs":
                        armorSlot = Armor.ArmorSlot.Legs;
                        ItemBrowser.Content = new ItemSearchPage(armorSlot);
                        break;

                    case "lb_feet":
                        armorSlot = Armor.ArmorSlot.Feet;
                        ItemBrowser.Content = new ItemSearchPage(armorSlot);
                        break;

                    case "lb_hands":
                        armorSlot = Armor.ArmorSlot.Hands;
                        ItemBrowser.Content = new ItemSearchPage(armorSlot);
                        break;

                    case "lb_head":
                        armorSlot = Armor.ArmorSlot.Head;
                        ItemBrowser.Content = new ItemSearchPage(armorSlot);
                        break;

                    case "lb_shoulder":
                        armorSlot = Armor.ArmorSlot.Shoulder;
                        ItemBrowser.Content = new ItemSearchPage(armorSlot);
                        break;

                    case "lb_main_hand":
                        ItemBrowser.Content = new ItemSearchPage(true);
                        break;

                    case "lb_off_hand":
                        ItemBrowser.Content = new ItemSearchPage(false);
                        break;

                    case "lb_necklace":
                        jewelrySlot = Jewelry.JewelrySlot.Necklace;
                        ItemBrowser.Content = new ItemSearchPage(jewelrySlot);
                        break;

                    case "lb_ring":
                        jewelrySlot = Jewelry.JewelrySlot.Ring;
                        ItemBrowser.Content = new ItemSearchPage(jewelrySlot);
                        break;

                    case "lb_charm":
                        jewelrySlot = Jewelry.JewelrySlot.Charm;
                        ItemBrowser.Content = new ItemSearchPage(jewelrySlot);
                        break;
                }

            }
        }

        private void Label_CustomizeItem_RightMouseDown(object sender, MouseButtonEventArgs e)
        {
            Label srcLb = e.Source as Label;
            if (srcLb != null)
            {
                Armor armor = null;
                Weapon weapon = null;
                Jewelry jewelry = null;
                bool isMainHand = false;
                switch (srcLb.Name)
                {
                    case "lb_chest":
                        armor = att.GetArmor(Attributes.ItemSlot.Chest);
                        break;

                    case "lb_legs":
                        armor = att.GetArmor(Attributes.ItemSlot.Legs);
                        break;

                    case "lb_feet":
                        armor = att.GetArmor(Attributes.ItemSlot.Feet);
                        break;

                    case "lb_hands":
                        armor = att.GetArmor(Attributes.ItemSlot.Hands);
                        break;

                    case "lb_head":
                        armor = att.GetArmor(Attributes.ItemSlot.Head);
                        break;

                    case "lb_shoulder":
                        armor = att.GetArmor(Attributes.ItemSlot.Shoulder);
                        break;

                    case "lb_main_hand":
                        weapon = att.GetWeapon(Attributes.ItemSlot.MainHand);
                        isMainHand = true;
                        break;

                    case "lb_off_hand":
                        weapon = att.GetWeapon(Attributes.ItemSlot.OffHand);
                        break;

                    case "lb_necklace":
                        jewelry = att.GetJewelry(Attributes.ItemSlot.Necklace);
                        break;

                    case "lb_ring":
                        jewelry = att.GetJewelry(Attributes.ItemSlot.Ring);
                        break;

                    case "lb_charm":
                        jewelry = att.GetJewelry(Attributes.ItemSlot.Charm);
                        break;
                };
                if (armor != null)
                {
                    ItemBrowser.Content = new ItemCusomizePage(armor.GetItemName(), isMainHand, armor.GetAffixKeyOrder(), armor.GetRuneWords());
                }
                if (weapon != null)
                {
                    ItemBrowser.Content = new ItemCusomizePage(weapon.GetItemName(), isMainHand, weapon.GetAffixKeyOrder(), weapon.GetRuneWords());
                }
                if (jewelry != null)
                {
                    ItemBrowser.Content = new ItemCusomizePage(jewelry.GetItemName(), isMainHand, jewelry.GetAffixKeyOrder(), jewelry.GetRuneWords());
                }
            }
        }

        private void Label_WeaponSwap_MouseDown(object sender, MouseEventArgs e)
        {
            att.WeaponSwap();
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isEntered)
            {
                bool isNull = true;
                Label srcLabel = e.Source as Label;
                Armor armor;
                Weapon weapon;
                Jewelry jewelry;
                switch (srcLabel.Name)
                {
                    case "lb_chest":
                        armor = att.GetArmor(Attributes.ItemSlot.Chest);
                        if (armor != null)
                        {
                            itemTooltip.ArmorTooltip(armor, true);
                            isNull = false;
                        }
                        break;

                    case "lb_feet":
                        armor = att.GetArmor(Attributes.ItemSlot.Feet);
                        if (armor != null)
                        {
                            itemTooltip.ArmorTooltip(armor, true);
                            isNull = false;
                        }
                        break;

                    case "lb_legs":
                        armor = att.GetArmor(Attributes.ItemSlot.Legs);
                        if (armor != null)
                        {
                            itemTooltip.ArmorTooltip(armor, true);
                            isNull = false;
                        }
                        break;

                    case "lb_hands":
                        armor = att.GetArmor(Attributes.ItemSlot.Hands);
                        if (armor != null)
                        {
                            itemTooltip.ArmorTooltip(armor, true);
                            isNull = false;
                        }
                        break;

                    case "lb_head":
                        armor = att.GetArmor(Attributes.ItemSlot.Head);
                        if (armor != null)
                        {
                            itemTooltip.ArmorTooltip(armor, true);
                            isNull = false;
                        }
                        break;

                    case "lb_shoulder":
                        armor = att.GetArmor(Attributes.ItemSlot.Shoulder);
                        if (armor != null)
                        {
                            itemTooltip.ArmorTooltip(armor, true);
                            isNull = false;
                        }
                        break;

                    case "lb_main_hand":
                        weapon = att.GetWeapon(Attributes.ItemSlot.MainHand);
                        if (weapon != null)
                        {
                            itemTooltip.WeaponTooltip(weapon, true);
                            isNull = false;
                        }
                        break;

                    case "lb_off_hand":
                        weapon = att.GetWeapon(Attributes.ItemSlot.OffHand);
                        if (weapon != null)
                        {
                            itemTooltip.WeaponTooltip(weapon, true);
                            isNull = false;
                        }                       
                        break;

                    case "lb_necklace":
                        jewelry = att.GetJewelry(Attributes.ItemSlot.Necklace);
                        if (jewelry != null)
                        {
                            itemTooltip.JewelryTooltip(jewelry, true);
                            isNull = false;
                        }
                        break;

                    case "lb_ring":
                        jewelry = att.GetJewelry(Attributes.ItemSlot.Ring);
                        if (jewelry != null)
                        {
                            itemTooltip.JewelryTooltip(jewelry, true);
                            isNull = false;
                        }
                        break;

                    case "lb_charm":
                        jewelry = att.GetJewelry(Attributes.ItemSlot.Charm);
                        if (jewelry != null)
                        {
                            itemTooltip.JewelryTooltip(jewelry, true);
                            isNull = false;
                        }
                        break;
                }
                if (!isNull)
                {

                    Canvas.SetLeft(itemTooltip.GetItemTooltip(), Mouse.GetPosition(Can_ItemPage).X + 20);
                    Canvas.SetTop(itemTooltip.GetItemTooltip(), Mouse.GetPosition(Can_ItemPage).Y - 10);
                    Can_ItemPage.Children.Add(itemTooltip.GetItemTooltip());
                }

                isEntered = true;
            }
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            isEntered = false;
            if(itemTooltip != null)
            {
                itemTooltip.ClearTooltipChildens();
                Can_ItemPage.Children.Remove(itemTooltip.GetItemTooltip());
            }
        }

        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            if (itemTooltip != null)
            {
                Canvas.SetLeft(itemTooltip.GetItemTooltip(), Mouse.GetPosition(Can_ItemPage).X + 20);
                Canvas.SetTop(itemTooltip.GetItemTooltip(), Mouse.GetPosition(Can_ItemPage).Y - 10);
            }
        }

       
    }
}
