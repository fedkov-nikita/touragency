using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Testovoe_zadaniye.AppServices.Interfaces;
using Testovoe_zadaniye.Controllers;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.Models;
using Testovoe_zadaniye.Paginator;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Testovoe_zadaniye.FileUploading;
using Microsoft.AspNetCore.Hosting;
using Testovoe_zadaniye.Extensions;


namespace Testovoe_zadaniye.AppServices.Services
{
    public class TouristService : ITouristService
    {
        TouragencyContext db;
        int count;
        List<Tourist> touristData;
        Expression<Func<Tourist, bool>> filter = null;
        IWebHostEnvironment _appEnvironment;
        IFileManager _fileManagerServ;


        public TouristService(TouragencyContext context, IWebHostEnvironment webHostEnv, IFileManager fileManager)
        {
            db = context;
            _appEnvironment = webHostEnv;
            _fileManagerServ = fileManager;

        }
        public async Task<Pagin<Tourist>> FullTouristList(int? age, string homeTown, string searchString, int pageNumber, int pageSize)
        {

            if (age != null && age != 0)
            {
                filter = (a => a.Age == age);
            }

            if (!String.IsNullOrEmpty(homeTown))
            {
                if (filter != null)
                {
                    filter = filter.AndAlso(a => a.Hometown.Contains(homeTown));
                }
                else
                {
                    filter = (a => a.Hometown.Contains(homeTown));
                }
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                if (filter != null)
                {
                    filter = filter.AndAlso(c => c.Fullname.Contains(searchString));
                }
                else
                {
                    filter = (c => c.Fullname.Contains(searchString));
                }
            }

            return (await PaginationPartMethod(filter, pageSize, pageNumber));
        }

