using Domain.DTO.Security;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPermisionDomain : IGenericDomain<PermisionDomain>
    {
        Task<IEnumerable<PermisionInfoDTO>> GetAllDTOAsync();
        Task<bool> AddRange(IEnumerable<PermisionDomain> model);
    }
}
