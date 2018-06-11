using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MySoft.Common
{
    public class JsonConfigHelper
    {
        private static IConfigurationRoot _configRoot;
        private static readonly string JsonPath = Directory.GetCurrentDirectory();

        static JsonConfigHelper()
        {
            LoadJsonConfig();
            var fileSystemWatcher = new FileSystemWatcher(JsonPath)
            {
                EnableRaisingEvents = true
            };

            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
        }

        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            LoadJsonConfig();
        }

        private static void LoadJsonConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(JsonPath).AddJsonFile("appsettings.json"); //SetBasePath 设置配置文件所在路径
            _configRoot = builder.Build();
        }

        public static T Get<T>(params string[] sectionNames)
        {
            IConfigurationSection configurationSection = null;
            foreach (var temp in sectionNames)
            {
                if (configurationSection == null)
                {
                    configurationSection = _configRoot.GetSection(temp);
                }
                else
                {
                    configurationSection = configurationSection.GetSection(temp);
                }
                if (!configurationSection.Exists()) throw new Exception($"[{temp}]配置节点不存在，期望配置为[{string.Join("-",sectionNames)}]，请检查");
            }
            try
            {
                return (T) Convert.ChangeType(configurationSection.Value, typeof(T));
            }
            catch
            {
                throw new Exception($"配置信息转换异常，值为[{configurationSection.Value}]，期望将其转换为类型[{typeof(T)}]");
            }
            
        }
    }
}
