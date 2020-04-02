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
    [Serializable]
    public class Categories : IXmlSerializable
    {
        ObservableCollection<Category> categories;

        public ObservableCollection<Category> CategoriesList { get => categories; set => categories = value; }

        public Categories()
        {
            categories = new ObservableCollection<Category>(); 
        }

        public Categories(ObservableCollection<Category> categories)
        {
            this.categories = categories;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadToDescendant("Category");
            while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Category")
            {
                Category c = new Category();
                c.ReadXml(reader);
                categories.Add(c);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (Category c in categories)
            {
                writer.WriteStartElement("Category");
                c.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
    }
}
