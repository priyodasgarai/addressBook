using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace addressBook.Helpers
{
    public class CategoryQueryObject
    {
         public string? Name { get; set; } 
        public string? Description { get; set; } 
        public string? SortBy { get; set; } 
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}