using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.IO.IsolatedStorage;

namespace Assassin
{
    public class StorageManager
    {

        public static void CreateDirectory(string path)
        {
            IsolatedStorageFile f = IsolatedStorageFile.GetUserStoreForApplication();

            if (!f.DirectoryExists("data"))
                f.CreateDirectory("data");
        }

        public static void SaveData(string path, string data)
        {
            IsolatedStorageFile f = IsolatedStorageFile.GetUserStoreForApplication();

            using (var fs = new IsolatedStorageFileStream("data\\" + path, FileMode.Create, f))
            {
                using (var isoFileWriter = new StreamWriter(fs))
                {
                    isoFileWriter.Write(data);
                }
            }
        }

        public static string LoadData(string path)
        {
            IsolatedStorageFile f = IsolatedStorageFile.GetUserStoreForApplication();

            if (!f.FileExists("data\\" + path))
                return "";
            else
            {
                string Text;

                using (var fs = new IsolatedStorageFileStream("data\\" + path, FileMode.Open, f))
                {
                    using (var isoFileReader = new StreamReader(fs))
                    {
                        Text = isoFileReader.ReadToEnd();
                    }
                }
                return Text;
            }
        }
    }
}
