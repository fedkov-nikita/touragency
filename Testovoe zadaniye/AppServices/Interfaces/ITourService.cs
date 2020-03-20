using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.Models;

namespace Testovoe_zadaniye.AppServices.Interfaces
{
    public interface ITourService
    {
        public AddTour CreateNewTourForm();
        public Task SaveNewTour(AddTour addTour);
    }
}
