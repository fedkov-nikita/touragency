using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.AppServices.Interfaces;
using Testovoe_zadaniye.Controllers;
using Testovoe_zadaniye.Models.Entities;
using Xunit;

namespace UnitTestApp.Tests
{
    public class NavTouristUnitTest
    {

        int id = 3;

        [Fact]
        public async void TouristListByGuideIdTest()
        {
            //Arrange 
            var mockTouristService = new Mock<ITouristService>();
            var mockLoggerService = new Mock<ILoggerCreator>();
            mockTouristService.Setup(repo => repo.TouristListByGuideId(id)).Returns(GetTestTourist());
            var controller = new TouristController(mockTouristService.Object, mockLoggerService.Object);

            //Act
            var result = await controller.TouristsOfCurrentGuideForm(id);
            var touristCount = await GetTestTourist();
            

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Tourist>>(
                viewResult.Model);
            Assert.Equal(touristCount.Count, model.Count());

        }
        private async Task<List<Tourist>> GetTestTourist()
        {
            Guide guide = new Guide() { Name = "John", GuideId = 2, Login = "ghjghj", Password = "555555", };
            Guide guide1 = new Guide() { Name = "Sara", GuideId = 1, Login = "sdfsdf", Password = "666666" };
            Tour tour = new Tour() { Data = DateTime.Now, Name = " Florencia Central Cathedral", TourId = 1};
            Tour tour1 = new Tour() { Data = DateTime.Now, Name = "Park Guel", TourId = 2 };
            Tour tour2 = new Tour() { Data = DateTime.Now, Name = "St.Baranabeo", TourId = 3 };


            var touristTours1 = new List<TouristTour>
            {
                new TouristTour{TourId = tour.TourId, TouristId = 1, Id = 1, Tour = tour },
                new TouristTour{TourId = tour1.TourId, TouristId = 1, Id = 2, Tour = tour1 },
                new TouristTour{TourId = tour2.TourId, TouristId = 1, Id = 3, Tour = tour2 }
            };
            var touristTours2 = new List<TouristTour>
            {
                new TouristTour{TourId = tour.TourId, TouristId = 2, Id = 4, Tour = tour },
                new TouristTour{TourId = tour1.TourId, TouristId = 2, Id = 5, Tour = tour1 },
                new TouristTour{TourId = tour2.TourId, TouristId = 2, Id = 6, Tour = tour2 }
            };
            var touristTours3 = new List<TouristTour>
            {
                new TouristTour{TourId = tour.TourId, TouristId = 3, Id = 7, Tour = tour },
                new TouristTour{TourId = tour1.TourId, TouristId = 3, Id = 8, Tour = tour1 },
                new TouristTour{TourId = tour2.TourId, TouristId = 3, Id = 9, Tour = tour2 }
            };

            var touristTours4= new List<TouristTour>
            {
                //id 7
                new TouristTour{TourId = tour.TourId, TouristId = 3, Id = 7, Tour = tour },
                //id 1
                new TouristTour{TourId = tour.TourId, TouristId = 1, Id = 1, Tour = tour },
                //id 4
                new TouristTour{TourId = tour.TourId, TouristId = 2, Id = 4, Tour = tour }

            };
            var touristTours5 = new List<TouristTour>
            {
                //id 8
                new TouristTour{TourId = tour1.TourId, TouristId = 3, Id = 8, Tour = tour1 },
                //id 2
                new TouristTour{TourId = tour1.TourId, TouristId = 1, Id = 2, Tour = tour1 },
                //id 5
                new TouristTour{TourId = tour1.TourId, TouristId = 2, Id = 5, Tour = tour1 }

            };
            var touristTours6 = new List<TouristTour>
            {
                //id 9
                new TouristTour{TourId = tour2.TourId, TouristId = 3, Id = 9, Tour = tour2 },
                //id 3
                new TouristTour{TourId = tour2.TourId, TouristId = 1, Id = 3, Tour = tour2 },
                // id 6
                new TouristTour{TourId = tour2.TourId, TouristId = 2, Id = 6, Tour = tour2 }

            };

            var tourists = new List<Tourist>
            {
                new Tourist {Age=25,
                    Fullname ="Witnick J.",
                    Guide= guide,
                    GuideId= guide.GuideId,
                    Hometown = "Rio",
                    TouristTours = touristTours1,
                    Touristid = 1,
                    Avatar = @"TestPath" },
                new Tourist {Age=28,
                    Fullname = "Griffin B.",
                    Guide= guide, GuideId=guide.GuideId,
                    Hometown = "Los-Angeles",
                    TouristTours = touristTours2,
                    Touristid=2,
                    Avatar= @"TestPath2" },
                new Tourist {Age=34,
                    Fullname="Michael J.",
                    Guide = guide,
                    GuideId = guide.GuideId,
                    Hometown = "Beyrut",
                    TouristTours = touristTours3,
                    Touristid = 3,
                    Avatar = @"TestPath3" }
                
            };

            guide.Tourists = tourists;

            tour.TouristTours = touristTours4;
            tour1.TouristTours = touristTours5;
            tour2.TouristTours = touristTours6;

            return await Task.FromResult(tourists);


        }
    }

}
     


