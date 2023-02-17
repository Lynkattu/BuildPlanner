using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace BuilPlanner
{
    public class ItemTooltip
    {
        private ItemList itemList = new ItemList();
        private StackPanel tooltip = new StackPanel
        {
            Background = Brushes.Black,
            Width = 250,
        };

        //Src armor,weapon, jewelry check if itemname can be found in armor, weapon or jewelry list.
        //Make tooltip based on item if it's found.
        public bool SrcArmor(string Itemname)
        {
            Armor armor = new Armor();
            foreach (Armor i in itemList.GetArmorList())
            {
                if (i.GetItemName() == Itemname)
                {
                    armor = i;
                    ArmorTooltip(armor);
                    return true;
                }
            }
            return false;
        }

        public bool SrcWeapon(string Itemname)
        {
            Weapon weapon = new Weapon();
            foreach (Weapon i in itemList.GetWeaponList())
            {
                if (i.GetItemName() == Itemname)
                {
                    weapon = i;
                    WeaponTooltip(weapon);
                    return true;
                }
            }
            return false;
        }

        public bool SrcJewelry(string Itemname)
        {
            Jewelry jewelry = new Jewelry();
            foreach (Jewelry i in itemList.GetJewelryList())
            {
                if (i.GetItemName() == Itemname)
                {
                    jewelry = i;
                    JewelryTooltip(jewelry);
                    return true;
                }
            }
            return false;
        }

        public void ArmorTooltip(Armor armor, bool isEquip = false)
        {
            TextBlock tb_Name = new TextBlock
            {
                Text = armor.GetItemName(),
                FontSize = 18,
                Foreground = armor.GetItemColor(),
                TextAlignment = TextAlignment.Center
            };

            TextBlock tb_Armor = new TextBlock
            {
                Text = "Armor: " + armor.GetArmorValue().ToString(),
                FontSize = 16,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock tb_Weight = new TextBlock
            {
                Text = "Weight: " + armor.GetWeight().ToString("0.0"),
                FontSize = 16,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            tooltip.Children.Add(tb_Name);
            tooltip.Children.Add(tb_Armor);
            tooltip.Children.Add(tb_Weight);
            CreateTbBasedOnItemType(armor, isEquip);
        }

        public void WeaponTooltip(Weapon weapon, bool isEquip = false)
        {
            TextBlock tb_Name = new TextBlock
            {
                Text = weapon.GetItemName(),
                FontSize = 18,
                Foreground = weapon.GetItemColor(),
                TextAlignment = TextAlignment.Center
            };

            TextBlock tb_WeaponClass = new TextBlock
            {
                Text = weapon.GetWeaponSlot() + " " + weapon.GetWeaponClass().ToString(),
                FontSize = 16,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock tb_AttackSpeed = new TextBlock
            {
                Text = "AttackSpeed: " + weapon.GetAttackSpeed().ToString(),
                FontSize = 16,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock tb_Attack = new TextBlock
            {
                Text = "Attack: " + weapon.GetAttack().ToString(),
                FontSize = 16,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock tb_Weight = new TextBlock
            {
                Text = "Weight: " + weapon.GetWeight().ToString("0.0"),
                FontSize = 16,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            tooltip.Children.Add(tb_Name);
            tooltip.Children.Add(tb_WeaponClass);
            tooltip.Children.Add(tb_AttackSpeed);
            tooltip.Children.Add(tb_Attack);
            tooltip.Children.Add(tb_Weight);
            CreateTbBasedOnItemType(weapon, isEquip);
        }

        public void JewelryTooltip(Jewelry jewelry, bool isEquip = false)
        {
            TextBlock tb_Name = new TextBlock
            {
                Text = jewelry.GetItemName(),
                FontSize = 18,
                Foreground = jewelry.GetItemColor(),
                TextAlignment = TextAlignment.Center
            };

            tooltip.Children.Add(tb_Name);
            CreateTbBasedOnItemType(jewelry, isEquip);
        }

        //create list of affix textblocks
        public List<TextBlock> GetAffixTextBlockList()
        {
            Brush textColor = Brushes.LightGreen;
            List<TextBlock> affix_list = new List<TextBlock>();
            TextBlock tb_Affix1 = new TextBlock
            {
                Text = "Affix",
                FontSize = 16,
                Foreground = textColor,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock tb_Affix2 = new TextBlock
            {
                Text = "Affix",
                FontSize = 16,
                Foreground = textColor,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock tb_Affix3 = new TextBlock
            {
                Text = "Affix",
                FontSize = 16,
                Foreground = textColor,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock tb_Affix4 = new TextBlock
            {
                Text = "Affix",
                FontSize = 16,
                Foreground = textColor,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock tb_Affix5 = new TextBlock
            {
                Text = "Affix",
                FontSize = 16,
                Foreground = textColor,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock tb_Affix6 = new TextBlock
            {
                Text = "Affix",
                FontSize = 16,
                Foreground = textColor,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            affix_list.Add(tb_Affix1); affix_list.Add(tb_Affix2); affix_list.Add(tb_Affix3); affix_list.Add(tb_Affix4); affix_list.Add(tb_Affix5); affix_list.Add(tb_Affix6);
            return affix_list;
        }
        public List<TextBlock> GetAffixTextBlockList(Item item)
        {
            Brush textColor = Brushes.LightGreen;
            Dictionary<Item.Affixes, double> affixDict = item.GetAffixDict();
            int affixCount = affixDict.Count;
            List<string> affixes = new List<string>();
            foreach (Item.Affixes i in affixDict.Keys)
            {
                if (i != Item.Affixes.None)
                {
                    affixes.Add(i.ToString() + ": " + affixDict[i].ToString() + "%");
                }
            }

            List<TextBlock> affix_list = new List<TextBlock>();

            if (affixCount >= 1)
            {
                TextBlock tb_Affix1 = new TextBlock
                {
                    Text = affixes[0].ToString(),
                    FontSize = 16,
                    Foreground = textColor,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                affix_list.Add(tb_Affix1);
            }


            if (affixCount >= 2)
            {
                TextBlock tb_Affix2 = new TextBlock
                {
                    Text = affixes[1].ToString(),
                    FontSize = 16,
                    Foreground = textColor,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                affix_list.Add(tb_Affix2);
            }

            if (affixCount >= 3)
            {
                TextBlock tb_Affix3 = new TextBlock
                {
                    Text = affixes[2].ToString(),
                    FontSize = 16,
                    Foreground = textColor,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                affix_list.Add(tb_Affix3);
            }

            if (affixCount >= 4)
            {
                TextBlock tb_Affix4 = new TextBlock
                {
                    Text = affixes[3].ToString(),
                    FontSize = 16,
                    Foreground = textColor,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                affix_list.Add(tb_Affix4);
            }

            if (affixCount >= 5)
            {
                TextBlock tb_Affix5 = new TextBlock
                {
                    Text = affixes[4].ToString(),
                    FontSize = 16,
                    Foreground = textColor,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                affix_list.Add(tb_Affix5);
            }

            if (affixCount >= 6)
            {
                TextBlock tb_Affix6 = new TextBlock
                {
                    Text = affixes[5].ToString(),
                    FontSize = 16,
                    Foreground = textColor,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                affix_list.Add(tb_Affix6);
            }


            return affix_list;
        }
        //create list of rune textblocks
        public List<TextBlock> GetRuneTextBlockList()
        {
            Brush textColor = Brushes.LightSkyBlue;
            List<TextBlock> rune_list = new List<TextBlock>();
            TextBlock tb_Rune1 = new TextBlock
            {
                Text = "Rune Slot",
                FontSize = 16,
                Foreground = textColor,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            TextBlock tb_Rune2 = new TextBlock
            {
                Text = "Rune Slot",
                FontSize = 16,
                Foreground = textColor,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            TextBlock tb_Rune3 = new TextBlock
            {
                Text = "Rune Slot",
                FontSize = 16,
                Foreground = textColor,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            TextBlock tb_Rune4 = new TextBlock
            {
                Text = "Rune Slot",
                FontSize = 16,
                Foreground = textColor,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            TextBlock tb_Rune5 = new TextBlock
            {
                Text = "Rune Slot",
                FontSize = 16,
                Foreground = textColor,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            TextBlock tb_Rune6 = new TextBlock
            {
                Text = "Rune Slot",
                FontSize = 16,
                Foreground = textColor,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            rune_list.Add(tb_Rune1); rune_list.Add(tb_Rune2); rune_list.Add(tb_Rune3); rune_list.Add(tb_Rune4); rune_list.Add(tb_Rune5); rune_list.Add(tb_Rune6);
            return rune_list;
        }

        public List<TextBlock> GetRuneTextBlockList(Item item)
        {
            List<Rune> itemRunes = item.GetRuneWords();
            List<TextBlock> rune_list = new List<TextBlock>();
            string runeDescreption;
            Brush textColor = Brushes.LightSkyBlue;
            if (item.GetRuneCount() >= 1)
            {
                if (itemRunes[0].GetRuneWord() == Rune.RuneWord.Empty)
                {
                    runeDescreption = itemRunes[0].GetRuneWord().ToString();
                }
                else 
                {
                    runeDescreption = itemRunes[0].GetRuneWord() + ": " + itemRunes[0].GetRuneAffix().ToString() + " " + itemRunes[0].GetRuneStatValue().ToString("0.0") + "%";
                }
                TextBlock tb_Rune1 = new TextBlock
                {                    
                    Text = runeDescreption,
                    FontSize = 16,
                    Foreground = textColor,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                rune_list.Add(tb_Rune1);
            }

            if (item.GetRuneCount() >= 2)
            {
                if (itemRunes[1].GetRuneWord() == Rune.RuneWord.Empty)
                {
                    runeDescreption = itemRunes[1].GetRuneWord().ToString();
                }
                else
                {
                    runeDescreption = itemRunes[1].GetRuneWord() + ": " + itemRunes[1].GetRuneAffix().ToString() + " " + itemRunes[1].GetRuneStatValue().ToString("0.0") + "%";
                }
                TextBlock tb_Rune2 = new TextBlock
                {
                    Text = runeDescreption,
                    FontSize = 16,
                    Foreground = textColor,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                rune_list.Add(tb_Rune2);
            }

            if (item.GetRuneCount() >= 3)
            {
                if (itemRunes[2].GetRuneWord() == Rune.RuneWord.Empty)
                {
                    runeDescreption = itemRunes[2].GetRuneWord().ToString();
                }
                else
                {
                    runeDescreption = itemRunes[2].GetRuneWord() + ": " + itemRunes[2].GetRuneAffix().ToString() + " " + itemRunes[2].GetRuneStatValue().ToString("0.0") + "%";
                }
                TextBlock tb_Rune3 = new TextBlock
                {
                    Text = runeDescreption,
                    FontSize = 16,
                    Foreground = textColor,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                rune_list.Add(tb_Rune3);
            }

            if (item.GetRuneCount() >= 4)
            {
                if (itemRunes[3].GetRuneWord() == Rune.RuneWord.Empty)
                {
                    runeDescreption = itemRunes[3].GetRuneWord().ToString();
                }
                else
                {
                    runeDescreption = itemRunes[3].GetRuneWord() + ": " + itemRunes[3].GetRuneAffix().ToString() + " " + itemRunes[3].GetRuneStatValue().ToString("0.0") + "%";
                }
                TextBlock tb_Rune4 = new TextBlock
                {
                    Text = runeDescreption,
                    FontSize = 16,
                    Foreground = textColor,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                rune_list.Add(tb_Rune4);
            }

            if (item.GetRuneCount() >= 5)
            {
                if (itemRunes[4].GetRuneWord() == Rune.RuneWord.Empty)
                {
                    runeDescreption = itemRunes[4].GetRuneWord().ToString();
                }
                else
                {
                    runeDescreption = itemRunes[4].GetRuneWord() + ": " + itemRunes[4].GetRuneAffix().ToString() + " " + itemRunes[4].GetRuneStatValue().ToString("0.0") + "%";
                }
                TextBlock tb_Rune5 = new TextBlock
                {
                    Text = runeDescreption,
                    FontSize = 16,
                    Foreground = textColor,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                rune_list.Add(tb_Rune5);
            }

            if (item.GetRuneCount() >= 6)
            {
                if (itemRunes[5].GetRuneWord() == Rune.RuneWord.Empty)
                {
                    runeDescreption = itemRunes[5].GetRuneWord().ToString();
                }
                else
                {
                    runeDescreption = itemRunes[5].GetRuneWord() + ": " + itemRunes[5].GetRuneAffix().ToString() + " " + itemRunes[5].GetRuneStatValue().ToString("0.0") + "%";
                }
                TextBlock tb_Rune6 = new TextBlock
                {
                    Text = runeDescreption,
                    FontSize = 16,
                    Foreground = textColor,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                rune_list.Add(tb_Rune6);
            }
            return rune_list;
        }

        //create base item stats to tooltip
        public void CreateTbBasedOnItemType(Item item, bool isEquip = false)
        {
            List<TextBlock> affix_list;
            List<TextBlock> rune_list;
            //create textblock based on if item is equip or not
            if (isEquip)
            {
                affix_list = GetAffixTextBlockList(item);
                rune_list = GetRuneTextBlockList(item);
            }
            else
            {
                affix_list = GetAffixTextBlockList();
                rune_list = GetRuneTextBlockList();
            }

            //Create affix texblocks
            if (affix_list.Count() > 0 && isEquip)
            {
                for ( int i = 0; i < affix_list.Count(); i++ )
                {
                    tooltip.Children.Add(affix_list[i]);
                }
            }
            else if (!isEquip)
            {
                for (int i = 0; i < item.GetAffixCount(); i++)
                {
                    tooltip.Children.Add(affix_list[i]);
                }
            }

            //create rune textblocks
            if ( rune_list.Count > 0 && isEquip)
            {
                for (int i = 0; i < rune_list.Count(); i++)
                {
                    tooltip.Children.Add(rune_list[i]);
                }

                if (item.runeSet.GetRuneSet() != RuneSet.RuneSets.None)
                {
                    TextBlock tb_RuneSet = new TextBlock
                    {
                        Text = "Rune set: " + item.runeSet.GetRuneSet().ToString(),
                        FontSize = 16,
                        Foreground = Brushes.Cyan,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    tooltip.Children.Add(tb_RuneSet);

                    TextBlock tb_RuneSetBonus = new TextBlock
                    {
                        Text = item.runeSet.GetDescription().ToString(),
                        FontSize = 12,
                        Foreground = Brushes.Cyan,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        TextWrapping = TextWrapping.Wrap,
                        Margin = new Thickness(10, 10, 10, 10)
                    };
                    tooltip.Children.Add(tb_RuneSetBonus);
                }
            }
            else if(!isEquip)
            {
                for (int i = 0; i < item.GetRuneCount(); i++)
                {
                    tooltip.Children.Add(rune_list[i]);
                }                
            }

            
           
            //create additional textblocks if item type is one of the following item types
            switch (item.GetItemType())
            {
                case Item.ItemType.Artifact:
                    TextBlock tb_Artifact = new TextBlock
                    {
                        Text = item.artifact.GetDescription(),
                        TextWrapping = TextWrapping.Wrap,
                        FontSize = 12,
                        Foreground = item.GetItemColor(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(10, 10, 10, 10)
                    };
                    tooltip.Children.Add(tb_Artifact);
                    break;

                case Item.ItemType.Blessed:
                    if (!isEquip)
                    {
                        TextBlock tb_Blessing = new TextBlock
                        {
                            Text = "Blessing",
                            FontSize = 16,
                            Foreground = item.GetItemColor(),
                            HorizontalAlignment = HorizontalAlignment.Center
                        };
                        tooltip.Children.Add(tb_Blessing);
                    }
                    else
                    {
                        TextBlock tb_Bless = new TextBlock
                        {
                            Text = item.blessed.GetName(),
                            FontSize = 16,
                            Foreground = item.GetItemColor(),
                            HorizontalAlignment = HorizontalAlignment.Center,
                        };

                        TextBlock tb_Blessing = new TextBlock
                        {
                            Text = item.blessed.GetDescription(),
                            FontSize = 12,
                            Foreground = item.GetItemColor(),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            TextWrapping = TextWrapping.Wrap,
                            Margin = new Thickness(10, 10, 10, 10)
                        };
                        tooltip.Children.Add(tb_Bless);
                        tooltip.Children.Add(tb_Blessing);

                    }
                    break;

                case Item.ItemType.Cursed:
                    TextBlock tb_Curse = new TextBlock
                    {
                        Text = item.cursed.GetCurse().ToString() + " Curse",
                        FontSize = 16,
                        Foreground = item.GetItemColor(),
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    tooltip.Children.Add(tb_Curse);

                    TextBlock tb_CurseDesc = new TextBlock
                    {
                        Text = item.cursed.GetDescription(),
                        TextWrapping = TextWrapping.Wrap,
                        FontSize = 12,
                        Foreground = item.GetItemColor(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(10, 10, 10, 10)
                    };
                    tooltip.Children.Add(tb_CurseDesc);
                    break;
            }

        }

        public StackPanel GetItemTooltip()
        {
            return tooltip;
        }

        public void ClearTooltipChildens()
        {
            tooltip.Children.Clear();
        }
    }
}
