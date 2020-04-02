using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyMealPlaner.Business_Layer;

namespace DailyMealPlaner.Data_Layer
{
    interface IDailyRation
    {
        void ClearRation();

        Product GetRationFirstProduct();

        void SetProductWeight(string product, string mealtime);
    }
}
