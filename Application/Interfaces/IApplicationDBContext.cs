using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDBContext
    {
       //On met juste les methodes et non les DbSet pour ne pas dependre à EF directement
    }
}
