using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Security
{
    public class UserRoleDTO
    {
        public long UserId { get; set; }
        public List<RoleInfoDTO> UserRoles { get; set; } = new List<RoleInfoDTO>();
    }
}
