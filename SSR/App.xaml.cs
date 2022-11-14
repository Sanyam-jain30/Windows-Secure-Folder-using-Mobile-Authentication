using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SSR
{
    public partial class App : Application
    {
        void AppStartup(object sender, StartupEventArgs e)
        {
            NavigationService n = NavigationService.GetNavigationService(sender as Button);

            n.Navigate(new Uri("MainWindow.xaml", UriKind.Relative));
        }
    }
}
