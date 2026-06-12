using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs
{

    public class CommentDto
    {
        public Guid Id { get; set; }

        public Guid TicketId { get; set; }

        public string Message { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public Guid CreatedById { get; set; }
    }
}
