using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DailyMealPlaner.Business_Layer;

namespace DailyMealPlaner.Data_Layer
{
    interface ICategory
    {
        ObservableCollection<Category> GetCategories();

        Category GetCategory(string product);

        void AddNewCategory(string title);

        bool CheckCategoryExistence(string category);

        void DeleteCategory(string category);

        ObservableCollection<Product> GetProducts(string category);

        Category GetCategoryByName(string name);

        bool IsOriginal(string category);

        List<String> SearchCategories(string strToSerch);

    }
}
