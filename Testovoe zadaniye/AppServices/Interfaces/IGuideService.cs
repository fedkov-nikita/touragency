using System.Collections.Generic;
using System.Threading.Tasks;
using Testovoe_zadaniye.Models.Entities;
using Testovoe_zadaniye.Models.OperationModels;
using Testovoe_zadaniye.Paginator;

namespace Testovoe_zadaniye.AppServices.Interfaces
{
    public interface IGuideService
    {
        public NewGuideForm CreateNewGuideForm();
        public Task<Guide> SaveNewGuideModel(NewGuideForm addGuide);
        public Task<GuideEditForm> CreateCurrentGuideEditForm(int? id);
        public Task SaveCurrentEditedGuideForm(GuideEditForm model);
        public Task<Guide> ProccesingGuideAuthorizationForm(GuidesAuthenticationForm guideLogin);
        public Task<Guide> GuideInfoById(int? id);
        public Task<List<Guide>> AllGuidesList();
        public Task DeleteCurrentGuide(int? id);
        public Task<Pagin<Tour>> AllToursListForGuide(int pageNumber, int pageSize);    
    }
}
