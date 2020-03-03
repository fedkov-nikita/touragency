using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Testovoe_zadaniye.FileUploading
{
    public interface IFileManager
    {
        Task<string> UploadPhoto(IFormFile formFile);
        void DeletePhoto(string delFileFolder);


    }
    public class FileManager: IFileManager
    {
        IWebHostEnvironment _appEnvironment;
        public FileManager(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        public async Task<string> UploadPhoto(IFormFile formFile)
        {
            var directory = Directory.CreateDirectory( @"C:\Users\VsemPC\Desktop\touragency-master\Testovoe zadaniye\wwwroot\images");
            string path = directory + formFile.FileName;
            // сохраняем файл в папку Files в каталоге wwwroot
            string fileName = Path.GetFileName(path);
            string fileExtension = Path.GetExtension(fileName);
            string randomFileName = Path.GetRandomFileName();
            string fullPath = "/images/" + Path.GetFileNameWithoutExtension(randomFileName) + fileExtension;

            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + fullPath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }
            return fullPath;
        }
         //Удаление имеющегося фото 
        public void DeletePhoto(string delFileFolder)
        {
                File.Delete(_appEnvironment.WebRootPath + delFileFolder);
        }

    }
    public static class FileUpExtensionMethod
    {
        
        public static async Task<string> PathReturn(this IFormFile file, IWebHostEnvironment appEnvironment )
        {
            FileManager fileManager = new FileManager(appEnvironment);

            return await fileManager.UploadPhoto(file);

        }
    }
}
