using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DailyMealPlaner.Business_Layer;

namespace DailyMealPlaner.Data_Layer
{
    interface IMealtime
    {
        ObservableCollection<Mealtime> GetMealtimes();

        void AddNewMealtime(String title);

        void AddProduct(string product, string mealtime);

        bool CheckMealtimeExistence(string mealtime);

        void DeleteMealtime(string mealtime);

        void DeleteProduct(string mealtime, string product);

        int GetRationCalories();

        Mealtime GetMealtimeByName(string name);

        Mealtime GetMealtimeByProduct(string product);

        Product GetProductByName(string name);

        bool IsOriginal(string mealtime);

        int RationProductsCount();

        void SaveMealtimeChanges();

        void SetProductWeight(int weight, string product, string mealtime);

    }
}
