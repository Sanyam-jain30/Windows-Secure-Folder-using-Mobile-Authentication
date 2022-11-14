using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static SSR.MainWindow;
using Ionic.Zip;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SSR
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class DragAndDrop : Page
    {
        public string[] droppedFilePaths;

        public DragAndDrop()
        {
            InitializeComponent();

            this.WindowHeight = 450;
            this.WindowWidth = 800;
        }

        public void DropSpace_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                var StackPanel = sender as StackPanel;
                StackPanel.Background = new SolidColorBrush(Color.FromRgb(155, 155, 155));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public void DropSpace_DragLeave(object sender, DragEventArgs e)
        {
            var StackPanel = sender as StackPanel;
            StackPanel.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

        public void DropSpace_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];

                FileInfo fi = new FileInfo(droppedFilePaths[0]);
                string password = StrongPasswordGen.Program.randompasswordgenerator();
                FilePathValue.Text = droppedFilePaths[0];
                LockWithPassword(droppedFilePaths[0], @"D:\Downloads\new.zip", password);

                string accountSid = "AC416a918c795eae91c9a8623d371cd36f";
                string authToken = "642e86c5d18156e3a49fc41ede94e2b9";
                TwilioClient.Init(accountSid, authToken);

                var to = new Twilio.Types.PhoneNumber("+918790394300");
                var from = new Twilio.Types.PhoneNumber("+15139515695");

                var message = MessageResource.Create(
                    to: to,
                    from: from,
                    body: "Password for the file name: " + fi.Name + " password is: " + password
                );
            }
            var StackPanel = sender as StackPanel;
            StackPanel.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

        private void LockWithPassword(string sourceFile, string zipFile, string password)
        {
            if (File.Exists(zipFile))
            {
                using (ZipFile zip = new ZipFile(zipFile))
                {
                    zip.Password = password;
                    zip.AddFile(sourceFile);
                    zip.Save();
                }
            }
            else
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.Password = password;

                    zip.AddFile(sourceFile);
                    zip.Save(zipFile);
                }
            }
            
        }

        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            NavigationService n = NavigationService.GetNavigationService(sender as Button);
            n.Navigate(new Uri("MainWindow.xaml", UriKind.Relative));
        }
    }
}
