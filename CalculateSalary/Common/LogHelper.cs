using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace MySoft.Common
{
    public class LogHelper
    {
        
        private static readonly ILog Logger;
        private static readonly Dictionary<string, DateTime> SendEmailRecordDic = new Dictionary<string, DateTime>();
        private static readonly object Lockob = new object();

        static LogHelper()
        {
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            BasicConfigurator.Configure(repository);
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            Logger = LogManager.GetLogger(repository.Name, "NETCorelog4net");
        }

        public static void Debug(object msg)
        {
            Logger.Debug(msg);
   
        }

        public static void Info(object msg)
        {
            Logger.Info(msg);

        }

        public static void Error(object msg)
        {
            Logger.Error(msg);

        }

        public static void Fatal(object msg)
        {
            Logger.Fatal(msg);

        }

        /// <summary>
        /// 发送邮件-增加判断如果相同异常对一个收件人24小时内只发一次
        /// </summary>
        /// <param name="email"></param>
        public static void SendFatalMail(string email, string msg)
        {
            lock (Lockob)
            {
                var dicKey = email + msg;
                //Info($"字典Key:{dicKey},当前字典count:{SendEmailRecordDic.Count},是否包含此Key:{SendEmailRecordDic.ContainsKey(dicKey)},字典序列化:{JsonHelper.Serialize(SendEmailRecordDic)}");
                if (SendEmailRecordDic.ContainsKey(dicKey))
                {
                    if (SendEmailRecordDic[dicKey].AddHours(24) > DateTime.Now) return;//24小时内发过的就不发送
                    SendEmailRecordDic.Remove(dicKey);
                }
#if DEBUG
                SendMail(email, "yangjian@ebopark.com", "3565759bbK", "电子发票异常-测试环境", msg);
#endif
                SendEmailRecordDic.Add(dicKey, DateTime.Now);
            }
        }


        /// <summary>
        /// 向用户发送邮件
        /// </summary>
        /// <param name="receiveUser">接收邮件的用户</param>
        /// <param name="sendUser">发送者显求的邮箱地址,可为空</param>
        /// <param name="userPassword">发送者的登陆密码</param>
        /// <param name="mailTitle">发送标题</param>
        /// <param name="mailContent">发送的内容</param>
        private static void SendMail(string receiveUser, string sendUser, string userPassword, string mailTitle, string mailContent)
        {
            try
            {
                var smtpClient = new SmtpClient
                {
                    EnableSsl = true, //开启SSL
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = "smtp.exmail.qq.com",
                    Credentials = new NetworkCredential(sendUser, userPassword)
                };

                var mailMessage = new MailMessage(sendUser, receiveUser)
                {
                    Subject = mailTitle,
                    Body = mailContent,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                    Priority = MailPriority.Low
                };

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                LogHelper.Error("邮件发送失败:" + ex.ToString());
            }
        }
    }
}
