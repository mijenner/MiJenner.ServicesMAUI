using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsManagerMauiDemo
{
    public class AppSettings
    {
        public string ConnString { get; set; }
        public string ApiString { get; set; }
        public bool IsRunningOK { get; set; }
    }
}
