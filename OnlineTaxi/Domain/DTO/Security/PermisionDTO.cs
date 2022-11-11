using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Security
{

    public class PermisionDTO
    {
        public List<PermisionInfoDTO> Permisions { get; set; } = new List<PermisionInfoDTO>();
        public long RoleId { get; set; }
    }
    public class PermisionInfoDTO
    {
        [Required(ErrorMessage = "شناسه اجباری میباشد")]
        public long Id { get; set; }
        [Required(ErrorMessage = "شناسه اجباری میباشد")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "شناسه اجباری میباشد")]
        [StringLength(100)]
        public string Value { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
