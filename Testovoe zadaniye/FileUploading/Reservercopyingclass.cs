using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.Extensions.Logging;
using Testovoe_zadaniye.LoggingMechanism;
using Microsoft.AspNetCore.Hosting;
using Ionic.Zip;


namespace Testovoe_zadaniye.FileUploading
{

    class Director
    {
        public ReserveSave Construct(Builder builder)
        {
            builder.CreateReserveSave();
            builder.BuildPartA();
            builder.BuildPartA();
            builder.BuildPartA();
            return builder.ReserveSave;
        }
    }

    abstract class Builder
    {
        public ReserveSave ReserveSave { get; private set; }
        public void CreateReserveSave()
        {
            ReserveSave = new ReserveSave();
        }
        public abstract void BuildPartA();
        public abstract void BuildPartB();
        public abstract void BuildPartC();
        public abstract ReserveSave ReserveMethod();
    }

    class ReserveSave
    {
        List<object> parts = new List<object>();
        public void ReserveMethod(string passToUpload, string passToZip, string name)
        {
            using (ZipFile zip = new ZipFile()) // Создаем объект для работы с архивом
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; // MAX степень сжатия
                zip.AddDirectory(passToUpload); // Кладем в архив папку вместе с содежимым
                zip.Save(passToZip+name);  // Создаем архив  
            }
        }
    }

    class ImageReserveBuilder : Builder
    {
        ReserveSave process = new ReserveSave();

        public override void BuildPartA()
        {
            string folderToSave = $@"C:\Users\VsemPC\Desktop\";
        }
        public override void BuildPartB()
        {
            string folderToUpload = @"C:\Users\VsemPC\Desktop\touragency-master\Testovoe zadaniye\wwwroot\images";
        }
        public override void BuildPartC()
        {
            DateTime now = DateTime.Now;
            string time = now.ToString("date dd.MM.yyyy~hh-mm-ss");
            string fileName = System.IO.Path.GetRandomFileName();
            string withoutExtensionFileName = Path.GetFileNameWithoutExtension(fileName) + time;
        }

        public override ReserveSave ReserveMethod()
        {
            return process;
        }
    }
}
    
//    public class ReserveCopyingClass
//    {
//        IWebHostEnvironment _appEnvironment;
//        TouragencyContext db;

//        public ReserveCopyingClass(IWebHostEnvironment appEnvironment)
//        {
//            appEnvironment = _appEnvironment;

//        }
//        public async void ReserveMethod()
//        {
//            using (ZipFile zip = new ZipFile()) // Создаем объект для работы с архивом
//            {
//                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; // MAX степень сжатия
//                zip.AddDirectory(@"C:\Users\VsemPC\Desktop\touragency-master\Testovoe zadaniye\wwwroot\images"); // Кладем в архив папку вместе с содежимым
//                string fileName = System.IO.Path.GetRandomFileName();
//                DateTime now = DateTime.Now;
//                string time = now.ToString("date dd.MM.yyyy_hh-mm-ss");
//                string withoutExtensionFileName = Path.GetFileNameWithoutExtension(fileName);
//                zip.Save($@"C:\Users\VsemPC\Desktop\{withoutExtensionFileName}{time}.zip");  // Создаем архив  
//            }
//        }


//    }
//}

