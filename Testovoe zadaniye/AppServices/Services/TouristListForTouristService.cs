using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Testovoe_zadaniye.AppServices.Interfaces;
using Testovoe_zadaniye.Controllers;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.Models;
using Testovoe_zadaniye.Paginator;
using Microsoft.EntityFrameworkCore;

namespace Testovoe_zadaniye.AppServices.Services
{
    public class TouristListForTouristService : ITouristListForTouristService
    {
        TouragencyContext db;

        public TouristListForTouristService(TouragencyContext context)
        {
            db = context;
        }
        public async Task<Pagin<Tourist>> FullTouristList(int? age, string homeTown, string searchString, int pageNumber, int pageSize)
        {

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            int count;
            List<Tourist> touristData;
            Expression<Func<Tourist, bool>> filter = null;

            if (age != null && age != 0)
            {
                filter = (a => a.Age == age);
            }

            if (!String.IsNullOrEmpty(homeTown))
            {
                if (filter != null)
                {
                    filter = filter.AndAlso(a => a.Hometown.Contains(homeTown));
                }
                else
                {
                    filter = (a => a.Hometown.Contains(homeTown));
                }
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                if (filter != null)
                {
                    filter = filter.AndAlso(c => c.Fullname.Contains(searchString));
                }
                else
                {
                    filter = (c => c.Fullname.Contains(searchString));
                }
            }

            IQueryable<Tourist> dataSearch = db.Tourists;
            if (filter != null)
            {
                dataSearch = dataSearch.Where(filter);
            }

            var data = await dataSearch.OrderBy(c => c.Fullname)
                                       .Skip(ExcludeRecords)
                                       .Take(pageSize)
                                       .ToListAsync();

            var dataCount = await dataSearch.CountAsync();
            count = dataCount;
            touristData = data;

            var result = new Pagin<Tourist>
            {
                Data = touristData,
                TotalItems = count,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Name = searchString,
                Age = age ?? 0,
                HomeTown = homeTown
            };

            return result;
        }

    }
}
