using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.Models;

namespace Testovoe_zadaniye.AppServices.Interfaces
{
    public interface ITourService
    {
        public AddTour CreateNewTourForm();
        public Task SaveNewTour(AddTour addTour);
        public EditTourForm CreateEditTourForm(int id);
        public Task SaveEditedTour(EditTourForm);
        public Task<List<Tour>> ToursOfChosenTourist(int? id);
        public Task DeleteChosenTour(int? id);
        public Task<List<Tour>> ShowAllTours();
    }
}
