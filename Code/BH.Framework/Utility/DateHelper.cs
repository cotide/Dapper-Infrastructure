using System;

namespace BH.Framework.Utility
{
    /// <summary>
    /// 日期信息帮助
    /// </summary>
    public class DateHelper
    {
        /// <summary>
        /// 获取星期
        /// </summary>
        /// <returns></returns>
        public static string GetWeekString()
        {
            string weekstr = DateTime.Now.DayOfWeek.ToString();
            switch (weekstr)
            {
                case "Monday": weekstr = "星期一"; break;
                case "Tuesday": weekstr = "星期二"; break;
                case "Wednesday": weekstr = "星期三"; break;
                case "Thursday": weekstr = "星期四"; break;
                case "Friday": weekstr = "星期五"; break;
                case "Saturday": weekstr = "星期六"; break;
                case "Sunday": weekstr = "星期日"; break;
            }

            return weekstr;
        }
    }

}
