using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySoft.Weeding.Models
{
    public class FriendInfo
    {
    
        public string urlName { get; set; }

        public List<string> NameInfos=new List<string>();

        public string PhoneNo { get; set; }

        public string JsArrNameInfos
        {
            get
            {
                var temp = string.Join("','",NameInfos);
                return $"['{temp}']";

            }
        }

        public string NormalStr => string.Join("", NameInfos);

        public double TypeSpeed//毫秒为单位
        {
            get
            {
                Double totalTypedCount = 0;
              
                for (int i = 0; i < NameInfos.Count; i++)
                {
                    var temp = NameInfos[i];
                    if (i == 0)
                    {
                        totalTypedCount += temp.Length;
                        continue;
                    }
                    var sameIndex = GetIndex(NameInfos[i - 1], temp);
                    if (sameIndex != 0) //证明部分匹配
                    {
                        totalTypedCount += (temp.Length + NameInfos[i - 1].Length) -(sameIndex*2); //删除和打字 两次操作
                    }
                    else//没有删除动作
                    {
                        totalTypedCount += temp.Length + NameInfos[i-1].Length;//删除和重新打字
                    }
                }
                var speed = Math.Ceiling( 2 / totalTypedCount*1000);//控制在6秒打完
                return speed;
            }
        }

        private int GetIndex(string lastTemp,string temp)
        {
            var tempIndex = 0;
            for (int i = 0; i < lastTemp.Length; i++)
            {
                if(temp.Length<i+1) break;
                if (lastTemp[i] == temp[i])
                {
                    tempIndex++;
                }
                else
                {
                    break;
                }
            }
            return tempIndex;
        }
    }
}
