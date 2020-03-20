using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.Models;
using Testovoe_zadaniye.AppServices.Interfaces;
using Testovoe_zadaniye.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Testovoe_zadaniye.AppServices.Services
{
    public class GuideService: IGuideService
    {
        TouragencyContext db;

        public GuideService(TouragencyContext context)
        {
            db = context;
        }
        public AddGuide CreateNewGuideForm()
        {
            AddGuide addGuide = new AddGuide();

            return addGuide;
        }
        public async Task<Guide> SaveNewGuideModel(AddGuide addGuide)
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
        public async Task<EditGuideForm> CreateCurrentGuideEditForm(int? id)
        {
            Guide guide = new Guide();
            if (id != 0)
            {
                guide = await db.Guides.Where(x => x.GuideId == id).FirstOrDefaultAsync();
            }
            EditGuideForm model = new EditGuideForm();
            model.Name = guide.Name;
            model.Login = guide.Login;
            model.Password = guide.Password;
            model.GuideId = guide.GuideId;

            return model;
        }
        public async Task SaveCurrentEditedGuideForm(EditGuideForm model)
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
        public async Task<Guide> ProccesingGuideAuthorizationForm(GuideLogin guideLogin)
        {
            Guide guide = await db.Guides.FirstOrDefaultAsync(c => c.Login == guideLogin.Name && c.Password == guideLogin.Password);
            return guide;
        }
    }
}
