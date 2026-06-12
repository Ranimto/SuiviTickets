using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