        public async Task<Pagin<Tourist>> PaginationPartMethod(Expression<Func<Tourist, bool>> filter, int pageSize, int pageNumber)
        {

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            IQueryable<Tourist> dataSearch = db.Tourists;
            if (filter != null)
            {
                dataSearch = dataSearch.Where(filter);
            }

            var data = await dataSearch.OrderBy(c => c.Fullname)
                                       .Skip(ExcludeRecords)
                                       .Take(pageSize)
                                       .ToListAsync();

            var dataCount = await dataSearch.CountAsync();
            count = dataCount;
            touristData = data;

            var result = new Pagin<Tourist>
            {
                Data = touristData,
                TotalItems = count,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return result;
        }



        public async Task<List<Tourist>> TouristListByGuideId(int? id)
        {
            var tourists = await db.Tourists.Where(c => c.GuideId == id).ToListAsync();

            return tourists;
        }

        public async Task<Addform> CreateNewTouristForm()
        {
            Addform model = new Addform();
            model.Guides = await db.Guides.ToListAsync();
            model.Tours = await db.Tours.ToListAsync();
            model.selectListg = new SelectList(model.Guides, "GuideId", "Name");
            model.selectListt = new MultiSelectList(model.Tours, "TourId", "Name");

            return model;
        }

       
        public async Task ProcessNewTouristModel(Addform model)
        {


            if (model.Avatar != null)
            {
                model.Path = await model.Avatar.PathReturn(_appEnvironment);
            }

            List<int> tours = model.Tours.Where(x => x.selected == true).Select(x => x.TourId).ToList();
            model.SelectedTourIds = tours;
        }

        public async Task ProcessEnteredTouristModel(Addform model)
        {
            model.Tours = await db.Tours.ToListAsync();

            foreach (var t in model.Tours)
            {
                if (model.SelectedTourIds.Contains(t.TourId))

                    t.selected = true;
            }
            model.Guides = await db.Guides.ToListAsync();
            model.selectListg = new SelectList(model.Guides, "GuideId", "Name");
            var selectedGuideItem = model.selectListg.First(x => x.Value == model.GuideId.ToString());
            selectedGuideItem.Selected = true;

        }

        public async Task<Tourist> SaveTouristModel(Addform model)
        {
            foreach (var t in model.Tours)
            {
                if (model.SelectedTourIds.Contains(t.TourId))
                    t.selected = true;
            }
            Tourist tourist = new Tourist();
            tourist.Fullname = model.Fullname;
            tourist.Hometown = model.Hometown;
            tourist.GuideId = model.GuideId;
            tourist.Age = model.Age;
            tourist.Avatar = model.Path;
            List<TouristTour> touristTours = new List<TouristTour>();
            await db.Tourists.AddAsync(tourist);
            // сохраняем в бд все изменения
            await db.SaveChangesAsync();
            foreach (var t in model.Tours.Where(x => x.selected == true))
            {
                TouristTour touristTour = new TouristTour();
                touristTour.TouristId = tourist.Touristid;
                touristTour.TourId = t.TourId;
                 touristTours.Add(touristTour);
            }
            await db.TouristTour.AddRangeAsync(touristTours);
            await db.SaveChangesAsync();

            return tourist;
        }
        public async Task<int> DeleteCurrentTourist(int? id)
        {
            if (id != null)
            {
                var targetTourist = await db.Tourists.FirstAsync(x => x.Touristid == id.Value); //Вынимаем турста по id из базы.
                int temp = targetTourist.GuideId; // сохраняем айди гида
                db.Entry(targetTourist).State = EntityState.Deleted; // удаляем туриста
                await db.SaveChangesAsync();
                return temp;
            }

            return 0;


        }

        public async Task<EditForm> ShowCurrentTouristEditForm(int? id)
        {
            Tourist tourist = new Tourist();
            if (id != 0)
            {
                tourist = await db.Tourists.Where(x => x.Touristid == id).FirstOrDefaultAsync();
            }
            EditForm model = new EditForm();
            model.Hometown = tourist.Hometown;
            model.Fullname = tourist.Fullname;
            model.Age = tourist.Age;
            model.GuideId = tourist.GuideId;
            model.Path = tourist.Avatar;
            model.Tours = await db.Tours.ToListAsync();
            model.SelectedTourIds = await db.TouristTour.Where(x => x.TouristId == tourist.Touristid).Select(x => x.TourId).ToListAsync();
            model.Guides = await db.Guides.ToListAsync();
            model.selectListg = new SelectList(model.Guides, "GuideId", "Name");
            model.Touristid = tourist.Touristid;
            foreach (var t in model.Tours)
            {
                if (model.SelectedTourIds.Contains(t.TourId))
                    t.selected = true;
            }

            return model;
        }

        public async Task SaveEditedTourist(EditForm model)
        {
            Tourist tourist = new Tourist();
            tourist.Fullname = model.Fullname;
            tourist.Hometown = model.Hometown;
            tourist.GuideId = model.GuideId;
            tourist.Age = model.Age;
            tourist.Touristid = model.Touristid;


            if (model.Avatar != null)
            {
                _fileManagerServ.DeletePhoto(model.Path);
                model.Path = await model.Avatar.PathReturn(_appEnvironment);
            }
            tourist.Avatar = model.Path;

            List<TouristTour> touristToursToRemove = new List<TouristTour>();
            touristToursToRemove = await db.TouristTour.Where(x => x.TouristId == tourist.Touristid).ToListAsync();
            db.RemoveRange(touristToursToRemove);
            await db.SaveChangesAsync();

            List<TouristTour> touristToursToAdd = new List<TouristTour>();
            foreach (var t in model.Tours.Where(x => x.selected == true))
            {
                TouristTour touristTour = new TouristTour();
                touristTour.TouristId = tourist.Touristid;
                touristTour.TourId = t.TourId;
                touristToursToAdd.Add(touristTour);
            }
            db.TouristTour.AddRange(touristToursToAdd);
            await db.SaveChangesAsync();

            if (model.Touristid != 0)
            {
                db.Entry(tourist).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

    }
}
