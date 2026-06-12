using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs
{
    /// <summary>
    /// User DTO
    /// </summary>
    public class UserDto
    {
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
        public RoleEnum Role { get; set; } = RoleEnum.User;
    }
}
