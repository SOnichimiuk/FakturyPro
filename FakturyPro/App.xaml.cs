using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using FakturyPro.Klasy;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FakturyPro.Data.Models;
using FakturyPro.Services;
using FakturyPro.Data.Dto;

namespace FakturyPro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
          
        }

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            
        }

    }
}
