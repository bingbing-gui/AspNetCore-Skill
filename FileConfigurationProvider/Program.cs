using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileConfigurationProvider
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.Sources.Clear();
                    var env = hostingContext.HostingEnvironment;
                    #region IniConfigurationProvider
                    //config.AddIniFile("MyIniConfig.ini", optional: true, reloadOnChange: true)
                    //      .AddIniFile($"MyIniConfig.{env.EnvironmentName}.ini", optional: true, reloadOnChange: true);
                    #endregion
                    #region JsonConfigurationProvider
                    //config.AddJsonFile("MyConfig.json", optional: true, reloadOnChange: true)
                    // .AddJsonFile($"MyConfig.{env.EnvironmentName}.json",
                    //                optional: true, reloadOnChange: true);
                    #endregion
                    #region XmlConfigurationProvider
                    //config.AddXmlFile("MyXMLFile.xml", optional: true, reloadOnChange: true)
                    //.AddXmlFile($"MyXMLFile.{env.EnvironmentName}.xml",
                    //                optional: true, reloadOnChange: true);
                    //绑定List
                    //config.AddXmlFile("MyXMLFileList.xml", optional: true, reloadOnChange: true)
                    //.AddXmlFile($"MyXMLFile.{env.EnvironmentName}.xml",
                    //                  optional: true, reloadOnChange: true);
                    #endregion
                    #region MemoryConfigurationProvider
                    var Dict = new Dictionary<string, string>
                        {
                           {"MyKey", "Dictionary MyKey Value"},
                           {"Position:Title", "Dictionary_Title"},
                           {"Position:Name", "Dictionary_Name" },
                           {"Logging:LogLevel:Default", "Warning"}
                        };
                    config.AddInMemoryCollection(Dict);
                    #endregion
                    config.AddEnvironmentVariables();
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
