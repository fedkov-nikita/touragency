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
    /// <summary>
    /// MainApp startup class for Structural
    /// Builder Design Pattern.
    /// </summary>


    /// <summary>
    /// The 'Director' class
    /// </summary>
    /// 
    class ReserveSave
    {
        ReserveFileManager director;
        ReserverBuilder builder;
        Reserver product;

        public ReserveSave()
        {
            director = new ReserveFileManager();
            builder = new ConcreteReserverBuilder();
            director.Construct(builder);
            product = builder.GetResult();


        }
        
        public void ReserveMethod()
        {
            
            using (ZipFile zip = new ZipFile()) // Создаем объект для работы с архивом
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; // MAX степень сжатия
                zip.AddDirectory(product.uploadLink); // Кладем в архив папку вместе с содежимым
                zip.Save($@"{product.zipLink}\{product.fileName}.zip");  // Создаем архив  
            }
        }
    }

    class ReserveFileManager
    {
        // Builder uses a complex series of steps
        public void Construct(ReserverBuilder builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
            builder.BuildPartC();
        }
    }

    /// <summary>
    /// The 'Builder' abstract class
    /// </summary>
    abstract class ReserverBuilder
    {
        public abstract void BuildPartA();
        public abstract void BuildPartB();
        public abstract void BuildPartC();
        public abstract Reserver GetResult();
    }

    /// <summary>
    /// The 'ConcreteBuilder1' class
    /// </summary>
    class ConcreteReserverBuilder : ReserverBuilder
    {
        private Reserver _product = new Reserver();

        public override void BuildPartA()
        {
            _product.zipLink = @"C:\Users\VsemPC\Desktop";
        }

        public override void BuildPartB()
        {
            _product.uploadLink = @"C:\Users\VsemPC\Desktop\touragency-master\Testovoe zadaniye\wwwroot\images";
        }

        public override void BuildPartC()
        {
            DateTime now = DateTime.Now;
            string time = now.ToString("date dd.MM.yyyy~hh-mm-ss");
            string fileName = System.IO.Path.GetRandomFileName();
            string withoutExtensionFileName = Path.GetFileNameWithoutExtension(fileName) + time;
            _product.fileName = withoutExtensionFileName;

        }

        public override Reserver GetResult()
        {
            return _product;
        }
    }

    class Reserver
    {
        public string uploadLink;

        public string zipLink;

        public string fileName;
    }
}


// Faceted builder realisation

//{

//    class Director
//    {
//        public ReserveSave Construct(Builder builder)
//        {
//            builder.CreateReserveSave();
//            builder.BuildPartA();
//            builder.BuildPartB();
//            builder.BuildPartC();
//            return builder.ReserveSave;
//        }
//    }

//    abstract class Builder
//    {
//        public ReserveSave ReserveSave { get; private set; }
//        public void CreateReserveSave()
//        {
//            ReserveSave = new ReserveSave();
//        }
//        public abstract void BuildPartA();
//        public abstract void BuildPartB();
//        public abstract void BuildPartC();
//    }

//    class ReserveSave
//    {
//        private string uploadLink;

//        public string UploadLink
//        {
//            get
//            {
//                return uploadLink;
//            }

//            set
//            {
//                uploadLink = value;
//            }
//        }
//        private string zipLink;
//        public string ZipLink { 
//            get 
//            {
//                return zipLink;
//            }
//            set 
//            { 
//                zipLink = value;
//            } 
//        }
//        private string fileName;
//        public string FileName {
//            get
//            {
//                return fileName;
//            }
//            set 
//            {
//                fileName = value;
//            }

//        }
//        public void ReserveMethod()
//        {
//            using (ZipFile zip = new ZipFile()) // Создаем объект для работы с архивом
//            {
//                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; // MAX степень сжатия
//                zip.AddDirectory(uploadLink); // Кладем в архив папку вместе с содежимым
//                zip.Save($@"{zipLink}{fileName}.zip");  // Создаем архив  
//            }
//        }
//    }

//    class ImageReserveBuilder : Builder
//    {
//        ReserveSave process = new ReserveSave();

//        public override void BuildPartA()
//        {
//            string folderToSave = @"‪C:\Users\Peppa\Desktop\";
//            this.ReserveSave.ZipLink = folderToSave;
//        }
//        public override void BuildPartB()
//        {
//            string folderToUpload = @"E:\TestovoeZadanie\Testovoe zadaniye\Testovoe zadaniye\wwwroot\images";
//            this.ReserveSave.UploadLink = folderToUpload;
//        }
//        public override void BuildPartC()
//        {
//            DateTime now = DateTime.Now;
//            string time = now.ToString("date dd.MM.yyyy~hh-mm-ss");
//            string fileName = System.IO.Path.GetRandomFileName();
//            string withoutExtensionFileName = Path.GetFileNameWithoutExtension(fileName) + time;
//            this.ReserveSave.FileName = withoutExtensionFileName;
//        }
//    }
//}


