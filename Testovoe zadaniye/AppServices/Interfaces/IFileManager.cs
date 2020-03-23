using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Testovoe_zadaniye.AppServices.Interfaces
{
    public interface IFileManager
    {
        Task<string> UploadPhoto(IFormFile formFile);
        void DeletePhoto(string delFileFolder);
    }
}
