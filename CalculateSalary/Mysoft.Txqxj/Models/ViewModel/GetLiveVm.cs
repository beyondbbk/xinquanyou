using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mysoft.Tjqxj.Models.ViewModel
{
    public class GetLiveVm
    {
        public GetLiveVm(string first="", string second="", string third="")
        {
            FirstMemu = (string.IsNullOrEmpty(first)) ? AllChooseInfos.First().FirstMemu : first;
            SecondMemu = (string.IsNullOrEmpty(second)) ? AllChooseInfos.First(u => u.FirstMemu==FirstMemu).SecondMemu : second;
            ThirdMemu = (string.IsNullOrEmpty(third)) ? AllChooseInfos.First(u=>u.FirstMemu==FirstMemu&&u.SecondMemu==SecondMemu).ThirdMemu : third;
        }

        public List<ChooseInfo> AllChooseInfos = new List<ChooseInfo>()
        {
            new ChooseInfo(){FirstMemu = "地面资料",SecondMemu = "气温",ThirdMemu = "小时最高气温",ApiName = "SURF_TH1"},//SURF_T1
            new ChooseInfo(){FirstMemu = "地面资料",SecondMemu = "气温",ThirdMemu = "小时气温",ApiName = "SURF_T1"},//SURF_TL1
            new ChooseInfo(){FirstMemu = "地面资料",SecondMemu = "气温",ThirdMemu = "小时最低气温",ApiName = "SURF_TL1"},
            //new ChooseInfo(){FirstMemu = "地面资料",SecondMemu = "气温",ThirdMemu = "日最高气温",ApiName = "IMG_SURF_TEM_DAY_MAX"},
            //new ChooseInfo(){FirstMemu = "地面资料",SecondMemu = "气温",ThirdMemu = "日平均气温",ApiName = "IMG_SURF_TEM_DAY_AVG"},
            //new ChooseInfo(){FirstMemu = "地面资料",SecondMemu = "气温",ThirdMemu = "日最低气温",ApiName = "IMG_SURF_TEM_DAY_MIN"},
            new ChooseInfo(){FirstMemu = "地面资料",SecondMemu = "降水",ThirdMemu = "小时降水",ApiName = "SURF_R1"},
            new ChooseInfo(){FirstMemu = "地面资料",SecondMemu = "降水",ThirdMemu = "相对湿度",ApiName = "SURF_RHumidity1"},

            new ChooseInfo(){FirstMemu = "雷达云图",SecondMemu = "风四卫星",ThirdMemu = "卫星云图",ApiName = "P_FY4A-_AGRI_SMALL"},
            new ChooseInfo(){FirstMemu = "雷达云图",SecondMemu = "雷达",ThirdMemu = "雷达拼图",ApiName = "RAD__B0_CR"},

            new ChooseInfo(){FirstMemu = "陆面数据",SecondMemu = "土壤数据",ThirdMemu = "0-5CM湿度",ApiName = "NAFP_CLDAS2.0_RT_JPG_SM000005"},
            new ChooseInfo(){FirstMemu = "陆面数据",SecondMemu = "土壤数据",ThirdMemu = "0-10CM湿度",ApiName = "NAFP_CLDAS2.0_RT_JPG_SM000010"},
            new ChooseInfo(){FirstMemu = "陆面数据",SecondMemu = "土壤数据",ThirdMemu = "10-40CM湿度",ApiName = "NAFP_CLDAS2.0_RT_JPG_SM010040"},
            new ChooseInfo(){FirstMemu = "陆面数据",SecondMemu = "土壤数据",ThirdMemu = "40-100CM湿度",ApiName = "NAFP_CLDAS2.0_RT_JPG_SM040100"},
            new ChooseInfo(){FirstMemu = "陆面数据",SecondMemu = "土壤数据",ThirdMemu = "100-200CM湿度",ApiName = "NAFP_CLDAS2.0_RT_JPG_SM100200"},

            new ChooseInfo(){FirstMemu = "陆面数据",SecondMemu = "驱动场数据",ThirdMemu = "2M气温",ApiName = "NAFP_CLDAS2.0_RT_JPG_TMP"},
            new ChooseInfo(){FirstMemu = "陆面数据",SecondMemu = "驱动场数据",ThirdMemu = "2M湿度",ApiName = "NAFP_CLDAS2.0_RT_JPG_SHU"},
            new ChooseInfo(){FirstMemu = "陆面数据",SecondMemu = "驱动场数据",ThirdMemu = "10M风速",ApiName = "NAFP_CLDAS2.0_RT_JPG_WIN"},
            new ChooseInfo(){FirstMemu = "陆面数据",SecondMemu = "驱动场数据",ThirdMemu = "气压",ApiName = "NAFP_CLDAS2.0_RT_JPG_PRS"},
            new ChooseInfo(){FirstMemu = "陆面数据",SecondMemu = "驱动场数据",ThirdMemu = "小时降水",ApiName = "NAFP_CLDAS2.0_RT_JPG_PRE"},
            new ChooseInfo(){FirstMemu = "陆面数据",SecondMemu = "驱动场数据",ThirdMemu = "短波辐射",ApiName = "NAFP_CLDAS2.0_RT_JPG_SSRA"},


        };

        public string FirstMemu { get; set; }

        public string SecondMemu { get; set; }
        public string ThirdMemu { get; set; }

        public List<string> GetFirstMemus()
        {
            return AllChooseInfos.Select(u => u.FirstMemu).Distinct().ToList();
        }

        public List<string> GetSecondMemus()
        {
            return AllChooseInfos.Where(u => u.FirstMemu == FirstMemu).Select(u => u.SecondMemu).Distinct().ToList();
        }

        public List<string> GetThirdMemus()
        {
            return AllChooseInfos.Where(u => u.FirstMemu == FirstMemu && u.SecondMemu == SecondMemu)
                .Select(u => u.ThirdMemu).Distinct().ToList();
        }

        public string ApiName
        {
            get
            {
                return AllChooseInfos.Single(u => u.FirstMemu == FirstMemu && u.SecondMemu == SecondMemu &&
                                                 u.ThirdMemu == ThirdMemu).ApiName;
            }
        }

        //图像信息，格式
        //"id": "1719325",
        //"fcstVT": "",
        //"c_IYMDHMS": "20180623000500",
        //"v_SHIJIAN": "20180623000500",
        //"v_TTIME": 0,
        //"c_PRODUCT_TYPE": "JPG",
        //"c_FNAME": "Z_SATE_C_BAWX_20180623000553_P_FY4A-_AGRI--_N_REGI_1047E_L1C_TCC-_MULT_GLL_20180622234500_20180622235959_1000M_V0001_small.JPG",
        //"c_FDIR": "",
        //"v_FSIZE": "463886",
        //"dataCode": "P_FY4A-_AGRI_SMALL",
        //"statusFlag": "2",
        //"fileURL": "http://image.data.cma.cn/vis/P_FY4A-_AGRI_SMALL/20180623/Z_SATE_C_BAWX_20180623000553_P_FY4A-_AGRI--_N_REGI_1047E_L1C_TCC-_MULT_GLL_20180622234500_20180622235959_1000M_V0001_small.JPG"

        public List<Dictionary<string,string>> ImageList=new List<Dictionary<string, string>>();

        public string GetDateInfo(string dateStr)
        {
            return dateStr.Substring(4,2)+"-"+dateStr.Substring(6, 2);
        }
        public string GetTimeInfo(string dateStr)
        {
            return dateStr.Substring(8, 2) + ":" + dateStr.Substring(10, 2);
        }
    }

    public class ChooseInfo
    {
        public string FirstMemu { get; set; }

        public string SecondMemu { get; set; }

        public string ThirdMemu { get; set; }

        public string ApiName { get; set; }
    }

    
}
