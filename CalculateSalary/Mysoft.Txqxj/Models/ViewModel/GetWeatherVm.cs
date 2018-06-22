using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Mysoft.Tjqxj.Models.ViewModel
{
    public class GetWeatherVm
    {
        //实时信息
        public RealtimeClimateInfo RealtimeClimateInfo = new RealtimeClimateInfo();

        //预报信息
        public ClimateInfo ClimateInfo { get; set; } = new ClimateInfo();

    }


    public class RealtimeClimateInfo
    {
        public string Address { get; set; }

        public string Tempture { get; set; }

        //天气描述
        public string ClimateDescription { get; set; }

        //下雨描述
        public string RainDescription { get; set; }

        //AQI
        public string AQI { get; set; }

        ////最近降水距离
        //public string NearRainDistance { get; set; }

        public string PM { get; set; }
    }

    //天气信息
    public class ClimateInfo
    {
        public List<string> DateInfos { get; set; } = new List<string>()
        {
            DateTime.Now.ToString("yyyy-MM-dd"),
            DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"),
            DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"),
            DateTime.Now.AddDays(3).ToString("yyyy-MM-dd"),
            DateTime.Now.AddDays(4).ToString("yyyy-MM-dd")
        };

        public List<string> DateDisplay { get; set; } = new List<string>()
        {
            "今天",
            "明天",
            "后天",
            CultureInfo.GetCultureInfo("zh-cn").DateTimeFormat.GetDayName(DateTime.Now.AddDays(2).DayOfWeek),
            CultureInfo.GetCultureInfo("zh-cn").DateTimeFormat.GetDayName(DateTime.Now.AddDays(3).DayOfWeek),
            CultureInfo.GetCultureInfo("zh-cn").DateTimeFormat.GetDayName(DateTime.Now.AddDays(4).DayOfWeek),
        };

        /// <summary>
        /// 温度信息
        /// "date": "2018-06-22",
        ///	"max": 32.0,
        ///	"avg": 29.37,
        ///	"min": 26.0
        /// </summary>
        public List<Dictionary<string, string>> Temperatures = new List<Dictionary<string, string>>();

        /// <summary>
        /// 天气信息
        /// "date": "2018-06-22",
        ///	"value": "RAIN"
        /// </summary>
        public List<Dictionary<string,string>> Skycons = new List<Dictionary<string, string>>();

        /// <summary>
        /// AQI指数
        /// "date": "2018-06-22",
        ///	"max": 8,
        ///	"avg": 5.65,
        ///	"min": 4
        /// </summary>
        public List<Dictionary<string, string>> AQIs = new List<Dictionary<string, string>>();

        /// <summary>
        /// PM2.5指数
        /// "date": "2018-06-22",
        ///	"max": 5,
        ///	"avg": 3.88,
        ///	"min": 3
        /// </summary>
        public List<Dictionary<string, string>> PMs = new List<Dictionary<string, string>>();

        //public string ApiName { get; set; }

        public static string GetClimateDisplayName(string apiClimateName)
        {
                switch (apiClimateName)
                {
                    case "CLEAR_DAY":
                        return "晴天";
                    case "CLEAR_NIGHT":
                        return "晴夜";
                    case "PARTLY_CLOUDY_DAY":
                    case "PARTLY_CLOUDY_NIGHT":
                        return "多云";
                    case "CLOUDY":
                        return "阴";
                    case "RAIN":
                        return "雨";
                    case "SNOW":
                        return "雪";
                    case "WIND":
                        return "风";
                    case "FOG":
                        return "雾";
                    default:
                        return "未知";
                }
            
        }

        public static string GetClimateIconName(string apiCliamteName)
        {
            
                switch (apiCliamteName)
                {
                    case "CLEAR_DAY":
                        return "clearday";
                    case "CLEAR_NIGHT":
                        return "clearnight";
                    case "PARTLY_CLOUDY_DAY":
                    case "PARTLY_CLOUDY_NIGHT":
                        return "partlyclound";
                    case "CLOUDY":
                        return "clound";
                    case "RAIN":
                        return "rain";
                    case "SNOW":
                        return "snow";
                    case "WIND":
                        return "wind";
                    case "FOG":
                        return "fog";
                    default:
                        return "unknown";
                }

            
        }

      
    }
}
