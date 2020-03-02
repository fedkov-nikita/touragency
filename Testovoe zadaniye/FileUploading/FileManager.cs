using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Testovoe_zadaniye.FileUploading
{
    public static class FileUpExtensionMethod
    {
        
        public static async Task<string> PathReturn(this IFormFile file, IWebHostEnvironment appEnvironment )
        {
            var directory = Directory.CreateDirectory(@"C:\Users\VsemPC\Desktop\touragency-master\Testovoe zadaniye\wwwroot\images");
            string path = directory + file.FileName;
            // сохраняем файл в папку Files в каталоге wwwroot
            string fileName = Path.GetFileName(path);
            string fileExtension = Path.GetExtension(fileName);
            string randomFileName = Path.GetRandomFileName();
            string fullPath = "/images/" + Path.GetFileNameWithoutExtension(randomFileName) + fileExtension;

            using (var fileStream = new FileStream(appEnvironment.WebRootPath + fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fullPath;

        }
    }
}
