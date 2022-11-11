using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Account
{
    public class LoginDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "نام کاربری اجباری میباشد")]
        public string Username { get; set; }
        [Required(ErrorMessage = "رمز عبور اجباری میباشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int UserType { get; set; }
        public bool IsAdmin { get; set; }
        public string ReturnUrl { get; set; }
    }
}
