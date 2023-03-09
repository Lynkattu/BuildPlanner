using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilPlanner
{
    public struct Lib
    {
        public enum Origin { Artist, Believer, Criminal, Occultist, Primitive, Scholar }
        public enum Background { Bard, Dancer, Jester, Mobed, Monk, Priest, Assassin, Pirate, Thief, Necromancer, Warlock, Sorcerer, Druid, Hofgothi, Shaman, Artificer, Alchemist, Wizard }


        public List<Background> GetBackgroundItem(Origin origin)
        {
            List<Background> backgroundItems = new List<Background>();
            switch (origin)
            {
                case Origin.Artist:
                    backgroundItems.Add(Background.Bard);
                    backgroundItems.Add(Background.Dancer);
                    backgroundItems.Add(Background.Jester);
                    //CbBackground.SelectedItem = subjectsList.First(i => i.Property == "DefaultChatSubject");
                    break;
                case Origin.Believer:
                    backgroundItems.Add(Background.Mobed);
                    backgroundItems.Add(Background.Monk);
                    backgroundItems.Add(Background.Priest);
                    break;
                case Origin.Criminal:
                    backgroundItems.Add(Background.Assassin);
                    backgroundItems.Add(Background.Pirate);
                    backgroundItems.Add(Background.Thief);
                    break;
                case Origin.Occultist:
                    backgroundItems.Add(Background.Necromancer);
                    backgroundItems.Add(Background.Sorcerer);
                    backgroundItems.Add(Background.Warlock);
                    break;
                case Origin.Primitive:
                    backgroundItems.Add(Background.Druid);
                    backgroundItems.Add(Background.Hofgothi);
                    backgroundItems.Add(Background.Shaman);
                    break;
                case Origin.Scholar:
                    backgroundItems.Add(Background.Alchemist);
                    backgroundItems.Add(Background.Artificer);
                    backgroundItems.Add(Background.Wizard);
                    break;
            }
            return backgroundItems;
        }

        public List<Origin> GetOriginItemList()
        {
            List<Origin> originList = new List<Origin>();

            foreach (Origin rw in (Origin[])Enum.GetValues(typeof(Origin)))
            {
                originList.Add(rw);
            }
            return originList;
        }

        public string GetOriginImage(Origin origin) 
        {
            string path = "";
            switch (origin)
            {
                case Lib.Origin.Criminal:
                    path = "pack://application:,,,/BuilPlanner;component/Images/Criminal_Background.jpg";
                    break;
                case Lib.Origin.Occultist:
                    path = "pack://application:,,,/BuilPlanner;component/Images/Occultist_Background.jpg";
                    break;
                case Lib.Origin.Artist:
                    path = "pack://application:,,,/BuilPlanner;component/Images/Artist_Background.jpg";
                    break;
                case Lib.Origin.Believer:
                    path = "pack://application:,,,/BuilPlanner;component/Images/Believer_Background.jpg";
                    break;
                case Lib.Origin.Primitive:
                    path = "pack://application:,,,/BuilPlanner;component/Images/Primitive_Background.jpg";
                    break;
                case Lib.Origin.Scholar:
                    path = "pack://application:,,,/BuilPlanner;component/Images/Scholar_Background.jpg";
                    break;
            }
            return path;
        }
    }
}
