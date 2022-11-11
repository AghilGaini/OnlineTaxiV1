using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PersonDomain
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Max Length is 50")]
        public string Name { get; set; }
    }
}
