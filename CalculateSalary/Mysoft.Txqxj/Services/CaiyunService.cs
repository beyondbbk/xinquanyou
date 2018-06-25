using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mysoft.Tjqxj.Models.ViewModel;
using MySoft.Common;

namespace Mysoft.Tjqxj.Services
{
    public class CaiyunService
    {
        private static string key = "DIA9u8bPzHsZzXta";


        /// <summary>
        /// 获取实时天气信息
        /// bool-接口调用成功标识
        /// string-天气
        /// string-pm2.5
        /// string 最近降水带距离
        /// string-aqi
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public static Tuple<bool, string,string,string,string,string,string> GetRealtimeInfo(string longitude, string latitude)
        {
            var url = $"http://api.caiyunapp.com/v2/{key}/{longitude},{latitude}/realtime.json";
            var getData = "unit=metric:v2";
            var result = HttpHelper.HttpGet($"{url}?{getData}");
            var tempDic = JsonHelper.DeserializeStringToDictionary<string, object>(result);
            if (tempDic["status"].ToString() != "ok")
            {
                LogHelper.Error("获取实时天气错误："+result);
                return null;
            }
            var resultDic = JsonHelper.DeserializeStringToDictionary<string, object>(tempDic["result"].ToString());
            var skycon = resultDic["skycon"].ToString();//天气类型
            var pm = resultDic["pm25"].ToString();
            var aqi = resultDic["aqi"].ToString();
            var tempture = resultDic["temperature"].ToString();
            var precipitation =
                JsonHelper.DeserializeStringToDictionary<string, object>(resultDic["precipitation"].ToString());
            var nearestDistance = "";
            if (precipitation.ContainsKey("nearest"))
            {
                var nearest = JsonHelper.DeserializeStringToDictionary<string, string>(precipitation["nearest"].ToString());
                nearestDistance = nearest["distance"].ToString();
            }
            //var local = JsonHelper.DeserializeStringToDictionary<string, string>(precipitation["local"].ToString());
            //var localIntensity = local["intensity"];
            var localPrecipitation = JsonHelper.GetString(result, "result", "precipitation", "local", "intensity");
            return new Tuple<bool, string, string, string, string,string,string>(true,skycon,pm,nearestDistance, aqi, tempture,localPrecipitation);
        }

        public static Tuple<bool, ClimateInfo> GetPrediction(RealtimeClimateInfo realtimeClimateInfo,string longitude, string latitude)
        {
            var url = $"http://api.caiyunapp.com/v2/{key}/{longitude},{latitude}/forecast.json";
            var getData = "unit=metric:v2";
            var result = HttpHelper.HttpGet($"{url}?{getData}");
            
            if (JsonHelper.GetString(result, "status") != "ok")
            {
                LogHelper.Error("获取天气预报信息错误：" + result);
                return null;
            }

            realtimeClimateInfo.ClimateDescription = JsonHelper.GetString(result, "result", "hourly", "description");
            realtimeClimateInfo.RainDescription = JsonHelper.GetString(result, "result", "minutely", "description");
            var climateInfo = new ClimateInfo();
            climateInfo.Temperatures = JsonHelper.Get<List<Dictionary<string, string>>>(result, "result", "daily", "temperature");
            climateInfo.AQIs = JsonHelper.Get<List<Dictionary<string, string>>>(result, "result", "daily", "aqi");
            climateInfo.PMs= JsonHelper.Get<List<Dictionary<string, string>>>(result, "result", "daily", "pm25");
            climateInfo.Skycons = JsonHelper.Get<List<Dictionary<string, string>>>(result, "result", "daily", "skycon");
            climateInfo.Precipitations = JsonHelper.Get<List<Dictionary<string,string>>>(result, "result", "daily", "precipitation");

            return new Tuple<bool, ClimateInfo>(true,climateInfo);
        }
    }
}
