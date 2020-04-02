using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DailyMealPlaner.Business_Layer;

namespace DailyMealPlaner.Data_Layer
{
    interface IProduct
    {
        void AddNewProduct(Product product, string category);

        void AddProductInMealtime(string product, string mealtime);

        bool CheckProductExistence(string product, string category);

        Product CreateNewProduct(string name, string calories, string protein, string fats, string carbs);

        void DeleteProductFromCategory(string category, string product);

        int GetCalories(string product, int weight);

        double GetCarbs(string product, int weight);

        double GetFats(string product, int weight);
        double GetProtein(string product, int weight);

        int GetWeight(string product);

        Product GetProductByName(string name);

        Product GetProductInOriginalList(string product, string category);

        bool IsOriginal(string product, string category);

        List<String> SearchProducts(string strToSearch);
    }
}
