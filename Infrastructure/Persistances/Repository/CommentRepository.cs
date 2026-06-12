using Application.Interfaces.IRepository;
using Domain.Entities;
using Infrastructure.Persistance.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistances.Repository
{
    public class CommentRepository : EfRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext context, ILogger<EfRepository<Comment>> logger) : base(context, logger)
        {
        }


        public Task<List<Comment>> GetCommentsByTicketIdAsync(Guid ticketId)
        {
            return _context.Set<Comment>()
                .Where(c => c.TicketId == ticketId)
                .ToListAsync();
        }
    }
}
