using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for ItemCusomizePage.xaml
    /// </summary>
    public partial class ItemCusomizePage : Page
    {
        private readonly Attributes att = new Attributes();
        private readonly Armor armor;
        private readonly Weapon weapon;
        private readonly Jewelry jewelry;
        private TextBlock tb_RuneSet = new TextBlock();
        private TextBlock tb_RuneSetBonus = new TextBlock();
        TextBlock tb_BlessingDescription = new TextBlock();

        private readonly List<Rune> runes = null;


        private List<Item.Affixes> affixKeyOrder;

        public ItemCusomizePage(string itemName, bool isMainHand)
        {
            InitializeComponent();

            armor = att.GetArmor(itemName);
            if (armor == null)
            {
                weapon = att.GetWeapon(itemName, isMainHand);
                if (weapon == null)
                {
                    jewelry = att.GetJewelry(itemName);
                    if (jewelry != null)
                    {
                        CreateCombobox(jewelry);
                    }
                }
                else
                {
                    CreateCombobox(weapon);
                }
            }
            else
            {
                CreateCombobox(armor);
            }              
        }

        public ItemCusomizePage(string itemName, bool isMainHand, List<Item.Affixes> affixOrder, List <Rune> runeList)
        {
            InitializeComponent();
            runes = runeList;
            affixKeyOrder = affixOrder;
            armor = att.GetArmor(itemName);
            if (armor == null)
            {
                weapon = att.GetWeapon(itemName, isMainHand);
                if (weapon == null)
                {
                    jewelry = att.GetJewelry(itemName);
                    if (jewelry != null)
                    {
                        CreateCombobox(jewelry);
                    }
                }
                else
                {
                    CreateCombobox(weapon);
                }
            }
            else
            {
                CreateCombobox(armor);
            }
        }

        private void CreateCombobox(Item item)
        {
            Thickness thic = new Thickness(60, 20, 60, 0);
            affixKeyOrder = new List<Item.Affixes>
            {
                Item.Affixes.None,
                Item.Affixes.None,
                Item.Affixes.None,
                Item.Affixes.None,
                Item.Affixes.None,
                Item.Affixes.None
            };
            SetQuality(item, thic);
            SetAffixes(item, thic);
            SetRunes(item, thic);
            SetItemType(item, thic);
        }

        private void SetQuality(Item item, Thickness thic)
        {
            List<Item.ItemQuality> cb_QualityItemList = new List<Item.ItemQuality>();
            foreach (Item.ItemQuality iq in (Item.ItemQuality[])Enum.GetValues(typeof(Item.ItemQuality)))
            {
                cb_QualityItemList.Add(iq);
            }

            TextBlock tb_Quality = new TextBlock
            {
                Text = "Quality",
                FontSize = 18,
                Foreground = Brushes.White,
                TextAlignment = TextAlignment.Center,
                Margin = thic
            };
            Sp_ItemCustomize.Children.Add(tb_Quality);

            ComboBox cb_Quality = new ComboBox()
            {
                Margin = thic,
                FontSize = 18,
                ItemsSource = cb_QualityItemList,
            };
            cb_Quality.SelectionChanged += new SelectionChangedEventHandler(Cb_Quality_SelectionChanged);
            foreach (Item.ItemQuality quality in cb_QualityItemList)
            {
                if (quality == item.GetQuality())
                {
                    cb_Quality.SelectedItem = quality;
                    break;
                }
            }
            Sp_ItemCustomize.Children.Add(cb_Quality);
        }

        private void SetAffixes(Item item, Thickness thic)
        {
            List<ComboBox> cb_affix_list = new List<ComboBox>();

            if (item.GetAffixCount() > 0)
            {
                List<Item.Affixes> Cb_ItemList = new List<Item.Affixes>();
                foreach (Item.Affixes aff in (Item.Affixes[])Enum.GetValues(typeof(Item.Affixes)))
                {
                    Cb_ItemList.Add(aff);
                }


                ComboBox cb_Affix1 = new ComboBox()
                {
                    Name = "Affix1",
                    Margin = thic,
                    FontSize = 18,
                    ItemsSource = Cb_ItemList,
                };
                if (item.GetAffixCount() >= 1)
                {
                    if (item.GetAffixKeyOrder().Count() > 0)
                    {
                        cb_Affix1.SelectedItem = item.GetAffixKeyOrder()[0];
                    }
                    else
                        cb_Affix1.SelectedItem = Item.Affixes.None;
                }
                cb_Affix1.SelectionChanged += new SelectionChangedEventHandler(Cb_SelectionChanged);
                cb_affix_list.Add(cb_Affix1);

                ComboBox cb_Affix2 = new ComboBox()
                {
                    Name = "Affix2",
                    Margin = thic,
                    FontSize = 18,
                    ItemsSource = Cb_ItemList,
                };
                if (item.GetAffixCount() >= 2)
                {
                    if (item.GetAffixKeyOrder().Count() > 0)
                    {
                        cb_Affix2.SelectedItem = item.GetAffixKeyOrder()[1];
                    }
                    else
                        cb_Affix2.SelectedItem = Item.Affixes.None;
                }
                cb_Affix2.SelectionChanged += new SelectionChangedEventHandler(Cb_SelectionChanged);
                cb_affix_list.Add(cb_Affix2);

                ComboBox cb_Affix3 = new ComboBox()
                {
                    Name = "Affix3",
                    Margin = thic,
                    FontSize = 18,
                    ItemsSource = Cb_ItemList,

                };
                if (item.GetAffixCount() >= 3)
                {
                    if (item.GetAffixKeyOrder().Count() > 0)
                    {
                        cb_Affix3.SelectedItem = item.GetAffixKeyOrder()[2];
                    }
                    else
                        cb_Affix3.SelectedItem = Item.Affixes.None;
                }
                cb_Affix3.SelectionChanged += new SelectionChangedEventHandler(Cb_SelectionChanged);
                cb_affix_list.Add(cb_Affix3);

                ComboBox cb_Affix4 = new ComboBox()
                {
                    Name = "Affix4",
                    Margin = thic,
                    FontSize = 18,
                    ItemsSource = Cb_ItemList,

                };
                if (item.GetAffixCount() >= 4)
                {
                    if (item.GetAffixKeyOrder().Count() > 0)
                    {
                        cb_Affix4.SelectedItem = item.GetAffixKeyOrder()[3];
                    }
                    else
                        cb_Affix4.SelectedItem = Item.Affixes.None;
                }
                cb_Affix4.SelectionChanged += new SelectionChangedEventHandler(Cb_SelectionChanged);
                cb_affix_list.Add(cb_Affix4);

                ComboBox cb_Affix5 = new ComboBox()
                {
                    Name = "Affix5",
                    Margin = thic,
                    FontSize = 18,
                    ItemsSource = Cb_ItemList,

                };
                if (item.GetAffixCount() >= 5)
                {
                    if (item.GetAffixKeyOrder().Count() > 0)
                    {
                        cb_Affix5.SelectedItem = item.GetAffixKeyOrder()[4];
                    }
                    else
                        cb_Affix5.SelectedItem = Item.Affixes.None;
                }
                cb_Affix5.SelectionChanged += new SelectionChangedEventHandler(Cb_SelectionChanged);
                cb_affix_list.Add(cb_Affix5);

                ComboBox cb_Affix6 = new ComboBox()
                {
                    Name = "Affix6",
                    Margin = thic,
                    FontSize = 18,
                    ItemsSource = Cb_ItemList,

                };
                if (item.GetAffixCount() >= 6)
                {
                    if (item.GetAffixKeyOrder().Count() > 0)
                    {
                        cb_Affix6.SelectedItem = item.GetAffixKeyOrder()[5];
                    }
                    else
                        cb_Affix6.SelectedItem = Item.Affixes.None;
                }
                cb_Affix6.SelectionChanged += new SelectionChangedEventHandler(Cb_SelectionChanged);
                cb_affix_list.Add(cb_Affix6);

                TextBlock tb_affixes = new TextBlock
                {
                    Text = "Affixes",
                    FontSize = 18,
                    Foreground = Brushes.LightGreen,
                    TextAlignment = TextAlignment.Center,
                    Margin = thic

                };
                Sp_ItemCustomize.Children.Add(tb_affixes);

                for (int i = 0; i < item.GetAffixCount(); i++)
                {
                    Sp_ItemCustomize.Children.Add(cb_affix_list[i]);
                }
            }
        }

        private void SetRunes(Item item, Thickness thic)
        {
            if (item.GetRuneCount() > 0)
            {
                List<ComboBox> cbList = new List<ComboBox>();
                List<Rune.RuneWord> Cb_ItemList = new List<Rune.RuneWord>();
                foreach (Rune.RuneWord rw in (Rune.RuneWord[])Enum.GetValues(typeof(Rune.RuneWord)))
                {
                    Cb_ItemList.Add(rw);
                }

                ComboBox cb_Rune1 = new ComboBox()
                {
                    Name = "Rune1",
                    Margin = thic,
                    FontSize = 18,
                    ItemsSource = Cb_ItemList

                };
                cb_Rune1.SelectionChanged += new SelectionChangedEventHandler(Cb_Rune_SelectionChanged);
                if (runes != null && runes.Count() >= 1)
                {
                    for (int i = 0; i < Cb_ItemList.Count(); i++)
                    {
                        if (runes[0].GetRuneWord() == Cb_ItemList[i])
                        {
                            cb_Rune1.SelectedIndex = i;
                        }
                    }
                }
                else if (item.GetRuneCount() >= 1)
                {
                    cb_Rune1.SelectedIndex = 0;
                }
                cbList.Add(cb_Rune1);

                ComboBox cb_Rune2 = new ComboBox()
                {
                    Name = "Rune2",
                    Margin = thic,
                    FontSize = 18,
                    ItemsSource = Cb_ItemList

                };
                cb_Rune2.SelectionChanged += new SelectionChangedEventHandler(Cb_Rune_SelectionChanged);
                if (runes != null && runes.Count() >= 2)
                {
                    for (int i = 0; i < Cb_ItemList.Count(); i++)
                    {
                        if (runes[1].GetRuneWord() == Cb_ItemList[i])
                        {
                            cb_Rune2.SelectedIndex = i;
                        }
                    }
                }
                else if (item.GetRuneCount() >= 2)
                {
                    cb_Rune2.SelectedIndex = 0;
                }
                cbList.Add(cb_Rune2);

                ComboBox cb_Rune3 = new ComboBox()
                {
                    Name = "Rune3",
                    Margin = thic,
                    FontSize = 18,
                    ItemsSource = Cb_ItemList

                };
                cb_Rune3.SelectionChanged += new SelectionChangedEventHandler(Cb_Rune_SelectionChanged);
                if (runes != null && runes.Count() >= 3)
                {
                    for (int i = 0; i < Cb_ItemList.Count(); i++)
                    {
                        if (runes[2].GetRuneWord() == Cb_ItemList[i])
                        {
                            cb_Rune3.SelectedIndex = i;
                        }
                    }
                }
                else if (item.GetRuneCount() >= 3)
                {
                    cb_Rune3.SelectedIndex = 0;
                }
                cbList.Add(cb_Rune3);

                ComboBox cb_Rune4 = new ComboBox()
                {
                    Name = "Rune4",
                    Margin = thic,
                    FontSize = 18,
                    ItemsSource = Cb_ItemList
                };
                cb_Rune4.SelectionChanged += new SelectionChangedEventHandler(Cb_Rune_SelectionChanged);
                if (runes != null && runes.Count() >= 4)
                {
                    for (int i = 0; i < Cb_ItemList.Count(); i++)
                    {
                        if (runes[3].GetRuneWord() == Cb_ItemList[i])
                        {
                            cb_Rune4.SelectedIndex = i;
                        }
                    }
                }
                else if (item.GetRuneCount() >= 4)
                {
                    cb_Rune4.SelectedIndex = 0;
                }
                cbList.Add(cb_Rune4);

                ComboBox cb_Rune5 = new ComboBox()
                {
                    Name = "Rune5",
                    Margin = thic,
                    FontSize = 18,
                    ItemsSource = Cb_ItemList

                };
                cb_Rune5.SelectionChanged += new SelectionChangedEventHandler(Cb_Rune_SelectionChanged);
                if (runes != null && runes.Count() >= 5)
                {
                    for (int i = 0; i < Cb_ItemList.Count(); i++)
                    {
                        if (runes[4].GetRuneWord() == Cb_ItemList[i])
                        {
                            cb_Rune5.SelectedIndex = i;
                        }
                    }
                }
                else if (item.GetRuneCount() >= 5)
                {
                    cb_Rune5.SelectedIndex = 0;
                }
                cbList.Add(cb_Rune5);

                ComboBox cb_Rune6 = new ComboBox()
                {
                    Name = "Rune6",
                    Margin = thic,
                    FontSize = 18,
                    ItemsSource = Cb_ItemList

                };
                cb_Rune6.SelectionChanged += new SelectionChangedEventHandler(Cb_Rune_SelectionChanged);
                if (runes != null && runes.Count() >= 6)
                {
                    for (int i = 0; i < Cb_ItemList.Count(); i++)
                    {
                        if (runes[5].GetRuneWord() == Cb_ItemList[i])
                        {
                            cb_Rune6.SelectedIndex = i;
                        }
                    }
                }
                else if (item.GetRuneCount() >= 6)
                {
                    cb_Rune6.SelectedIndex = 0;
                }
                cbList.Add(cb_Rune6);

                TextBlock tb_Runes = new TextBlock
                {
                    Text = "Runes",
                    FontSize = 18,
                    Foreground = Brushes.LightSkyBlue,
                    TextAlignment = TextAlignment.Center,
                    Margin = thic
                };
                Sp_ItemCustomize.Children.Add(tb_Runes);

                for (int i = 0; i < item.GetRuneCount(); i++)
                {
                    Sp_ItemCustomize.Children.Add(cbList[i]);
                }


            }
        }

        private void ShowRuneSet(Item item)
        {
            if (item.runeSet.GetRuneSet() != RuneSet.RuneSets.None)
            {
                if (Sp_ItemCustomize.Children.Contains(tb_RuneSetBonus))
                {
                    Sp_ItemCustomize.Children.Remove(tb_RuneSet);
                    Sp_ItemCustomize.Children.Remove(tb_RuneSetBonus);
                }
                Thickness thic = new Thickness(60, 20, 60, 0);
                tb_RuneSet = new TextBlock
                {
                    Text = "Rune set: " + item.runeSet.GetRuneSet().ToString(),
                    FontSize = 18,
                    Foreground = Brushes.Cyan,
                    TextAlignment = TextAlignment.Center,
                    Margin = thic
                };
                Sp_ItemCustomize.Children.Add(tb_RuneSet);

                tb_RuneSetBonus = new TextBlock
                {
                    Text = item.runeSet.GetDescription(),
                    FontSize = 16,
                    Foreground = Brushes.Cyan,
                    TextAlignment = TextAlignment.Center,
                    Margin = thic,
                    TextWrapping = TextWrapping.Wrap
                };
                Sp_ItemCustomize.Children.Add(tb_RuneSetBonus);

            }
            else if (Sp_ItemCustomize.Children.Contains(tb_RuneSetBonus))
            {
                Sp_ItemCustomize.Children.Remove(tb_RuneSet);
                Sp_ItemCustomize.Children.Remove(tb_RuneSetBonus);
            }
        }

        private void SetItemType(Item item, Thickness thic)
        {
            if (item.GetItemType() == Item.ItemType.Artifact)
            {
                TextBlock tb_Artifact = new TextBlock
                {
                    Text = "Artifact Power",
                    FontSize = 18,
                    Foreground = Brushes.Red,
                    TextAlignment = TextAlignment.Center,
                    Margin = thic
                };

                TextBlock tb_ArtifactPower = new TextBlock
                {
                    Text = item.artifact.GetDescription(),
                    FontSize = 16,
                    Foreground = Brushes.Red,
                    TextAlignment = TextAlignment.Center,
                    Margin = thic,
                    TextWrapping = TextWrapping.Wrap
                };

                Sp_ItemCustomize.Children.Add(tb_Artifact);
                Sp_ItemCustomize.Children.Add(tb_ArtifactPower);
            }

            if (item.GetItemType() == Item.ItemType.Blessed)
            {
                TextBlock tb_Blessing = new TextBlock
                {
                    Text = "Blessing",
                    FontSize = 18,
                    Foreground = Brushes.Orange,
                    TextAlignment = TextAlignment.Center,
                    Margin = thic
                };

                ComboBox cb_Blessing = new ComboBox()
                {
                    Margin = thic,
                    FontSize = 18,
                };
                cb_Blessing.SelectedIndex = 0;
                cb_Blessing.SelectionChanged += new SelectionChangedEventHandler(Cb_Blessed_SelectionChanged);

                List<Blessed.Blessing> cb_ItemList = new List<Blessed.Blessing>();
                foreach (Blessed.Blessing bless in (Blessed.Blessing[])Enum.GetValues(typeof(Blessed.Blessing)))
                {
                    if (bless != Blessed.Blessing.None)
                    {
                        cb_ItemList.Add(bless);
                    }
                }
                cb_Blessing.ItemsSource = cb_ItemList;

                tb_BlessingDescription = new TextBlock
                {
                    Name = "tb_BlessingDescription",
                    Text = item.blessed.GetDescription(),
                    FontSize = 16,
                    Foreground = Brushes.Orange,
                    TextAlignment = TextAlignment.Center,
                    Margin = thic,
                    TextWrapping = TextWrapping.Wrap
                };

                Sp_ItemCustomize.Children.Add(tb_Blessing);
                Sp_ItemCustomize.Children.Add(cb_Blessing);
                Sp_ItemCustomize.Children.Add(tb_BlessingDescription);

            }

            if (item.GetItemType() == Item.ItemType.Cursed)
            {
                TextBlock tb_Curse = new TextBlock
                {
                    Text = "Curse",
                    FontSize = 18,
                    Foreground = Brushes.MediumOrchid,
                    TextAlignment = TextAlignment.Center,
                    Margin = thic
                };

                TextBlock tb_CursePower = new TextBlock
                {
                    Text = item.cursed.GetDescription(),
                    FontSize = 16,
                    Foreground = Brushes.MediumOrchid,
                    TextAlignment = TextAlignment.Center,
                    Margin = thic,
                    TextWrapping = TextWrapping.Wrap
                };

                Sp_ItemCustomize.Children.Add(tb_Curse);
                Sp_ItemCustomize.Children.Add(tb_CursePower);
            }
        }

        private void Cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = e.Source as ComboBox;
            Item.Affixes affix = (Item.Affixes)cb.SelectedValue;

            if (jewelry != null)
            {
                if (!affixKeyOrder.Contains(affix) || affix == Item.Affixes.None)
                {
                    switch (cb.Name)
                    {
                        case "Affix1":
                            jewelry.AddAffix(affix, affixKeyOrder[0]);
                            affixKeyOrder[0] = affix;                           
                            break;
                        case "Affix2":
                            jewelry.AddAffix(affix, affixKeyOrder[1]);
                            affixKeyOrder[1] = affix;                           
                            break;
                        case "Affix3":
                            jewelry.AddAffix(affix, affixKeyOrder[2]);
                            affixKeyOrder[2] = affix;
                            break;
                        case "Affix4":
                            jewelry.AddAffix(affix, affixKeyOrder[3]);
                            affixKeyOrder[3] = affix;          
                            break;
                        case "Affix5":
                            jewelry.AddAffix(affix, affixKeyOrder[4]);
                            affixKeyOrder[4] = affix;                       
                            break;
                        case "Affix6":
                            jewelry.AddAffix(affix, affixKeyOrder[5]);
                            affixKeyOrder[5] = affix;
                            break;
                    }
                    jewelry.UpdateAffixKeyOrder(affixKeyOrder);
                }
                else
                {
                    MessageBox.Show("Item can't have duplicate affixes.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (armor != null)
            {
                if (!affixKeyOrder.Contains(affix) || affix == Item.Affixes.None)
                {
                    switch (cb.Name)
                    {
                        case "Affix1":
                            armor.AddAffix(affix, affixKeyOrder[0]);
                            affixKeyOrder[0] = affix;
                            break;
                        case "Affix2":
                            armor.AddAffix(affix, affixKeyOrder[1]);
                            affixKeyOrder[1] = affix;
                            break;
                        case "Affix3":
                            armor.AddAffix(affix, affixKeyOrder[2]);
                            affixKeyOrder[2] = affix;
                            break;
                        case "Affix4":
                            armor.AddAffix(affix, affixKeyOrder[3]);
                            affixKeyOrder[3] = affix;
                            break;
                        case "Affix5":
                            armor.AddAffix(affix, affixKeyOrder[4]);
                            affixKeyOrder[4] = affix;
                            break;
                        case "Affix6":
                            armor.AddAffix(affix, affixKeyOrder[5]);
                            affixKeyOrder[5] = affix;
                            break;
                    }
                    armor.UpdateAffixKeyOrder(affixKeyOrder);
                }
                else
                {
                    MessageBox.Show("Item can't have duplicate affixes.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if(weapon != null)
            {
                if (!affixKeyOrder.Contains(affix) || affix == Item.Affixes.None)
                {
                    switch (cb.Name)
                    {
                        case "Affix1":
                            weapon.AddAffix(affix, affixKeyOrder[0]);
                            affixKeyOrder[0] = affix;
                            break;
                        case "Affix2":
                            weapon.AddAffix(affix, affixKeyOrder[1]);
                            affixKeyOrder[1] = affix;
                            break;
                        case "Affix3":
                            weapon.AddAffix(affix, affixKeyOrder[2]);
                            affixKeyOrder[2] = affix;
                            break;
                        case "Affix4":
                            weapon.AddAffix(affix, affixKeyOrder[3]);
                            affixKeyOrder[3] = affix;
                            break;
                        case "Affix5":
                            weapon.AddAffix(affix, affixKeyOrder[4]);
                            affixKeyOrder[4] = affix;
                            break;
                        case "Affix6":
                            weapon.AddAffix(affix, affixKeyOrder[5]);
                            affixKeyOrder[5] = affix;
                            break;
                    }
                    weapon.UpdateAffixKeyOrder(affixKeyOrder);
                }
                else
                {
                    MessageBox.Show("Item can't have duplicate affixes.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }

        private void Cb_Rune_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = e.Source as ComboBox;
            Rune.RuneWord runeWord = (Rune.RuneWord)cb.SelectedValue;
            if (jewelry != null)
            {
                switch (cb.Name)
                {
                    case "Rune1":
                        jewelry.AddRune(runeWord, 0);
                        break;
                    case "Rune2":
                        jewelry.AddRune(runeWord, 1);
                        break;
                    case "Rune3":
                        jewelry.AddRune(runeWord, 2); 
                        break;
                    case "Rune4":
                        jewelry.AddRune(runeWord, 3);
                        break;
                    case "Rune5":
                        jewelry.AddRune(runeWord, 4); 
                        break;
                    case "Rune6":
                        jewelry.AddRune(runeWord, 5);
                        break;
                }
                ShowRuneSet(jewelry);
            }
            else if (armor != null)
            {
                switch (cb.Name)
                {
                    case "Rune1":
                        armor.AddRune(runeWord, 0);
                        break;
                    case "Rune2":
                        armor.AddRune(runeWord, 1);
                        break;
                    case "Rune3":
                        armor.AddRune(runeWord, 2);
                        break;
                    case "Rune4":
                        armor.AddRune(runeWord, 3);
                        break;
                    case "Rune5":
                        armor.AddRune(runeWord, 4);
                        break;
                    case "Rune6":
                        armor.AddRune(runeWord, 5);
                        break;
                }
                ShowRuneSet(armor);

            }
            else if (weapon != null)
            {
                switch (cb.Name)
                {
                    case "Rune1":
                        weapon.AddRune(runeWord, 0);
                        break;
                    case "Rune2":
                        weapon.AddRune(runeWord, 1);
                        break;
                    case "Rune3":
                        weapon.AddRune(runeWord, 2);
                        break;
                    case "Rune4":
                        weapon.AddRune(runeWord, 3);
                        break;
                    case "Rune5":
                        weapon.AddRune(runeWord, 4);
                        break;
                    case "Rune6":
                        weapon.AddRune(runeWord, 5);
                        break;
                }
                ShowRuneSet(weapon);

            }

        }

        private void Cb_Blessed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = e.Source as ComboBox;
            if (armor != null)
            {
                armor.SetBlessing((Blessed.Blessing)cb.SelectedItem);
                tb_BlessingDescription.Text = armor.blessed.GetDescription();

            }
            else if (jewelry != null)
            {
                jewelry.SetBlessing((Blessed.Blessing)cb.SelectedItem);
                tb_BlessingDescription.Text = jewelry.blessed.GetDescription();

            }
            else if (weapon != null)
            {
                weapon.SetBlessing((Blessed.Blessing)cb.SelectedItem);
                tb_BlessingDescription.Text = weapon.blessed.GetDescription();

            }
        }

        private void Cb_Quality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = e.Source as ComboBox;

            if (armor != null)
            {
                switch(cb.SelectedItem)
                {
                    case Item.ItemQuality.Novice:
                        armor.SetItemQuality(Item.ItemQuality.Novice);
                        break;
                    case Item.ItemQuality.Journeyman:
                        armor.SetItemQuality(Item.ItemQuality.Journeyman);
                        break;
                    case Item.ItemQuality.Expert:
                        armor.SetItemQuality(Item.ItemQuality.Expert);
                        break;
                    case Item.ItemQuality.Master:
                        armor.SetItemQuality(Item.ItemQuality.Master);
                        break;
                }
                
            }
            if (jewelry != null)
            {
                switch (cb.SelectedItem)
                {
                    case Item.ItemQuality.Novice:
                        jewelry.SetItemQuality(Item.ItemQuality.Novice);
                        break;
                    case Item.ItemQuality.Journeyman:
                        jewelry.SetItemQuality(Item.ItemQuality.Journeyman);
                        break;
                    case Item.ItemQuality.Expert:
                        jewelry.SetItemQuality(Item.ItemQuality.Expert);
                        break;
                    case Item.ItemQuality.Master:
                        jewelry.SetItemQuality(Item.ItemQuality.Master);
                        break;
                }
            }
            if (weapon != null)
            {
                switch (cb.SelectedItem)
                {
                    case Item.ItemQuality.Novice:
                        weapon.SetItemQuality(Item.ItemQuality.Novice);
                        break;
                    case Item.ItemQuality.Journeyman:
                        weapon.SetItemQuality(Item.ItemQuality.Journeyman);
                        break;
                    case Item.ItemQuality.Expert:
                        weapon.SetItemQuality(Item.ItemQuality.Expert);
                        break;
                    case Item.ItemQuality.Master:
                        weapon.SetItemQuality(Item.ItemQuality.Master);
                        break;
                }
            }
        }
    }
}
