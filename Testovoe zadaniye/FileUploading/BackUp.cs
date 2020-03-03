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
    class ReserveSave
    {
        Reserver reserver;

        public ReserveSave()
        {
            reserver = Reserver.CreateConResBuilder().BuildPartA()
                                                     .BuildPartB()
                                                     .BuildPartC()
                                                     .Build();
        }
        
        public void ReserveMethod()
        {
            
            using (ZipFile zip = new ZipFile()) // Создаем объект для работы с архивом
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; // MAX степень сжатия
                zip.AddDirectory(reserver.uploadLink); // Кладем в архив папку вместе с содежимым
                zip.Save($@"{reserver.zipLink}\{reserver.fileName}.zip");  // Создаем архив  
            }
        }
    }

    class ConcreteReserverBuilder 
    {
        private Reserver _reserver = new Reserver();


        public ConcreteReserverBuilder()
        {
            _reserver = new Reserver(); // создание обьекта класса Reserver
        }
        public  ConcreteReserverBuilder BuildPartA()
        {
            _reserver.zipLink = @"C:\Users\VsemPC\Desktop";
            return this; // возврат уже существующего обьекта класса ConcreteReseverBuilder
        }

        public ConcreteReserverBuilder BuildPartB()
        {
            _reserver.uploadLink = @"C:\Users\VsemPC\Desktop\touragency-master\Testovoe zadaniye\wwwroot\images";
            return this; // возврат уже существующего обьекта класса ConcreteReseverBuilder
        }

        public ConcreteReserverBuilder BuildPartC()
        {
            DateTime now = DateTime.Now;
            string time = now.ToString("date dd.MM.yyyy~hh-mm-ss");
            string fileName = System.IO.Path.GetRandomFileName();
            string withoutExtensionFileName = Path.GetFileNameWithoutExtension(fileName) + time;
            _reserver.fileName = withoutExtensionFileName;
            return this; // возврат уже существующего обьекта класса ConcreteReseverBuilder 

        }

        public Reserver Build()
        {
            return _reserver; // возврат обьекта класса Reserver
        }
    }

    class Reserver
    {
        public string uploadLink;

        public string zipLink;

        public string fileName;

        public static ConcreteReserverBuilder CreateConResBuilder()
        {
            return new ConcreteReserverBuilder();
        }
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


