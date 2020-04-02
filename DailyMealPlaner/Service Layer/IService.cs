using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DailyMealPlaner.Business_Layer;

namespace DailyMealPlaner.Service_Layer
{
    interface IService
    {
        ObservableCollection<Category> GetCategories();

        ObservableCollection<Mealtime> GetMealtimes();

        ObservableCollection<Product> GetProducts(string category);

        Category GetCategoryByProduct(string product);

        void AddNewCategory(string category);

        void AddNewMealtime(string title);

        void AddNewProduct(Product product, string category);

        void AddProductInMealtime(string product, string mealtime);


        bool CheckCategoryExistence(string category);

        bool CheckMealtimeExistence(string mealtime);

        bool CheckProductExistence(string product, string category);

        void ClearRation();

        Product CreateNewProduct(string name, string calories, string protein, string fats, string carbs);

        void DeleteMealtime(string mealtime);

        void DeleteCategory(string category);

        void DeleteProductFromCategory(string category, string product);

        void DeleteProductFromMealtime(string mealtime, string product);

        double GetDailyCaloriesRate();

        int GetCalories(string product, int weight);

        double GetCarbs(string product, int weight);

        double GetFats(string product, int weight);

        Product GetProductByName(string product);

        Category GetCategoryByName(string category);

        int GetRationCalories();

        double GetProtein(string product, int weight);

        Mealtime getMealtimeByName(string mealtime);

        Mealtime GetMealtimeByProduct(string product);

        Product GetRationFirstProduct();

        int GetUserAge();

        DailyActivity GetUserActivity();

        int GetUserHeight();

        double GetUserWeight();

        int GetWeight(string product);

        bool IsOriginalCategory(string category);

        bool IsOriginalMealtime(string mealtime);

        bool IsOriginalProduct(string product, string category);

        int RationProductsCount();

        void SaveMealtimeChanges();

        void SaveUserInfo();

        List<String> SearchCategories(string strToSearch);
        List<String> SearchProducts(string strToSearch);

        void SetAge(string age);

        void SetHeight(string height);

        void SetWeight(string weight);

        void SetDailyActivity(string dailyActivity);

        void SetProductWeight(int weight, string product, string mealtime);
    }
}
