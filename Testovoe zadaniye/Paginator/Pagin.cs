﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.Models.Entities;

namespace Testovoe_zadaniye.Paginator
{
     public class Pagin<T> where T : class
    {
        public Pagin() { }

            public List<T> Data { get; set; }
            public long TotalItems { get; set; }
            public long PageNumber { get; set; }
            public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalItems, PageSize));

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }

    }
}
