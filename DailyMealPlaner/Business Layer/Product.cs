using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using DailyMealPlaner.Presentation_Layer;

namespace DailyMealPlaner.Business_Layer
{
    public class Product : TreeViewItemBase, IXmlSerializable
    {
        string name;
        int weight;
        double protein;
        double fats;
        double carbs;
        double calories;

        public Product() { }

        public Product(string name, int weight, double protein, double fats, double carbs, double calories)
        {
            this.name = name;
            this.weight = weight;
            this.protein = protein;
            this.fats = fats;
            this.carbs = carbs;
            this.calories = calories;
        }

        public string Name { get => name; set => name = value; }
        public int Weight { get => weight; set => weight = value; }
        public double Protein { get => protein; set => protein = value; }
        public double Fats { get => fats; set => fats = value; }
        public double Carbs { get => carbs; set => carbs = value; }
        public double Calories { get => calories; set => calories = value; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

            info.AddValue("name", this.name);
            info.AddValue("weight", this.weight);
            info.AddValue("protein", this.protein);
            info.AddValue("fats", this.fats);
            info.AddValue("carbs", this.carbs);
            info.AddValue("calories", this.calories);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }


        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement();
            name = reader.ReadElementString("Name");
            weight = Convert.ToInt32(reader.ReadElementString("Gramms"));
            protein = Convert.ToDouble(reader.ReadElementString("Protein"));
            fats = Convert.ToDouble(reader.ReadElementString("Fats"));
            carbs = Convert.ToDouble(reader.ReadElementString("Carbs"));
            calories = Convert.ToDouble(reader.ReadElementString("Calories"));
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Name", name);
            writer.WriteElementString("Gramms", weight.ToString());
            writer.WriteElementString("Protein", protein.ToString());
            writer.WriteElementString("Fats", fats.ToString());
            writer.WriteElementString("Carbs", carbs.ToString());
            writer.WriteElementString("Calories", calories.ToString());
        }

        public override string ToString()
        {
            return name;
        }
    }
}
