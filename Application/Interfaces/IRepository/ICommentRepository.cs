using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<Comment>> GetCommentsByTicketIdAsync(Guid ticketId);
    }
}
