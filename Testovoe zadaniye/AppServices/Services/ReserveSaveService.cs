using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.AppServices.Interfaces;

namespace Testovoe_zadaniye.AppServices.Services
{
    class ReserveSave : IReserveSave
    {
        readonly Reserver reserver;

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
        private readonly Reserver _reserver = new Reserver();


        public ConcreteReserverBuilder()
        {
            _reserver = new Reserver(); // создание обьекта класса Reserver
        }
        public ConcreteReserverBuilder BuildPartA()
        {
            _reserver.zipLink = @"C:\Users\Peppa\Desktop";
            return this; // возврат уже существующего обьекта класса ConcreteReseverBuilder
        }

        public ConcreteReserverBuilder BuildPartB()
        {
            _reserver.uploadLink = @"E:\TestovoeZadanie\touragency\Testovoe zadaniye\wwwroot\images";
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
