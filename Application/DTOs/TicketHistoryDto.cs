using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs
{
    public class TicketHistoryDto
    {
        public Guid Id { get; set; }

        public Guid TicketId { get; set; }

        public string OldStatus { get; set; } = null!;
        public string NewStatus { get; set; } = null!;

        public DateTime ChangedAt { get; set; }

        public Guid ChangedById { get; set; }
    }
}
