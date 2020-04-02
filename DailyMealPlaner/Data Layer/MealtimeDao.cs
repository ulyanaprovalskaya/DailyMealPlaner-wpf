using DailyMealPlaner.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DailyMealPlaner.Data_Layer
{
    class MealtimeDao : IMealtime
    {
        private static Db db = Db.GetInstance();
        public void AddNewMealtime(String title)
        {
            db.Mealtimes.Add(new Mealtime(title));
            db.SaveMealtimeChanges();
        }

        public void AddProduct(string product, string mealtime)
        {
            foreach (Mealtime m in db.Mealtimes)
            {
                if (m.Name == mealtime)
                {
                    Product prod = new Product();
                    foreach (Category c in db.Categories)
                    {
                        foreach (Product p in c.Products)
                        {
                            if (p.Name == product) prod = p;
                        }
                    }
                    m.Add(prod);
                }
            }
        }

        public bool CheckMealtimeExistence(string mealtime)
        {
            if (GetMealtimeByName(mealtime) != null) return true;
            return false;
        }

        public void DeleteMealtime(string mealtime)
        {
            foreach (Mealtime m in GetMealtimes().ToArray())
            {
                if (m.Name == mealtime) db.Mealtimes.Remove(m);
                db.SaveMealtimeChanges();
            }
        }

        public void DeleteProduct(string mealtime, string product)
        {
            foreach (Product p in GetMealtimeByName(mealtime).Products.ToArray())
            {
                if (p.Name == product) GetMealtimeByName(mealtime).Products.Remove(p);
                db.SaveMealtimeChanges();
            }
        }

        public int GetRationCalories()
        {
            int calories = 0;
            foreach(Mealtime m in db.Mealtimes)
            {
                foreach(Product p in m.Products)
                {
                    calories += (int)(p.Calories / 100 * p.Weight);
                }
            }
            return calories;
        }

        public Mealtime GetMealtimeByName(string name)
        {
            foreach(Mealtime m in db.Mealtimes)
            {
                if (m.Name == name) return m;
            }
            return null;
        }

        public ObservableCollection<Mealtime> GetMealtimes()
        {
            return db.Mealtimes;
        }

        public Mealtime GetMealtimeByProduct(string product)
        {
            foreach(Mealtime m in db.Mealtimes)
            {
                foreach(Product p in m.Products)
                {
                    if (p.Name == product) return m;
                }
            }
            return null;
        }

        public Product GetProductByName(string product)
        {
            foreach (Mealtime m in db.Mealtimes)
            {
                foreach (Product p in m.Products)
                {
                    if (p.Name == product) return p;
                }
            }
            return null;
        }

        public bool IsOriginal(string mealtime)
        {
            foreach(Mealtime m in db.OriginalMealtimes)
            {
                if (m.Name == mealtime) return true;
            }
            return false;
        }

        public int RationProductsCount()
        {
            int count = 0;
            foreach(Mealtime m in db.Mealtimes)
            {
                count += m.Products.Count;
            }
            return count;
        }

        public void SaveMealtimeChanges()
        {
            db.DailyRation.Mealtimes = db.Mealtimes; 
            db.SaveMealtimeChanges();
        }

        public void SetProductWeight(int weight, string product, string mealtime)
        {
            foreach(Product p in GetMealtimeByName(mealtime).Products)
            {
                if (p.Name == product) p.Weight = weight;
            }
        }
    }
}
