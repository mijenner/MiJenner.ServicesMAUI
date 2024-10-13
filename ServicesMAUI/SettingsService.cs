using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiJenner.ServicesMAUI
{
    public class SettingsService : ISettingsService
    {
        public SettingsService() { }

        public Task<T> Get<T>(string key, T defaultValue)
        {
            var result = Preferences.Default.Get<T>(key, defaultValue);
            return Task.FromResult(result);
        }

        public Task Save<T>(string key, T value)
        {
            Preferences.Default.Set(key, value);
            return Task.CompletedTask;
        }
    }
}
