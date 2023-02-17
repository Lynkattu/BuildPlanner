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
    /// Interaction logic for ItemSearchPage.xaml
    /// </summary>
    public partial class ItemSearchPage : Page
    {     
        private readonly ItemList itemList = new ItemList();
        private readonly ItemTooltip itemTooltip = new ItemTooltip();

        private enum ItemClass { Armor, weapon, Jewelry }
        private ItemClass itemClass;
        private Armor.ArmorSlot armorSlot;
        private bool isMainHand;
        private Jewelry.JewelrySlot jewelrySlot;
        private string search = "";

        private bool isEntered = false;
        private Item.ItemType it = Item.ItemType.Normal;

        //Armor
        public ItemSearchPage(Armor.ArmorSlot Slot)//armor list
        {
            InitializeComponent();
            armorSlot = Slot;
            itemClass = ItemClass.Armor;
            search = "";
            List<Armor> SearchItems = new List<Armor>();
            foreach (Armor i in itemList.GetArmorList())
            {
                if (i.GetArmorSlot() == armorSlot) SearchItems.Add(i);
            }

                foreach (Item i in SearchItems)
            {
                TextBlock t = new TextBlock();
                t.Text = i.GetItemName();
                t.FontSize = 18;
                t.MouseMove += new MouseEventHandler(Label_MouseMove);
                t.MouseEnter += new MouseEventHandler(Tb_MouseEnter);
                t.MouseLeave += new MouseEventHandler(Tb_MouseLeave);
                t.MouseUp += new MouseButtonEventHandler(Tb_SelectItem_MouseUp);
                t.Foreground = i.GetItemColor();               
                ItemPanel.Children.Add(t);
            }
        }

        //Weapon
        public ItemSearchPage(bool isMainhand)//weapon list
        {
            InitializeComponent();
            isMainHand = isMainhand;
            itemClass = ItemClass.weapon;
            search = "";
            List<Weapon> SearchItems = new List<Weapon>();
            foreach (Weapon i in itemList.GetWeaponList())
            {                  
                if (isMainHand)
                {
                    if (i.GetWeaponSlot() == Weapon.WeaponSlot.MainHand || i.GetWeaponSlot() == Weapon.WeaponSlot.TwoHand || i.GetWeaponSlot() == Weapon.WeaponSlot.OneHand)
                        SearchItems.Add(i);
                }
                else
                {
                    if (i.GetWeaponSlot() == Weapon.WeaponSlot.OffHand || i.GetWeaponSlot() == Weapon.WeaponSlot.OneHand)
                        SearchItems.Add(i);
                }                
            }

            foreach (Item i in SearchItems)
            {
                TextBlock t = new TextBlock();
                t.Text = i.GetItemName();
                t.FontSize = 18;
                t.MouseMove += new MouseEventHandler(Label_MouseMove);
                t.MouseEnter += new MouseEventHandler(Tb_MouseEnter);
                t.MouseLeave += new MouseEventHandler(Tb_MouseLeave);
                t.MouseUp += new MouseButtonEventHandler(Tb_SelectItem_MouseUp);
                t.Foreground = i.GetItemColor();
                ItemPanel.Children.Add(t);
            }
        }

        //Jewelry
        public ItemSearchPage(Jewelry.JewelrySlot Slot)//jewelry list
        {
            InitializeComponent();
            jewelrySlot = Slot;
            itemClass = ItemClass.Jewelry;
            search = "";
            List<Jewelry> SearchItems = new List<Jewelry>();
            foreach (Jewelry i in itemList.GetJewelryList())
            {
                if (i.GetJewelrySlot() == jewelrySlot) SearchItems.Add(i);
            }

            foreach (Item i in SearchItems)
            {
                TextBlock t = new TextBlock();
                t.Text = i.GetItemName();
                t.FontSize = 18;
                t.MouseMove += new MouseEventHandler(Label_MouseMove);
                t.MouseEnter += new MouseEventHandler(Tb_MouseEnter);
                t.MouseLeave += new MouseEventHandler(Tb_MouseLeave);
                t.MouseUp += new MouseButtonEventHandler(Tb_SelectItem_MouseUp);
                t.Foreground = i.GetItemColor();
                ItemPanel.Children.Add(t);
            }
        }


        private void Label_MouseMove(object sender, MouseEventArgs e)
        {            
            Canvas.SetLeft(itemTooltip.GetItemTooltip(), Mouse.GetPosition(Can).X + 20);
            Canvas.SetTop(itemTooltip.GetItemTooltip(), Mouse.GetPosition(Can).Y - 10);
        }

        private void Tb_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isEntered)
            {
                TextBlock srcTb = e.Source as TextBlock;
                if (!itemTooltip.SrcArmor(srcTb.Text.ToString()))
                    if (!itemTooltip.SrcWeapon(srcTb.Text.ToString()))
                        itemTooltip.SrcJewelry(srcTb.Text.ToString());

                Canvas.SetLeft(itemTooltip.GetItemTooltip(), Mouse.GetPosition(Can).X + 20);
                Canvas.SetTop(itemTooltip.GetItemTooltip(), Mouse.GetPosition(Can).Y - 10);
                Can.Children.Add(itemTooltip.GetItemTooltip());
                isEntered = true;
            }
        }

        private void Tb_MouseLeave(object sender, MouseEventArgs e)
        {
            isEntered = false;
            itemTooltip.ClearTooltipChildens();
            Can.Children.Remove(itemTooltip.GetItemTooltip());
        }

        private void Tb_SelectItem_MouseUp(object sender, MouseEventArgs e)
        {
            TextBlock srcTb = e.Source as TextBlock;
            Attributes att = new Attributes();
            att.AddEquipment(srcTb.Text.ToString(), isMainHand);

            
            if (att.GetWeapon(Attributes.ItemSlot.OffHand) != null && att.GetWeapon(Attributes.ItemSlot.MainHand) != null)
            {
                if (att.GetWeapon(Attributes.ItemSlot.MainHand).GetWeaponSlot() == Weapon.WeaponSlot.TwoHand)
                {
                    //Remove mainhand weapon if it's two-handed when offhand weapon is selected
                    if (att.GetWeapon(Attributes.ItemSlot.OffHand).GetItemName() == srcTb.Text.ToString())
                    {
                        att.RemoveItem(Attributes.ItemSlot.MainHand);
                    }

                    //Remove offhand weapon if selected mainhand weapon is two-handed
                    else if (att.GetWeapon(Attributes.ItemSlot.MainHand).GetItemName() == srcTb.Text.ToString() && att.GetWeapon(Attributes.ItemSlot.OffHand) != null)
                    {
                        att.RemoveItem(Attributes.ItemSlot.OffHand);
                    }
                }
            }
       
            ItemCusomizePage icustomize = new ItemCusomizePage(srcTb.Text.ToString(), isMainHand);
            NavigationService.Navigate(icustomize);
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            search = tbx_Search.Text;
            CreateItemSearchPage();
        }

        private void Cb_ItemType_DropDownClosed(object sender, EventArgs e)
        {
            switch (Cb_ItemType.Text)
            {
                case "Artifact":
                    it = Item.ItemType.Artifact;
                    break;
                case "Blessed":
                    it = Item.ItemType.Blessed;
                    break;
                case "Cursed":
                    it = Item.ItemType.Cursed;
                    break;
                case "Normal":
                    it = Item.ItemType.Normal;
                    break;
                case "Runic":
                    it = Item.ItemType.Runic;
                    break;
            }
            CreateItemSearchPage();
        }

        private void CreateItemSearchPage()
        {
            if (Cb_ItemType.Text != "Any")
            {
                switch (itemClass)
                {
                    case ItemClass.Armor:
                        List<Item> SearchArmor = new List<Item>();
                        foreach (Armor i in itemList.GetArmorList())
                        {
                            if (i.GetItemName().ToLower().Contains(search.ToLower()))
                            {
                                if (it == i.GetItemType())
                                {
                                    if (i.GetArmorSlot() == armorSlot)
                                        SearchArmor.Add(i);
                                }
                            }
                        }
                        ItemSearch(SearchArmor);
                        break;

                    case ItemClass.weapon:
                        List<Item> SearchWeapons = new List<Item>();
                        foreach (Weapon i in itemList.GetWeaponList())
                        {
                            if (i.GetItemName().ToLower().Contains(search.ToLower()))
                            {
                                if (isMainHand && i.GetItemType() == it)
                                {
                                    if (i.GetWeaponSlot() == Weapon.WeaponSlot.OneHand || i.GetWeaponSlot() == Weapon.WeaponSlot.TwoHand || i.GetWeaponSlot() == Weapon.WeaponSlot.MainHand)
                                    {
                                        SearchWeapons.Add(i);
                                    }
                                }
                                else if (i.GetItemType() == it)
                                {
                                    if (i.GetWeaponSlot() == Weapon.WeaponSlot.OneHand || i.GetWeaponSlot() == Weapon.WeaponSlot.OffHand)
                                    {
                                        SearchWeapons.Add(i);
                                    }
                                }
                            }
                        }
                        break;

                    case ItemClass.Jewelry:
                        List<Item> SearchItems = new List<Item>();
                        foreach (Jewelry i in itemList.GetJewelryList())
                        {
                            if (i.GetItemName().ToLower().Contains(search.ToLower()))
                            {
                                if (it == i.GetItemType())
                                {
                                    if (i.GetJewelrySlot() == jewelrySlot)
                                        SearchItems.Add(i);
                                }
                            }
                        }
                        ItemSearch(SearchItems);
                        break;
                }
            }
            else
            {
                switch (itemClass)
                {
                    case ItemClass.Armor:
                        List<Item> SearchArmor = new List<Item>();
                        foreach (Armor i in itemList.GetArmorList())
                        {
                            if (i.GetItemName().ToLower().Contains(search.ToLower()))
                            {
                                if (i.GetArmorSlot() == armorSlot) SearchArmor.Add(i);
                            }
                        }
                        ItemSearch(SearchArmor);
                        break;
                    case ItemClass.weapon:
                        List<Item> SearchWeapons = new List<Item>();
                        foreach (Weapon i in itemList.GetWeaponList())
                        {
                            if (i.GetItemName().ToLower().Contains(search.ToLower()))
                            {
                                if (isMainHand)
                                {
                                    if (i.GetWeaponSlot() == Weapon.WeaponSlot.OneHand || i.GetWeaponSlot() == Weapon.WeaponSlot.TwoHand || i.GetWeaponSlot() == Weapon.WeaponSlot.MainHand)
                                    {
                                        SearchWeapons.Add(i);
                                    }
                                }
                                else
                                {
                                    if (i.GetWeaponSlot() == Weapon.WeaponSlot.OneHand || i.GetWeaponSlot() == Weapon.WeaponSlot.OffHand)
                                    {
                                        SearchWeapons.Add(i);
                                    }
                                }
                            }
                        }
                        ItemSearch(SearchWeapons);
                        break;
                    case ItemClass.Jewelry:
                        List<Item> SearchJewelry = new List<Item>();
                        foreach (Jewelry i in itemList.GetJewelryList())
                        {
                            if (i.GetItemName().ToLower().Contains(search.ToLower()))
                            {
                                if (i.GetJewelrySlot() == jewelrySlot) SearchJewelry.Add(i);
                            }
                        }
                        ItemSearch(SearchJewelry);
                        break;
                }
            }
        }

        private void ItemSearch(List<Item> SearchItems)
        {           
            ItemPanel.Children.Clear();

            foreach (Item i in SearchItems)
            {
                TextBlock t = new TextBlock();
                t.Text = i.GetItemName();
                t.FontSize = 18;
                t.MouseMove += new MouseEventHandler(Label_MouseMove);
                t.MouseEnter += new MouseEventHandler(Tb_MouseEnter);
                t.MouseLeave += new MouseEventHandler(Tb_MouseLeave);
                t.MouseUp += new MouseButtonEventHandler(Tb_SelectItem_MouseUp);
                t.Foreground = i.GetItemColor();
                ItemPanel.Children.Add(t);
            }
        }
    }
}
