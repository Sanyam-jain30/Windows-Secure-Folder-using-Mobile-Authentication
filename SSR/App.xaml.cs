using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;

namespace SSR
{
    public partial class App : Application
    {
        void AppStartup(object sender, StartupEventArgs e)
        {
            // Create the startup window
            MainWindow wnd = new MainWindow();
            // Do stuff here, e.g. to the window
            wnd.Title = "SSR";
            // Show the window
            wnd.Show();
        }
    }
}
