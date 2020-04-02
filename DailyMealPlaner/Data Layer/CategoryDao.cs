using DailyMealPlaner.Business_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlaner.Data_Layer
{
    class CategoryDao : ICategory
    {
        private static Db db = Db.GetInstance();

        public void AddNewCategory(string title)
        {
            db.Categories.Add(new Category(title));
            db.SaveCategoryChanges();
        }

        public bool CheckCategoryExistence(string category)
        {
            if (GetCategoryByName(category) != null) return true;
            return false;
        }

        public void DeleteCategory(string category)
        {
            db.Categories.Remove(GetCategoryByName(category));
            db.SaveCategoryChanges();
        }

        public ObservableCollection<Category> GetCategories()
        {
            return db.Categories;
        }

        public Category GetCategory(string product)
        {
            foreach(Category c in GetCategories())
            {
                foreach(Product p in GetProducts(c.Name))
                {
                    if (p.Name == product) return c;
                }
            }
            return null;
        }

        public Category GetOriginalCategory(string category)
        {
            foreach (Category c in db.OriginalCategories)
            {
                if (c.Name == category) return c; 
            }
            return null;
        }

        public Category GetCategoryByName(string name)
        {
            foreach(Category c in GetCategories())
            {
                if (c.Name == name) return c;
            }
            return null;
        }

        public ObservableCollection<Product> GetProducts(string category)
        {
            foreach(Category c in GetCategories())
            {
                if (c.Name == category) return c.Products; 
            }
            return null;
        }

        public bool IsOriginal(string category)
        {
            if (GetOriginalCategory(category) != null) return true;
            return false;
        }

        public List<String> SearchCategories(string strToSearch)
        {
            List<String> suitableCategories= new List<String>();
            foreach (Category c in db.Categories)
            {
                string text = c.ToString().ToLower();
                if (text.IndexOf(strToSearch) != -1) suitableCategories.Add(c.Name);
            }
            if (strToSearch == "") suitableCategories.Clear();
            return suitableCategories;
        }
    }
}
