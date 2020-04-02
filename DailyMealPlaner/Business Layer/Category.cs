using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using DailyMealPlaner.Presentation_Layer;

namespace DailyMealPlaner.Business_Layer 
{
    public class Category : TreeViewItemBase, IXmlSerializable
    {
        String name;
        ObservableCollection<Product> products;

        public ObservableCollection<Product> Products { get => products; set => products = value; }
        public String Name { get => name; set => name = value; }

        public Category()
        {
            products = new ObservableCollection<Product>();
        }

        public Category(String name)
        {
            this.name = name;
            this.products = new ObservableCollection<Product>();
        }

        public Category(String name, ObservableCollection<Product> products)
        {
            this.name = name;
            this.products = products;
        }

        public void Add(Product product)
        {
            this.products.Add(product);
        }

        public void Remove(Product product)
        {
            this.products.Remove(product);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.LocalName == "Category") name = reader.GetAttribute("name");
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

        public override string ToString()
        {
            return name;
        }


    }
}
