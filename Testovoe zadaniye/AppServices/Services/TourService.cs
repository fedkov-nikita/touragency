using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.AppServices.Interfaces;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.Models;

namespace Testovoe_zadaniye.AppServices.Services
{
    public class TourService: ITourService
    {
        TouragencyContext db;
        public TourService(TouragencyContext context)
        {
            db = context;
        }
        public AddTour CreateNewTourForm()
        {
            AddTour addTour = new AddTour();

            return addTour;
        }
        public async Task SaveNewTour(AddTour addTour)
        {
            Tour tour = new Tour();
            tour.Name = addTour.Name;
            tour.Data = addTour.Date;
            if (tour.Name != null && tour.Data != null)
            {
                await db.Tours.AddAsync(tour);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Some field of tour is null");
            }
        }
    }
}
