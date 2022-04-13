using PropertyManagementData;
using PropertyManagementData.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagementService
{
    public class ImageService : IDisposable
    {
        private PropertyManagementContext _db;

        private string _saveDirectoryPath;

        public ImageService(PropertyManagementContext db, string saveDirectoryPath)
        {
            _db = db;
            _saveDirectoryPath = saveDirectoryPath;
        }

        public int SaveImage(string sourcePath)
        {
            if (String.IsNullOrWhiteSpace(sourcePath))
                throw new ArgumentException(nameof(sourcePath));

            Directory.CreateDirectory(_saveDirectoryPath);

            string extension = Path.GetExtension(sourcePath);
            string fileName = Path.GetRandomFileName() + extension;

            string newPath = Path.Combine(_saveDirectoryPath, fileName);
            File.Copy(sourcePath, newPath);

            var newImage = new Image { FileName = fileName };
            _db.Image.Add(newImage);

            _db.SaveChanges();

            return newImage.ImageId;
        }

        public string GetImagePath(int imageId)
        {
            var image = _db.Image.Find(imageId);

           return Path.Combine(_saveDirectoryPath, image.FileName);
        }

        public void DeleteImage(int imageId)
        {
            var image = _db.Image.Find(imageId);

            string path = Path.Combine(_saveDirectoryPath, image.FileName);

            File.Delete(path);

            _db.Image.Remove(image);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
