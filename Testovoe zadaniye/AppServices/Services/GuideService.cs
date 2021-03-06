﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.AppServices.Interfaces;
using Testovoe_zadaniye.DataBase;
using Microsoft.EntityFrameworkCore;
using Testovoe_zadaniye.Paginator;
using Testovoe_zadaniye.Models.OperationModels;
using Testovoe_zadaniye.Models.Entities;

namespace Testovoe_zadaniye.AppServices.Services
{
    public class GuideService: IGuideService
    {
        TouragencyContext db;

        public GuideService(TouragencyContext context)
        {
            db = context;
        }
        public NewGuideForm CreateNewGuideForm()
        {
            NewGuideForm addGuide = new NewGuideForm();

            return addGuide;
        }
        public async Task<Guide> SaveNewGuideModel(NewGuideForm addGuide)
        {
            Guide guide = await db.Guides.FirstOrDefaultAsync(u => u.Login == addGuide.Login);
            if (guide == null)
            {
                // добавляем пользователя в бд
                db.Guides.Add(new Guide { Name = addGuide.Name, Login = addGuide.Login, Password = addGuide.Password });
                await db.SaveChangesAsync();
                
            }
            return guide;

        }
        public async Task<GuideEditForm> CreateCurrentGuideEditForm(int? id)
        {
            Guide guide = new Guide();
            if (id != 0)
            {
                guide = await db.Guides.Where(x => x.GuideId == id).FirstOrDefaultAsync();
            }
            GuideEditForm model = new GuideEditForm();
            model.Name = guide.Name;
            model.Login = guide.Login;
            model.Password = guide.Password;
            model.GuideId = guide.GuideId;

            return model;
        }
        public async Task SaveCurrentEditedGuideForm(GuideEditForm model)
        {
            Guide guide = new Guide();
            guide.Name = model.Name;
            guide.Login = model.Login;
            guide.Password = model.Password;
            guide.GuideId = model.GuideId;

            if (model.GuideId != 0)
            {
                db.Entry(guide).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
        public async Task<Guide> ProccesingGuideAuthorizationForm(GuidesAuthenticationForm guideLogin)
        {
            Guide guide = await db.Guides.FirstOrDefaultAsync(c => c.Login == guideLogin.Name && c.Password == guideLogin.Password);
            return guide;
        }
        public async Task<List<Guide>> AllGuidesList()
        {
            return await db.Guides.ToListAsync();
        }
        public async Task<Guide> GuideInfoById(int? id)
        {
            Guide guide = await db.Guides.FirstOrDefaultAsync(i => i.GuideId == id.Value);
            
            return guide;
        }
        public async Task DeleteCurrentGuide(int? id)
        {
            if (id != null)
            {
                var targetGuide = db.Guides.First(x => x.GuideId == id.Value); //Вынимаем тур по id из базы.
                db.Entry(targetGuide).State = EntityState.Deleted; // удаляем тур
                await db.SaveChangesAsync();
            }
        }

        public async Task<Pagin<Tour>> AllToursListForGuide(int pageNumber, int pageSize)
        {
            int ExcludeRecords = (pageNumber * pageSize) - pageSize;
            var result = new Pagin<Tour>
            {
                Data = await db.Tours.OrderBy(c => c.Name).Skip(ExcludeRecords).Take(pageSize).AsNoTracking().ToListAsync(),
                TotalItems = db.Tours.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }
    }
}
