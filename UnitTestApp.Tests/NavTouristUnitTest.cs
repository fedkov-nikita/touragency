using Microsoft.AspNetCore.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Testovoe_zadaniye.AppServices.Interfaces;
using Testovoe_zadaniye.Controllers;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.Models;
using Testovoe_zadaniye.Paginator;
using Xunit;

namespace UnitTestApp.Tests
{
    public class NavTouristUnitTest
    {
        int? age = 32;
        string homeTown = "Tokyo";
        string searchString = "Fluffy B.";
        int pageNumber = 1;
        int pageSize = 4;



        [Fact]
        public void TouristListTest()
        {
            


            //Arrange 
            var mock = new Mock<ITouristService>();
            mock.Setup(repo => repo.FullTouristList(age, homeTown, searchString, pageNumber, pageSize)).Returns(TouristTest());
        }

        
        //public async Task<Pagin<Tourist>> TouristTest()
        //{

        //    Pagin<Tourist> pagin = new Pagin<Tourist>
        //    {
               
        //    };

        //}




    }
}
