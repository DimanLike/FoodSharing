using System.Globalization;

namespace FoodSharing.Tools.Time
{
    public  class TimeConverter
    {
        public static async Task<string> GetTime(DateTime time)
        {
            DateTime today = DateTime.Now;
            DateTime yesterday = today.AddDays(-1); //Вчера
            DateTime week = today.AddDays(-7); //Неделя

            if (time.DayOfYear == today.DayOfYear )
                return time.ToString("HH:mm");
            else if (time.DayOfYear == yesterday.DayOfYear)
                return "Вчера";
            else if (time.DayOfYear < yesterday.DayOfYear && time.DayOfYear > week.DayOfYear)
                return time.ToString("ddd", CultureInfo.CreateSpecificCulture("ru-ru"));         
            else 
                return time.ToString("dd MMMM", CultureInfo.CreateSpecificCulture("ru-ru"));           
        }
    }
}

