using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyMealPlaner.Business_Layer;

namespace DailyMealPlaner.Data_Layer
{
    interface IUser
    {
        int GetAge();

        int GetHeight();

        double GetWeight();

        DailyActivity GetDailyActivity();

        double GetCalories();

        void SaveInfo();

        void SetAge(string age);

        void SetHeight(string height);

        void SetWeight(string weight);

        void SetDailyActivity(string dailyActivity);
    }
}
