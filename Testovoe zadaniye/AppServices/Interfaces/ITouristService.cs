using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Testovoe_zadaniye.Models;
using Testovoe_zadaniye.Paginator;

namespace Testovoe_zadaniye.AppServices.Interfaces
{
    public interface ITouristService
    {

        public Task<Pagin<Tourist>> FullTouristList(int? age, string homeTown, string searchString, int pageNumber, int pageSize);
        public Task<Pagin<Tourist>> PaginationPartMethod(Expression<Func<Tourist, bool>> filter, int pageSize, int pageNumber);
        public Task<List<Tourist>> TouristListByGuideId(int? id);
        public Task<Addform> CreateNewTouristForm();
        public Task ProcessNewTouristModel(Addform model);
        public Task ProcessEnteredTouristModel(Addform model);
        public Task<Tourist> SaveTouristModel(Addform model);
        public Task<int> DeleteCurrentTourist(int? id);
        public Task<EditForm> ShowCurrentTouristEditForm(int? id);
        public Task SaveEditedTourist(EditForm model);
    }
}
