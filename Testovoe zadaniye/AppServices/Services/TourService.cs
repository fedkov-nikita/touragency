using Microsoft.EntityFrameworkCore;
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
        public NewTourForm CreateNewTourForm()
        {
            NewTourForm addTour = new NewTourForm();

            return addTour;
        }
        public async Task SaveNewTour(NewTourForm addTour)
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
        public TourEditForm CreateEditTourForm(int id) 
        {
            Tour tour = new Tour();
            if (id != 0)
            {
                tour = db.Tours.Where(x => x.TourId == id).FirstOrDefault();
            }
            TourEditForm model = new TourEditForm();
            model.Date = tour.Data;
            model.Name = tour.Name;
            model.TourId = tour.TourId;

            return model;
        }
        public async Task SaveEditedTour(TourEditForm model)
        {
            Tour tour = new Tour();
            tour.Data = model.Date;
            tour.Name = model.Name;
            tour.TourId = model.TourId;

            if (model.TourId != 0)
            {
                db.Entry(tour).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
        public async Task<List<Tour>> ToursOfChosenTourist(int? id)
        {
            List<Tour> tourList = await db.TouristTour.Where(
                        i => i.TouristId == id.Value).Select(i => i.Tour).ToListAsync();

            return tourList;

        }
        public async Task DeleteChosenTour(int? id)
        {
            if (id != null)
            {
                var targetTour = await db.Tours.FirstAsync(x => x.TourId == id.Value); //Вынимаем тур по id из базы.
                db.Entry(targetTour).State = EntityState.Deleted; // удаляем тур
                await db.SaveChangesAsync();
            }
        }
        public async Task<List<Tour>> ShowAllTours()
        {

            List<Tour> tourList = await db.Tours.ToListAsync();
            return tourList;
        }
    }
}
