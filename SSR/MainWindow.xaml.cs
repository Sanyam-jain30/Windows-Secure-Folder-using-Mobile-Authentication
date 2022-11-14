using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
using static System.Net.WebRequestMethods;

namespace SSR
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        public int index = 0;
        private string directory = @"D:\Downloads\new.zip";

        public struct FileDetails
        {
            public int SNo { set; get; }
            public string FileName { set; get; }
            public string LastModified { set; get; }
            public string LockUnlock { set; get; }
            public long Size { set; get; }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.WindowHeight = 450;
            this.WindowWidth = 800;

            using (var zip = ZipFile.Read(directory))
            {
                int totalEntries = zip.Entries.Count;
                foreach (ZipEntry e in zip.Entries)
                {
                    DateTime modifiedDate = e.LastModified;
                    FileGrid.Items.Add(new FileDetails { SNo = ++index, FileName = e.FileName, LastModified = modifiedDate.ToString("dd/MM/yyyy"), LockUnlock = "Assest/Lock.png", Size = e.CompressedSize });
                   
                }
            }

        }

        private void DragDrop_Click(object sender, RoutedEventArgs e)
        {
            NavigationService n = NavigationService.GetNavigationService(sender as Button);

            n.Navigate(new Uri("DragAndDrop.xaml", UriKind.Relative));
        }

        /*public void UpdateTable()
        {
            DragAndDrop dragAndDrop = new DragAndDrop();

            FileInfo fi = new FileInfo(dragAndDrop.droppedFilePaths[0]);

            var size = fi.Length;
            DateTime modifiedDate = File.GetLastWriteTime(dragAndDrop.droppedFilePaths[0]);
            FileGrid.Items.Add(new FileDetails { SNo = ++index, FileName = fi.Name, LastModified = modifiedDate.ToString("dd/MM/yyyy"), LockUnlock = "Assest/Lock.png", Size = size });
     
            FileGrid.Items.Add(new FileDetails { SNo = ++index, FileName = "Hello", LastModified = DateTime.Today.ToString("dd/MM/yyyy"), LockUnlock = "Assest/Lock.png", Size = 2000 });
        }*/
    }
}
