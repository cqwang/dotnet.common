using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.FileExt
{
    public class XmlConfigModifier
    {
        public static void AddOrUpdateAppSetting(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                config.AppSettings.Settings.Add(key, value);
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void AddOrUpdateConnectionString(string name, string connectionString, string providerName = null)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.ConnectionStrings.ConnectionStrings[name] != null)
            {
                config.ConnectionStrings.ConnectionStrings[name].ConnectionString = connectionString;
            }
            else
            {
                var setting = string.IsNullOrEmpty(providerName) ? new ConnectionStringSettings(name, connectionString) : new ConnectionStringSettings(name, connectionString, providerName);
                config.ConnectionStrings.ConnectionStrings.Add(setting);
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
