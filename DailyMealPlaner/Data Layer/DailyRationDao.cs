using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DailyMealPlaner.Business_Layer;

namespace DailyMealPlaner.Data_Layer
{
    class DailyRationDao : IDailyRation
    {
        private static Db db = Db.GetInstance();

        public void ClearRation()
        {
            foreach(Mealtime m in db.Mealtimes)
            {
                m.Products.Clear();
            }
        }

        public Product GetRationFirstProduct()
        {
            foreach(Mealtime m in db.Mealtimes)
            {
                foreach (Product p in m.Products) return p;
            }
            return null;
        }

        public void SetProductWeight(string product, string mealtime)
        {

        }
    }
}
