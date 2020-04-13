using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.Models.Entities;
using Testovoe_zadaniye.Paginator;

namespace Testovoe_zadaniye.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        TouragencyContext db;
        public DefaultController(TouragencyContext context)
        {
            db = context;
        }
        public Pagin<Tourist> ToVueMethod(int pageSize = 4, int pageNumber=1)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            IQueryable<Tourist> dataSearch = db.Tourists;
            var data = dataSearch.OrderBy(c => c.Fullname)
                                       .Skip(ExcludeRecords)
                                       .Take(pageSize)
                                       .ToList();

            var dataCount = dataSearch.Count();

            var result = new Pagin<Tourist>
            {
                Data = data,
                TotalItems = dataCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return result;
        }

    }
}