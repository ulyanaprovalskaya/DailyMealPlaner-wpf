using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace DailyMealPlaner.Business_Layer
{
    [Serializable, XmlRoot("DailyRation")]
    public class DailyRation : IXmlSerializable
    {
        ObservableCollection<Mealtime> mealtimes;
        public ObservableCollection<Mealtime> Mealtimes { get => mealtimes; set => mealtimes = value; }
        public double TotalCalories
        {
            get
            {
                double calories = 0;
                foreach (Mealtime m in mealtimes)
                {
                    foreach (Product p in m.Products)
                    {
                        calories += (p.Calories / 100) * p.Weight;
                    }
                }
                return calories;
            }
        }

        public DailyRation()
        {
            mealtimes = new ObservableCollection<Mealtime>();
        }


        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadToDescendant("Mealtime");
            while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Mealtime")
            {
                Mealtime m = new Mealtime();
                m.ReadXml(reader);
                mealtimes.Add(m);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (Mealtime m in mealtimes)
            {
                writer.WriteStartElement("Mealtime");
                m.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
    }
}
