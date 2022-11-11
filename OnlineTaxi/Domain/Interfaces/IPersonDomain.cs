using Domain.DTO.Home;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPersonDomain : IGenericDomain<PersonDomain>
    {
        Task<IEnumerable<PersonInfoDTO>> GetAllDTOAsync();
        Task<bool> UpdatePersonDTOAsync(UpdatePersonDTO model);
    }
}
