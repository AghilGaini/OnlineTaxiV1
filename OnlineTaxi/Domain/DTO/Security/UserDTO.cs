using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Security
{
    public class UserDTO
    {
        public List<UserInfoDTO> Users { get; set; } = new List<UserInfoDTO>();
        public List<ActionItems> Actions { get; set; } = new List<ActionItems>();
    }

    public class UserInfoDTO
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public int UserType { get; set; }
    }
}
