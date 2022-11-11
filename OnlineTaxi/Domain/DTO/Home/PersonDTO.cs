using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Home
{
    public class PersonDTO
    {
        public List<PersonInfoDTO> PersonsInfo { get; set; } = new List<PersonInfoDTO>();
        public List<ActionItems> Actions { get; set; } = new List<ActionItems>();
    }
    public class PersonInfoDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
