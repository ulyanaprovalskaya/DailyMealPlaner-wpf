using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.ObjectModel;
using DailyMealPlaner.Business_Layer; 

namespace DailyMealPlaner
{
    class Serializer<T>
    {
        public static void Serialize(T obj, string filepath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, obj);
            }
        }

        public static T Deserialize(T obj, String filepath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                obj = (T)deserializer.Deserialize(fs);

            }
            return obj;
        }
    }
}