using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.AppServices.Services;

namespace Testovoe_zadaniye.AppServices.Interfaces
{
    public interface ILoggerCreator
    {
        public Logger FactoryMethod();
    }
}
