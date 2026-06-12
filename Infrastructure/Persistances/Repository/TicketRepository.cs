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
    public class TicketRepository : EfRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(DbContext context, ILogger<EfRepository<Ticket>> logger) : base(context, logger)
        {
        }
    }
}
