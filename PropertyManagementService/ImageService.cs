using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyManagementData;
using PropertyManagementData.Model;

namespace PropertyManagementService
{
    public class ImageService : IDisposable
    {
        private IDbContextFactory<PropertyManagementContext> _dbContextFactory;

        private string _saveDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "property-img");

        public ImageService(IDbContextFactory<PropertyManagementContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public int SaveImage(string sourcePath)
        {
            using var db = _dbContextFactory.CreateDbContext();

            if (String.IsNullOrWhiteSpace(sourcePath))
                throw new ArgumentException(nameof(sourcePath));

            Directory.CreateDirectory(_saveDirectoryPath);

            string extension = Path.GetExtension(sourcePath);
            string fileName = Path.GetRandomFileName() + extension;

            string newPath = Path.Combine(_saveDirectoryPath, fileName);
            File.Copy(sourcePath, newPath);

            var newImage = new Image { FileName = fileName };
            db.Image.Add(newImage);

            db.SaveChanges();

            return newImage.ImageId;
        }

        public string GetImagePath(int imageId)
        {
            using var db = _dbContextFactory.CreateDbContext();
            var image = db.Image.Find(imageId);

            return Path.Combine(_saveDirectoryPath, image.FileName);
        }

        public void DeleteImage(int imageId)
        {
            using var db = _dbContextFactory.CreateDbContext();

            var image = db.Image.Find(imageId);

            string path = Path.Combine(_saveDirectoryPath, image.FileName);

            File.Delete(path);

            db.Image.Remove(image);
            db.SaveChanges();
        }

        public void Dispose()
        {
            //db.Dispose();
        }
    }
}
