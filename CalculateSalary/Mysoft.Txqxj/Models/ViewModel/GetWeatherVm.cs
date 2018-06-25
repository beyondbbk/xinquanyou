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

        //本地降水量
        public string LocalPrecipitation { get; set; }

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
        public List<Dictionary<string, string>> Skycons = new List<Dictionary<string, string>>();

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

        /// <summary>
        /// 本地降雨量
        /// </summary>
        public List<Dictionary<string, string>> Precipitations = new List<Dictionary<string, string>>();

        /// <summary>
        /// 根据APIName和降水量 判断天气
        /// </summary>
        /// <param name="apiClimateName"></param>
        /// <param name="precipitation"></param>
        /// <returns></returns>
        public static string GetClimateDisplayName(string apiClimateName, string precipitation)
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
                    var rainRank = GetRainRank(precipitation);
                    if (rainRank == 1) return "小雨";
                    if (rainRank == 2) return "中雨";
                    return "大雨";
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

        public static string GetClimateIconName(string apiCliamteName, string precipitation)
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
                    var rainRank = GetRainRank(precipitation);
                    if (rainRank == 1) return "littlerain";
                    if (rainRank == 2) return "middlerain";
                    return "bigrain";
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

        /// <summary>
        /// 1-小雨 2-中雨 3-大雨
        /// </summary>
        /// <param name="precipitation"></param>
        /// <returns></returns>
        private static int GetRainRank(string precipitation)
        {
            var temp = Convert.ToDouble(precipitation);
            if (temp < 0.9) return 1;
            if (temp > 2.87) return 3;
            return 2;
        }

        public static string GetAirRank(string aqi)
        {
            var temp = Convert.ToInt32(aqi);
            if (temp <= 50) return $"<span style='color:#62E446'>优</span>";
            if (50 < temp && temp < 100) return $"<span style='color:#62E446'>良</span>";
            if (100 < temp && temp < 200) return "<span style='color:#F18A88'>轻度污染</span>";//#F18A88
            if (200 < temp && temp < 300) return "<span style='color:#DC615F'>中度污染</span>";//#DC615F
            return "<span style='color:#B32724'>严重污染</span>";//#B32724
        }

    }
}
