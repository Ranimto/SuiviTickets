using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public IList<string> Roles { get;  set; }
        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
