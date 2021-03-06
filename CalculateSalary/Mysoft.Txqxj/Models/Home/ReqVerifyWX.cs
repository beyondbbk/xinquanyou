﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mysoft.Txqxj.Models.Home
{
    public class ReqVerifyWx
    {
        /// <summary>
        /// 微信加密签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string Echostr { get; set; }
    }
}
