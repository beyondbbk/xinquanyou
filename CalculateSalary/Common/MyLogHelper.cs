using System;
using System.IO;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace MySoft.Common
{
    public class MyLogHelper
    {
        private static readonly ILog log;

        static MyLogHelper()
        {
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            BasicConfigurator.Configure(repository);
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            log = LogManager.GetLogger(repository.Name, "NETCorelog4net");
        }

        public static void Debug(object msg)
        {
            log.Debug(msg);
        }

        public static void Info(object msg)
        {
            log.Info(msg);
        }

        public static void Error(object msg)
        {
            log.Error(msg);
        }

        public static void Fatal(object msg)
        {
            log.Fatal(msg);
        }
    }
}
