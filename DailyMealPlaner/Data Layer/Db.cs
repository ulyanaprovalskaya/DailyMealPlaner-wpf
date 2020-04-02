using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Collections.ObjectModel;
using DailyMealPlaner.Business_Layer;

namespace DailyMealPlaner.Data_Layer
{
    class Db
    {
        readonly static String productsFilepath = @"SourceFiles\Products.xml";
        readonly static String originalProductsFilepath = @"SourceFiles\OriginalProducts.xml";
        readonly static String mealtimesFilepath = @"SourceFiles\Mealtimes.xml";
        readonly static String originalMealtimesFilepath = @"SourceFiles\OriginalMealtimes.xml";
        readonly static String userInfoFilepath = @"SourceFiles\User.txt";

        ObservableCollection<Category> categories;
        ObservableCollection<Category> originalCategories;
        ObservableCollection<Mealtime> mealtimes;
        ObservableCollection<Mealtime> originalMealtimes;
        DailyRation dailyRation;
        User user;

        private static Db instance;

        public ObservableCollection<Category> Categories { get => categories; set => categories = value; }
        public ObservableCollection<Category> OriginalCategories { get => originalCategories; set => originalCategories = value; }
        public ObservableCollection<Mealtime> Mealtimes { get => mealtimes; set => mealtimes = value; }
        public ObservableCollection<Mealtime> OriginalMealtimes { get => originalMealtimes; set => originalMealtimes = value; }
        public User User { get => user; set => user = value; }
        public DailyRation DailyRation { get => dailyRation; set => dailyRation = value; }

        private Db()
        {
            user = new User();
            Read();
        }

        public static Db GetInstance()
        {
            if (instance == null) instance = new Db();
            return instance;
        }

        private void Read()
        {
            Categories categories = new Categories();
            categories = Serializer<Categories>.Deserialize(categories, productsFilepath);
            this.categories = categories.CategoriesList;

            categories = new Categories();
            categories = Serializer<Categories>.Deserialize(categories, originalProductsFilepath);
            this.originalCategories = categories.CategoriesList;

            dailyRation = new DailyRation();
            dailyRation = Serializer<DailyRation>.Deserialize(dailyRation, originalMealtimesFilepath);
            this.originalMealtimes = dailyRation.Mealtimes;

            dailyRation = new DailyRation();
            dailyRation = Serializer<DailyRation>.Deserialize(dailyRation, mealtimesFilepath);
            this.mealtimes = dailyRation.Mealtimes;

            ReadUserInfo();
        }

        private void ReadUserInfo()
        {
            try
            {
                using (StreamReader reader = new StreamReader(userInfoFilepath))
                {
                    user.Age = Convert.ToInt32(reader.ReadLine());
                    user.Height = Convert.ToInt32(reader.ReadLine());
                    user.Weight = Convert.ToDouble(reader.ReadLine());
                    user.DailyActivity = (DailyActivity)Enum.Parse(typeof(DailyActivity), reader.ReadLine());
                }
            }
            catch { }
        }

        public void WriteUserInfo()
        {
            using (StreamWriter writer = new StreamWriter(userInfoFilepath))
            {
                writer.WriteLine(user.Age);
                writer.WriteLine(user.Height);
                writer.WriteLine(user.Weight);
                writer.WriteLine(user.DailyActivity);
            }
        }

        public void SaveCategoryChanges()
        {
            Categories categories = new Categories();
            categories.CategoriesList = this.Categories;
            Serializer<Categories>.Serialize(categories, productsFilepath);
        }

        public void SaveMealtimeChanges()
        {
            dailyRation = new DailyRation();
            dailyRation.Mealtimes = this.Mealtimes;
            Serializer<DailyRation>.Serialize(dailyRation, mealtimesFilepath);
        }
    }
}
