using System.Collections.Generic;
using System.Threading.Tasks;
using Testovoe_zadaniye.Models.Entities;
using Testovoe_zadaniye.Models.OperationModels;

namespace Testovoe_zadaniye.AppServices.Interfaces
{
    public interface ITourService
    {
        public NewTourForm CreateNewTourForm();
        public Task SaveNewTour(NewTourForm addTour);
        public TourEditForm CreateEditTourForm(int id);
        public Task SaveEditedTour(TourEditForm model);
        public Task<List<Tour>> ToursOfChosenTourist(int? id);
        public Task DeleteChosenTour(int? id);
        public Task<List<Tour>> ShowAllTours();
    }
}
