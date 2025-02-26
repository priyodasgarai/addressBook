using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace addressBook.Dtos.Brand
{
    public class UpdateBrandRequestDto
    {
       public string Name { get; set; } = string.Empty;
        public string? Image { get; set; }      
        public int? Status { get; set; }

    }
}