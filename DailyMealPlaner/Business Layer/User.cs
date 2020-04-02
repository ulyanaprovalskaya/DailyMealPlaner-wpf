using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlaner.Business_Layer
{
    enum DailyActivity
    {
        Low,
        Normal,
        Average,
        High
    }

    class User
    {
        double weight;
        int height;
        int age;
        DailyActivity dailyActivity;

        public double Weight { get => weight; set => weight = value; }
        public int Height { get => height; set => height = value; }
        public int Age { get => age; set => age = value; }
        public DailyActivity DailyActivity { get => dailyActivity; set => dailyActivity = value; }
        public double ARM
        {
            get
            {
                if (DailyActivity == DailyActivity.Low) return 1.2;
                if (DailyActivity == DailyActivity.Normal) return 1.375;
                if (DailyActivity == DailyActivity.Average) return 1.55;
                if (DailyActivity == DailyActivity.High) return 1.725;
                else return 0;
            }
        }
        public double BMR
        {
            get
            {
                return 447.593 + 9.247 * Weight + 3.098 * Height - 4.330 * Age;
            }
        }
        public double DailyCaloriesRate
        {
            get
            {
                return Math.Round(BMR * ARM);
            }
        }

    }
}
