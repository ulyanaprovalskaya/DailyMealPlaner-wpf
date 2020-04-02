using DailyMealPlaner.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DailyMealPlaner.Data_Layer
{
    class UserDao : IUser
    {
        private static Db db = Db.GetInstance();

        public int GetAge()
        {
            return db.User.Age;
        }

        public double GetCalories()
        {
            return db.User.DailyCaloriesRate;
        }

        public DailyActivity GetDailyActivity()
        {
            return db.User.DailyActivity;
        }

        public int GetHeight()
        {
            return db.User.Height;
        }

        public double GetWeight()
        {
            return db.User.Weight;
        }

        public void SaveInfo()
        {
            db.WriteUserInfo();
        }

        public void SetAge(string age)
        {
            if (Int32.TryParse(age, out int h))
            {
                db.User.Age = h;
            }
            else db.User.Age = 0;
        }

        public void SetHeight(string height)
        {
            if (Int32.TryParse(height,  out int h))
            {
                db.User.Height = h;
            }
            else db.User.Height = 0;
        }

        public void SetWeight(string weight)
        {
            if (Int32.TryParse(weight, out int h))
            {
                db.User.Weight = h;
            }
            else db.User.Weight = 0;
        }

        public void SetDailyActivity(string dailyActivity)
        {
            db.User.DailyActivity = (DailyActivity)Enum.Parse(typeof(DailyActivity), dailyActivity);
        }

    }
}
