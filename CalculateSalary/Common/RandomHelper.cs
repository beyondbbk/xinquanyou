using System;
using System.Threading;

namespace MySoft.Common
{
    /// <summary>
    /// 随机数生成器
    /// </summary>
    public class RandomHelper
    {
        private static readonly Random Random = new Random();
        public static string Create()
        {
            lock (Random)
            {
                Thread.Sleep(1);
                return DateTime.Now.ToString("yyyyMMddhhmmssfff") + Random.Next(10000, 99999);
            }
        }
    }
}
