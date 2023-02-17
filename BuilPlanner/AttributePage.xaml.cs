using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BuilPlanner
{
    /// <summary>
    /// Interaction logic for AttributePage.xaml
    /// </summary>
    public partial class AttributePage : Page
    {
        private readonly Attributes attrib = new Attributes();
        private Dictionary<Attributes.Attribute, int> attributes;

        private int attribPerLevel = 8;
        private bool shiftDown = false;
        private bool ctrlDown = false;
        public AttributePage(Dictionary<Attributes.Attribute, int> att)           
        {
            InitializeComponent();
            attributes = new Dictionary<Attributes.Attribute, int>(att);
            attrib.UpdateAttributes(attributes);
            UpdateAttributes();
        }
        private void UpdateAttributes()
        {
            TbLevelValue.Text = attrib.GetLevel().ToString();
            TbStrengthValue.Text = attributes[Attributes.Attribute.Strength].ToString();
            TbDexterityValue.Text = attributes[Attributes.Attribute.Dexterity].ToString();
            TbIntelligenceValue.Text = attributes[Attributes.Attribute.Intelligence].ToString();
            TbWillpowerValue.Text = attributes[Attributes.Attribute.Willpower].ToString();
            TbConstitutionValue.Text = attributes[Attributes.Attribute.Constitution].ToString();
            TbEnduranceValue.Text = attributes[Attributes.Attribute.Endurance].ToString();
            TbAgilityValue.Text = attributes[Attributes.Attribute.Agility].ToString();
            TbWisdomValue.Text = attributes[Attributes.Attribute.Wisdom].ToString();
            TbCharismaValue.Text = attributes[Attributes.Attribute.Charisma].ToString();
            TbRemainAttrib.Text = attrib.GetRemainAttributes().ToString();

            TbThoughness.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Thoughness).ToString();
            TbEquipload.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Equipload).ToString();
            TbPrecision.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Precision).ToString();
            TbDodge.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Dodge).ToString();
            TbMana.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Mana).ToString();
            TbMending.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Mending).ToString();
            TbConcentration.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Concentration).ToString();
            TbEfficiency.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Efficiency).ToString();
            TbLife.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Life).ToString();
            TbBlock.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Block).ToString();
            TbStamina.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Stamina).ToString();
            TbPoise.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Poise).ToString();
            TbHaste.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Haste).ToString();
            TbQuickness.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Quickness).ToString();
            TbPenetration.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Penetration).ToString();
            TbAlacrity.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Alacrity).ToString();
            TbResilence.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Resilence).ToString();
            TbPerspicacious.Text = attrib.GetAttributeStat(Attributes.AttributeStat.Perspicacious).ToString();
        }
        private void ShiftDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.LeftShift)
            {
                shiftDown = true;
            }
            if (e.Key == Key.LeftCtrl)
            {
                ctrlDown = true;
            }
        }

        private void ShiftUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift)
            {
                shiftDown = false;
            }
            if (e.Key == Key.LeftCtrl)
            {
                ctrlDown = false;
            }
        }

        private void DecreaseAttribute(Attributes.Attribute attributeType)
        {
            Dictionary<Attributes.Attribute, int> baseAtt = attrib.GetBaseAttributes();

            if (attributes[attributeType] > baseAtt[attributeType])
            {
                if (shiftDown && !ctrlDown)
                {
                        
                    if (attributes[attributeType] - 10 < baseAtt[attributeType])
                    {                     
                        attrib.SetRemainAttributes(attrib.GetRemainAttributes() + Math.Abs(baseAtt[attributeType] - attributes[attributeType]));
                        attributes[attributeType] = baseAtt[attributeType];
                    }
                    else
                    {
                        attributes[attributeType] -= 10;
                        attrib.SetRemainAttributes(attrib.GetRemainAttributes() + 10);
                    }
                    
                }
                else if (ctrlDown)
                {

                    attrib.SetRemainAttributes(attributes[attributeType] - baseAtt[attributeType]);
                    attributes[attributeType] = baseAtt[attributeType];
                }
                else
                {
                    attributes[attributeType]--;
                    attrib.SetRemainAttributes(attrib.GetRemainAttributes() + 1);
                }
            }
        }

        private void IncreaseAttribute(Attributes.Attribute attributeType)
        {
            if (attrib.GetRemainAttributes() > 0)
            {
                if (shiftDown && !ctrlDown)
                {
                    if (attrib.GetRemainAttributes() < 10)
                    {
                        attributes[attributeType] += attrib.GetRemainAttributes();
                        attrib.SetRemainAttributes(0);
                    }
                    else
                    {
                        attributes[attributeType] += 10;
                        attrib.SetRemainAttributes(attrib.GetRemainAttributes() - 10);
                    }
                }
                else if (ctrlDown)
                {
                    attributes[attributeType] += attrib.GetRemainAttributes();
                    attrib.SetRemainAttributes(0);

                }
                else
                {
                    attributes[attributeType]++;
                    attrib.SetRemainAttributes(attrib.GetRemainAttributes() - 1);
                }
            }
        }

        private void BtIncreaseLevel_Click(object sender, RoutedEventArgs e)
        {
            if (attrib.GetLevel() < 80)
            {
                if (shiftDown && !ctrlDown)
                {
                    if (attrib.GetLevel() <= 70)
                    {
                        attrib.SetLevel(attrib.GetLevel() + 10);
                        attrib.SetRemainAttributes(attrib.GetRemainAttributes() + 10 * attribPerLevel);
                    }
                    else 
                    {
                        attrib.SetRemainAttributes(attrib.GetRemainAttributes() + (Math.Abs(attrib.GetLevel() - 80) * attribPerLevel));
                        attrib.SetLevel(80);                     
                    }
                }
                else if (ctrlDown)
                {

                    attrib.SetRemainAttributes(attrib.GetRemainAttributes() + (Math.Abs(attrib.GetLevel() - 80) * attribPerLevel));
                    attrib.SetLevel(80);
                }
                else
                {
                    attrib.SetLevel(attrib.GetLevel() + 1);
                    attrib.SetRemainAttributes(attrib.GetRemainAttributes() + attribPerLevel);
                }

                TbLevelValue.Text = attrib.GetLevel().ToString();
                TbRemainAttrib.Text = attrib.GetRemainAttributes().ToString();
                attrib.UpdateAttributes(attributes);
            }
        }

        private void BtDecreaseLevel_Click(object sender, RoutedEventArgs e)
        {
            if (attrib.GetLevel() > 1 && attrib.GetRemainAttributes() >= attribPerLevel)
            {
                if (shiftDown && !ctrlDown)
                {                    
                    if (attrib.GetLevel() <= 10 || attrib.GetRemainAttributes() < attribPerLevel * 10)
                    {
                        int div = attrib.GetRemainAttributes() / attribPerLevel;
                        attrib.SetLevel(attrib.GetLevel() - div);
                        attrib.SetRemainAttributes(attrib.GetRemainAttributes() - (div * attribPerLevel));
                    }
                    else
                    {
                        attrib.SetLevel(attrib.GetLevel() - 10);
                        attrib.SetRemainAttributes(attrib.GetRemainAttributes() - (10 * attribPerLevel));
                    }

                }
                else if (ctrlDown)
                {
                    int div = attrib.GetRemainAttributes() / attribPerLevel;
                    attrib.SetLevel(attrib.GetLevel() - div);
                    attrib.SetRemainAttributes(attrib.GetRemainAttributes() - (div * attribPerLevel));
                }
                else
                {
                    attrib.SetLevel(attrib.GetLevel() - 1);
                    attrib.SetRemainAttributes(attrib.GetRemainAttributes() - attribPerLevel);
                }
                UpdateAttributes();
                attrib.UpdateAttributes(attributes);
            }
        }

        private void BtIncreaseStrength_Click(object sender, RoutedEventArgs e)
        {
            if (attrib.GetRemainAttributes() >= 1)
            {
                IncreaseAttribute(Attributes.Attribute.Strength);
                attrib.UpdateAttributes(attributes);
                UpdateAttributes();
            }
        }

        private void BtDecreaseStrength_Click(object sender, RoutedEventArgs e)
        {
            DecreaseAttribute(Attributes.Attribute.Strength);
            attrib.UpdateAttributes(attributes);
            UpdateAttributes();
        }

        private void BtIncreaseDexterity_Click(object sender, RoutedEventArgs e)
        {
            if (attrib.GetRemainAttributes() >= 1)
            {
                IncreaseAttribute(Attributes.Attribute.Dexterity);
                attrib.UpdateAttributes(attributes);
                UpdateAttributes();
            }
        }

        private void BtDecreaseDexterity_Click(object sender, RoutedEventArgs e)
        {
            DecreaseAttribute(Attributes.Attribute.Dexterity);
            attrib.UpdateAttributes(attributes);
            UpdateAttributes();
        }

        private void BtIncreaseIntelligence_Click(object sender, RoutedEventArgs e)
        {
            if (attrib.GetRemainAttributes() >= 1)
            {
                IncreaseAttribute(Attributes.Attribute.Intelligence);
                attrib.UpdateAttributes(attributes);
                UpdateAttributes();
            }
        }

        private void BtDecreaseIntelligence_Click(object sender, RoutedEventArgs e)
        {
            DecreaseAttribute(Attributes.Attribute.Intelligence);
            attrib.UpdateAttributes(attributes);
            UpdateAttributes();
        }

        private void BtIncreaseWillpower_Click(object sender, RoutedEventArgs e)
        {
            if (attrib.GetRemainAttributes() >= 1)
            {
                IncreaseAttribute(Attributes.Attribute.Willpower);
                attrib.UpdateAttributes(attributes);
                UpdateAttributes();
            }
        }

        private void BtDecreaseWillpower_Click(object sender, RoutedEventArgs e)
        {
            DecreaseAttribute(Attributes.Attribute.Willpower);
            attrib.UpdateAttributes(attributes);
            UpdateAttributes();
        }

        private void BtIncreaseConstitution_Click(object sender, RoutedEventArgs e)
        {
            if (attrib.GetRemainAttributes() >= 1)
            {
                IncreaseAttribute(Attributes.Attribute.Constitution);
                attrib.UpdateAttributes(attributes);
                UpdateAttributes();
            }
        }

        private void BtDecreaseConstitutution_Click(object sender, RoutedEventArgs e)
        {
            DecreaseAttribute(Attributes.Attribute.Constitution);
            attrib.UpdateAttributes(attributes);
            UpdateAttributes();
        }

        private void BtIncreaseEndurance_Click(object sender, RoutedEventArgs e)
        {
            if (attrib.GetRemainAttributes() >= 1)
            {
                IncreaseAttribute(Attributes.Attribute.Endurance);
                attrib.UpdateAttributes(attributes);
                UpdateAttributes();
            }
        }

        private void BtDecreaseEndurance_Click(object sender, RoutedEventArgs e)
        {
            DecreaseAttribute(Attributes.Attribute.Endurance);
            attrib.UpdateAttributes(attributes);
            UpdateAttributes();
        }

        private void BtIncreaseAgility_Click(object sender, RoutedEventArgs e)
        {
            if (attrib.GetRemainAttributes() >= 1)
            {
                IncreaseAttribute(Attributes.Attribute.Agility);
                attrib.UpdateAttributes(attributes);
                UpdateAttributes();
            }
        }

        private void BtDecreaseAgility_Click(object sender, RoutedEventArgs e)
        {
            DecreaseAttribute(Attributes.Attribute.Agility);
            attrib.UpdateAttributes(attributes);
            UpdateAttributes();
        }

        private void BtIncreaseWisdom_Click(object sender, RoutedEventArgs e)
        {
            if (attrib.GetRemainAttributes() >= 1)
            {
                IncreaseAttribute(Attributes.Attribute.Wisdom);
                attrib.UpdateAttributes(attributes);
                UpdateAttributes();
            }
        }

        private void BtDecreaseWisdom_Click(object sender, RoutedEventArgs e)
        {
            DecreaseAttribute(Attributes.Attribute.Wisdom);
            attrib.UpdateAttributes(attributes);
            UpdateAttributes();
        }

        private void BtIncreaseCharisma_Click(object sender, RoutedEventArgs e)
        {
            if (attrib.GetRemainAttributes() >= 1)
            {
                IncreaseAttribute(Attributes.Attribute.Charisma);
                attrib.UpdateAttributes(attributes);
                UpdateAttributes();
            }
        }

        private void BtDecreaseCharisma_Click(object sender, RoutedEventArgs e)
        {
            DecreaseAttribute(Attributes.Attribute.Charisma);
            attrib.UpdateAttributes(attributes);
            UpdateAttributes();
        }

        private void BtResetAttributes_Click(object sender, RoutedEventArgs e)
        {
            attrib.ResetAtrributes();
            UpdateAttributes();
        }
    }
}
