using BuddyBux.BusinessLogic;
using BuddyBux.DAO;
using BuddyBux.Models;
using System;
using System.IO;

namespace BuddyBuxCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            TestFileManager();
        }

        private static void TestFileManager()
        {
            IBuddyBuxDAO db = new BuddyBuxDAO(@"Data Source=.\SQLEXPRESS;Initial Catalog=BuddyBux;Integrated Security=true");
            FileManager mgr = new FileManager(db);
            var fileItem = mgr.StoreFile(@"C:\BuddyBux\Source\baseball.jpg");
            var fileModel = mgr.GetFile(fileItem.Id);
            File.WriteAllBytes($@"C:\BuddyBux\Destination\{fileModel.Name}", fileModel.Data);

            var fileData = File.ReadAllBytes(@"C:\BuddyBux\Source\baseball.jpg");
            fileItem = mgr.StoreFile(fileData, "baseball.jpg");
            fileModel = mgr.GetFile(fileItem.Id);
            File.WriteAllBytes($@"C:\BuddyBux\Destination\baseball2.jpg", fileModel.Data);

            //FileModel testFile = mgr.GetFile(1);
            //Console.WriteLine("Test file: " + testFile.Name + " file content " + System.Text.Encoding.Default.GetString(testFile.Data));
            //testFile = mgr.GetFile(6);
            //Console.WriteLine("Test file: " + testFile.Name + " file content " + System.Text.Encoding.Default.GetString(testFile.Data));
            //testFile = mgr.GetFile(3);
            //Console.WriteLine("Test file: " + testFile.Name + " file content " + System.Text.Encoding.Default.GetString(testFile.Data));
            //testFile = mgr.GetFile(6);
            //Console.WriteLine("Test file: " + testFile.Name + " file content " + System.Text.Encoding.Default.GetString(testFile.Data));
            //Console.ReadKey();
        }
    }
}
