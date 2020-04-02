using DailyMealPlaner.Business_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlaner.Data_Layer
{
    class ProductDao : IProduct
    {
        private static Db db = Db.GetInstance();
        public void AddNewProduct(Product product, string category)
        {
            foreach (Category c in db.Categories)
            {
                if (c.Name == category)
                {
                    c.Products.Add(product);
                    db.SaveCategoryChanges();
                }
            }
        }

        public void AddProductInMealtime(string product, string mealtime)
        {
            foreach (Mealtime m in db.Mealtimes)
            {
                if (m.Name == mealtime)
                {
                    m.Add(GetProductByName(product));
                }
            }
        }

        public bool CheckProductExistence(string product, string category)
        {
            if (GetProductByName(product, category) != null) return true;
            return false;
        }

        public Product CreateNewProduct(string name, string calories, string protein, string fats, string carbs)
        {
            return new Product(name, 100, Convert.ToDouble(protein), Convert.ToDouble(fats), Convert.ToDouble(carbs), Convert.ToDouble(calories));
        }

        public void DeleteProductFromCategory(string category, string product)
        {
            foreach(Category c in db.Categories.ToArray())
            {
                if (GetProductByName(product, category) != null)
                {
                    c.Products.Remove(GetProductByName(product, category));
                    db.SaveCategoryChanges();
                }
            }
        }

        public int GetCalories(string product, int weight)
        {
            return (int)((GetProductByName(product)).Calories / 100 * weight);
        }

        public double GetCarbs(string product, int weight)
        {
            return ((GetProductByName(product)).Carbs / 100 * weight);
        }

        public double GetFats(string product, int weight)
        {
            return ((GetProductByName(product)).Fats / 100 * weight);
        }

        public double GetProtein(string product, int weight)
        {
            return ((GetProductByName(product)).Protein / 100 * weight);
        }

        public int GetWeight(string product)
        {
            return GetProductByName(product).Weight;
        }

        public Product GetProductByName(string name)
        {
            foreach (Category c in db.Categories)
            {
                foreach (Product p in c.Products)
                {
                    if (p.Name == name) return p;
                }
            }
            return null;
        }

        public Product GetProductByName(string name, string category)
        {
            foreach (Category c in db.Categories)
            {
                if (c.Name == category)
                {
                    foreach (Product p in c.Products)
                    {
                        if (p.Name == name) return p;
                    }
                }
            }
            return null;
        }

        public Product GetProductInOriginalList(string product, string category)
        {
            foreach (Category c in db.OriginalCategories)
            {
                if (c.Name == category)
                {
                    foreach (Product p in c.Products)
                    {
                        if (p.Name == product) return p;
                    }
                }
            }
            return null;
        }

        public bool IsOriginal(string product, string category)
        {
            if (GetProductInOriginalList(product, category) != null) return true;
            return false;
        }

        public List<String> SearchProducts(string strToSearch)
        {
            List<String> suitableProducts = new List<String>();
            foreach (Category c in db.Categories)
            {
                foreach (Product p in c.Products)
                {
                    string text = p.ToString().ToLower();
                    if (text.IndexOf(strToSearch) != -1) suitableProducts.Add(p.Name);
                }
            }
            if (strToSearch == "") suitableProducts.Clear();
            return suitableProducts;
        }
    }
}
