using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using static AspNetCore.ChangeToToken.Utilities.Utilities;

namespace AspNetCore.ChangeToToken.Extensions
{

    public interface IConfigurationMonitor
    {
        bool MonitoringEnabled { get; set; }
        string CurrentState { get; set; }
    }
    public class ConfigurationMonitor : IConfigurationMonitor
    {
        private byte[] _appsettingsHash = new byte[20];

        private byte[] _appsettingsEnvHash = new byte[20];

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ConfigurationMonitor(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            ChangeToken.OnChange<IConfigurationMonitor>(
                () => configuration.GetReloadToken(), InvokeChange, this);
        }

        public bool MonitoringEnabled { get; set; } = false;
        public string CurrentState { get; set; } = "Not monitoring";

        public void InvokeChange(IConfigurationMonitor configurationMonitor)
        {
            if (MonitoringEnabled)
            {
                var appSettingsHash = ComputeHash("appSettings.json");
                var appSettingsEnvHash = ComputeHash($"appSettings.{_webHostEnvironment.EnvironmentName}.json");

                if (!_appsettingsEnvHash.SequenceEqual(appSettingsEnvHash)
                   || !_appsettingsHash.SequenceEqual(appSettingsHash))
                {
                    string message = $"State updated at {DateTime.Now}";
                    _appsettingsEnvHash = appSettingsEnvHash;
                    _appsettingsHash = appSettingsHash;
                    WriteConsole("Configuration changed (ConfigurationMonitor Class) " +
                 $"{message}, state:{configurationMonitor.CurrentState}");
                }
            }

        }
    }
}
