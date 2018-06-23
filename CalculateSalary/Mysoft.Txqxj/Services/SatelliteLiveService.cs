using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySoft.Common;

namespace Mysoft.Tjqxj.Services
{
    //卫星资料实时获取
    public class SatelliteLiveService
    {
        public static List<Dictionary<string, string>> GetImageInfo(string type)
        {
            var url = "http://data.cma.cn/weatherGis/web/bmd/VisDataDef/getVisData?datacode="+ type;
            var result =  HttpHelper.HttpGet(url);
            return JsonHelper.Get<List<Dictionary<string, string>>>(result, "data");
        }
    }
}
