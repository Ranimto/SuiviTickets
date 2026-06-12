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
    public class AttachmentRepository : EfRepository<Attachment>, IAttachementRepository
    {
        public AttachmentRepository(DbContext context, ILogger<EfRepository<Attachment>> logger) : base(context, logger)
        {
        }
    }
}
