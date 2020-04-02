using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using DailyMealPlaner.Presentation_Layer;

namespace DailyMealPlaner.Business_Layer
{
    public class Mealtime : TreeViewItemBase, IXmlSerializable
    {
        String name;
        ObservableCollection<Product> products;

        public string Name { get => name; set => name= value; }
        public ObservableCollection<Product> Products { get => products; set => products = value; }

        public Mealtime()
        {
            name = null;
            products = new ObservableCollection<Product>();
        }

        public Mealtime(String name)
        {
            this.name = name;
            products = new ObservableCollection<Product>();
        }

        public void Add(Product product)
        {
            this.products.Add(product);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.LocalName == "Mealtime") name = reader.GetAttribute("name");
            reader.ReadToDescendant("Product");
            while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Product")
            {
                Product prod = new Product();
                prod.ReadXml(reader);
                products.Add(prod);
            }
            reader.Read();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("name", name);
            foreach (Product p in products)
            {
                writer.WriteStartElement("Product");
                p.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
    }
}
