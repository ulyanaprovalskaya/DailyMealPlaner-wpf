using DailyMealPlaner.Business_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyMealPlaner.Data_Layer;

namespace DailyMealPlaner.Service_Layer
{
    class Service : IService
    {
        private readonly IProduct productDao = new ProductDao();
        private readonly ICategory categoryDao = new CategoryDao();
        private readonly IMealtime mealtimeDao = new MealtimeDao();
        private readonly IDailyRation dailyRationDao = new DailyRationDao();
        private readonly IUser userDao = new UserDao();


        public void AddNewCategory(string category)
        {
            categoryDao.AddNewCategory(category);
        }

        public void AddNewMealtime(string title)
        {
            mealtimeDao.AddNewMealtime(title);
        }

        public void AddNewProduct(Product product, string category)
        {
            productDao.AddNewProduct(product, category);
        }

        public void AddProductInMealtime(string product, string mealtime)
        {
            //productDao.AddProductInMealtime(product, mealtime);
            mealtimeDao.AddProduct(product, mealtime);
        }

        public bool CheckCategoryExistence(string category)
        {
            return categoryDao.CheckCategoryExistence(category);
        }

        public bool CheckMealtimeExistence(string mealtime)
        {
            return mealtimeDao.CheckMealtimeExistence(mealtime);
        }

        public bool CheckProductExistence(string product, string category)
        {
            return productDao.CheckProductExistence(product, category);
        }

        public void ClearRation()
        {
            dailyRationDao.ClearRation();
            mealtimeDao.SaveMealtimeChanges();
        }

        public Product CreateNewProduct(string name, string calories, string protein, string fats, string carbs)
        {
            return productDao.CreateNewProduct(name, calories, protein, fats, carbs);
        }

        public void DeleteCategory(string category)
        {
            categoryDao.DeleteCategory(category);
        }

        public void DeleteMealtime(string mealtime)
        {
            mealtimeDao.DeleteMealtime(mealtime);
        }

        public void DeleteProductFromCategory(string category, string product)
        {
            productDao.DeleteProductFromCategory(category, product);
        }

        public void DeleteProductFromMealtime(string mealtime, string product)
        {
            mealtimeDao.DeleteProduct(mealtime, product);
        }

        public int GetCalories(string product, int weight)
        {
            return productDao.GetCalories(product, weight);
        }

        public double GetCarbs(string product, int weight)
        {
            return productDao.GetCarbs(product, weight);
        }

        public double GetFats(string product, int weight)
        {
            return productDao.GetFats(product, weight);
        }

        public double GetProtein(string product, int weight)
        {
            return productDao.GetProtein(product, weight);
        }

        public ObservableCollection<Category> GetCategories()
        {
            return categoryDao.GetCategories();
        }

        public Category GetCategoryByProduct(string product)
        {
            return categoryDao.GetCategory(product);
        }

        public Category GetCategoryByName(string category)
        {
            return categoryDao.GetCategoryByName(category);
        }

        public Mealtime getMealtimeByName(string mealtime)
        {
            return mealtimeDao.GetMealtimeByName(mealtime);
        }

        public Mealtime GetMealtimeByProduct(string product)
        {
            return mealtimeDao.GetMealtimeByProduct(product);
        }

        public ObservableCollection<Mealtime> GetMealtimes()
        {
            return mealtimeDao.GetMealtimes();
        }

        public ObservableCollection<Product> GetProducts(string category)
        {
            return categoryDao.GetProducts(category);
        }

        public double GetDailyCaloriesRate()
        {
            return userDao.GetCalories();
        }

        public Product GetProductByName(string product)
        {
            return productDao.GetProductByName(product);
        }

        public int GetRationCalories()
        {
            return mealtimeDao.GetRationCalories();
        }

        public Product GetRationFirstProduct()
        {
            return dailyRationDao.GetRationFirstProduct();
        }

        public int GetUserAge()
        {
            return userDao.GetAge();
        }

        public DailyActivity GetUserActivity()
        {
            return userDao.GetDailyActivity();
        }

        public int GetUserHeight()
        {
            return userDao.GetHeight();
        }

        public double GetUserWeight()
        {
            return userDao.GetWeight();
        }

        public int GetWeight(string product)
        {
            return productDao.GetWeight(product);
        }

        public bool IsOriginalCategory(string category)
        {
            return categoryDao.IsOriginal(category);
        }

        public bool IsOriginalMealtime(string mealtime)
        {
            return mealtimeDao.IsOriginal(mealtime);
        }

        public bool IsOriginalProduct(string product, string category)
        {
            return productDao.IsOriginal(product, category);
        }

        public int RationProductsCount()
        {
            return mealtimeDao.RationProductsCount();
        }

        public void SaveMealtimeChanges()
        {
            mealtimeDao.SaveMealtimeChanges();
        }

        public void SaveUserInfo()
        {
            userDao.SaveInfo();
        }

        public List<String> SearchCategories(string strToSearch)
        {
            return categoryDao.SearchCategories(strToSearch);
        }

        public List<String> SearchProducts(string strToSearch)
        {
            return productDao.SearchProducts(strToSearch);
        }

        public void SetAge(string age)
        {
            userDao.SetAge(age);
            SaveUserInfo();
        }

        public void SetHeight(string height)
        {
            userDao.SetHeight(height);
            SaveUserInfo();
        }

        public void SetWeight(string weight)
        {
            userDao.SetWeight(weight);
            SaveUserInfo();
        }

        public void SetDailyActivity(string dailyActivity)
        {
            userDao.SetDailyActivity(dailyActivity);
            SaveUserInfo();
        }

        public void SetProductWeight(int weight, string product, string mealtime)
        {
            mealtimeDao.SetProductWeight(weight, product, mealtime);
        }
    }
}
