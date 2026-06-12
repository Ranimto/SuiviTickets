using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// User
    /// </summary>
    public class User 
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public string PasswordHash { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public RoleEnum Role { get; set; } = RoleEnum.User;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
