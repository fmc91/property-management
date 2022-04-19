using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PropertyManagementCommon;
using PropertyManagementData;
using PropertyManagementData.Model;

namespace PropertyManagementService
{
    public class ImageService
    {
        private readonly IDbContextFactory<PropertyManagementContext> _dbContextFactory;

        private readonly string _savedImageDirectory;

        public ImageService(
            IDbContextFactory<PropertyManagementContext> dbContextFactory, IOptions<StorageOptions> storageOptions)
        {
            _dbContextFactory = dbContextFactory;
            _savedImageDirectory = storageOptions.Value.SavedImageDirectory;
        }

        public int SaveImage(string sourcePath)
        {
            using var db = _dbContextFactory.CreateDbContext();

            if (String.IsNullOrWhiteSpace(sourcePath))
                throw new ArgumentException(nameof(sourcePath));

            Directory.CreateDirectory(_savedImageDirectory);

            string extension = Path.GetExtension(sourcePath);
            string fileName = Path.GetRandomFileName() + extension;

            string newPath = Path.Combine(_savedImageDirectory, fileName);
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

            return Path.Combine(_savedImageDirectory, image.FileName);
        }

        public void DeleteImage(int imageId)
        {
            using var db = _dbContextFactory.CreateDbContext();

            var image = db.Image.Find(imageId);

            string path = Path.Combine(_savedImageDirectory, image.FileName);

            File.Delete(path);

            db.Image.Remove(image);
            db.SaveChanges();
        }
    }
}
