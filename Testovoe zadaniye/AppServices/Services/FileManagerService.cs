using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.AppServices.Interfaces;

namespace Testovoe_zadaniye.AppServices.Services
{
    public class FileManager : IFileManager
    {
        IWebHostEnvironment _appEnvironment;
        public FileManager(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        public async Task<string> UploadPhoto(IFormFile formFile)
        {
            var directory = Directory.CreateDirectory(@"E:\TestovoeZadanie\touragency\Testovoe zadaniye\wwwroot\images");
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
}
