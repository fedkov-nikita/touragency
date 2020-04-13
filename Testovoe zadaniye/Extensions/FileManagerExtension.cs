using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Testovoe_zadaniye.AppServices.Services;

namespace Testovoe_zadaniye.FileUploading
{ 
    public static class FileUpExtensionMethod
    {
        
        public static async Task<string> PathReturn(this IFormFile file, IWebHostEnvironment appEnvironment )
        {
            FileManager fileManager = new FileManager(appEnvironment);

            return await fileManager.UploadPhoto(file);

        }
    }
}
