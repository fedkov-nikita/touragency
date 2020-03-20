using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.Models;

namespace Testovoe_zadaniye.AppServices.Interfaces
{
    public interface IGuideService
    {
        public AddGuide CreateNewGuideForm();
        public Task<Guide> SaveNewGuideModel(AddGuide addGuide);
        public Task<EditGuideForm> CreateCurrentGuideEditForm(int? id);
        public Task SaveCurrentEditedGuideForm(EditGuideForm model);
        public Task<Guide> ProccesingGuideAuthorizationForm(GuideLogin guideLogin);
    }
}
