using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Testovoe_zadaniye.FileUploading
{
    public class FileManager
    {
        IWebHostEnvironment _appEnvironment;
        IFormFile _formFile;
        public FileManager(IFormFile formFile, IWebHostEnvironment appEnvironment )
        {
            _appEnvironment = appEnvironment;
            _formFile = formFile;
        }
        public async Task<string> UploadPhoto()
        {
            var directory = Directory.CreateDirectory(@"E:\TestovoeZadanie\Testovoe zadaniye\Testovoe zadaniye\wwwroot\images");
            string path = directory + _formFile.FileName;
            // сохраняем файл в папку Files в каталоге wwwroot
            string fileName = Path.GetFileName(path);
            string fileExtension = Path.GetExtension(fileName);
            string randomFileName = Path.GetRandomFileName();
            string fullPath = "/images/" + Path.GetFileNameWithoutExtension(randomFileName) + fileExtension;

            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + fullPath, FileMode.Create))
            {
                await _formFile.CopyToAsync(fileStream);
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
            FileManager fileManager = new FileManager(file, appEnvironment);

            return await fileManager.UploadPhoto();

        }
    }
}
