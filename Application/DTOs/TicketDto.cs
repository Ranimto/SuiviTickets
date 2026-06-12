using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs
{
    public class TicketDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string Priority { get; set; } = null!;
        public string Status { get; set; } = null!;

        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public Guid? AssignedToId { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid CreatedById { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
