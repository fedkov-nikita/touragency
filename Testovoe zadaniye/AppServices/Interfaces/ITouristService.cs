using System;
using System.Collections.Generic;
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
        public Task<NewTouristForm> CreateNewTouristForm();
        public Task ProcessNewTouristModel(NewTouristForm model);
        public Task ProcessEnteredTouristModel(NewTouristForm model);
        public Task<Tourist> SaveTouristModel(NewTouristForm model);
        public Task<int> DeleteCurrentTourist(int? id);
        public Task<TouristEditForm> ShowCurrentTouristEditForm(int? id);
        public Task SaveEditedTourist(TouristEditForm model);
    }
}
