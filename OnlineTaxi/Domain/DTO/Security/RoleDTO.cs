using Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Security
{
    public class RoleDTO
    {
        public List<RoleInfoDTO> Roles { get; set; } = new List<RoleInfoDTO>();
        public List<ActionItems> Actions { get; set; } = new List<ActionItems>();
    }
    public class RoleInfoDTO
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "حداکثر تعداد مجاز 100 میباشد")]
        public string Title { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
