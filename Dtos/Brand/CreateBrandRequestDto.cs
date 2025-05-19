using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace addressBook.Dtos.Brand
{
    public class CreateBrandRequestDto
    {
        public string Name { get; set; } = string.Empty;

        public string? Image { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int Status { get; set; }

        
    }
}