using System;
using System.IO;
using Windows.Storage;

namespace MijnLijn.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, @"Assets\staticHaltes.sqlite");


            CopyDatabaseIfNotExists();

            return path;
        }

        private static async void CopyDatabaseIfNotExists()
        {
            StorageFile file;
            try
            {
                file = await ApplicationData.Current.LocalFolder.GetFileAsync("staticHaltes.sqlite");
            }
            catch
            {
                StorageFile Importedfile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/staticHaltes.sqlite"));
                file = await Importedfile.CopyAsync(Windows.Storage.ApplicationData.Current.LocalFolder);
            }
            string path = file.Path;

            //using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path))
            //{
            //    conn.CreateTable<User>();
            //}
        }
    }
}
