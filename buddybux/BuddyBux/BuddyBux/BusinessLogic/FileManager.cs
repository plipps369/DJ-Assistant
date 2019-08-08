using BuddyBux.DAO;
using BuddyBux.Models;
using BuddyBux.Models.Database;
using System;
using System.IO;
using System.Security.Cryptography;

namespace BuddyBux.BusinessLogic
{
    public class FileManager
    {
        private string _storageRootPath;
        private IBuddyBuxDAO _db;

        public FileManager(IBuddyBuxDAO db, string storageRootPath = @"C:\BuddyBux\Storage")
        {
            Directory.CreateDirectory(storageRootPath);
            _storageRootPath = storageRootPath;
            _db = db;
        }

        public FileItem StoreFile(string filePath)
        {
            FileItem result = null;

            string hash = HashFile(filePath);
            FileInfo info = new FileInfo(filePath);
            result = CreateFileItem(info.Name, hash, (int) info.Length);
            var destPath = Path.Combine(result.StoragePath, result.StorageName);
            File.Copy(filePath, destPath);

            return result;
        }

        public FileItem StoreFile(byte[] fileData, string fileName)
        {
            FileItem result = null;

            string hash = HashFile(fileData);
            result = CreateFileItem(fileName, hash, fileData.Length);
            string fullPath = Path.Combine(result.StoragePath, result.StorageName);
            File.WriteAllBytes(fullPath, fileData);

            return result;
        }

        public void DeleteFile(FileItem file)
        {
            string filePath = Path.Combine(file.StoragePath, file.StorageName);
            File.Delete(filePath);
            _db.DeleteFileItem(file.Id);
        }

        public void DeleteFile(int id)
        {
            DeleteFile(_db.GetFileItemById(id));
        }

        public FileModel GetFile(int id)
        {
            FileModel result = new FileModel();

            // Get record from db by id
            FileItem file = _db.GetFileItemById(id);

            // Retrieve file data from local storage
            string fullPath = Path.Combine(file.StoragePath, file.StorageName);
            result.Data = File.ReadAllBytes(fullPath);
            result.Name = file.OriginalName;

            return result;
        }

        private FileItem CreateFileItem(string fileName, string hash, int size)
        {
            FileItem item = new FileItem();
            item.OriginalName = fileName;
            item.StorageName = $"{Guid.NewGuid()}.bin";
            item.StoragePath = _storageRootPath;
            item.Size = size;
            item.Hash = hash;

            item.Id = _db.AddFileItem(item);

            return item;
        }

        public static string HashFile(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                return GenerateMD5Hash(stream);
            }
        }

        public static string HashFile(byte[] fileData)
        {
            using (var stream = new MemoryStream(fileData))
            {
                return GenerateMD5Hash(stream);
            }
        }

        private static string GenerateMD5Hash(Stream data)
        {
            using (var md5 = MD5.Create())
            {
                return Convert.ToBase64String(md5.ComputeHash(data));
            }
        }


    }
}
