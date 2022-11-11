﻿using Domain.DTO.Security;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRoleDomain : IGenericDomain<RoleDomain>
    {
        Task<IEnumerable<RoleInfoDTO>> GetAllDTOAsync();
    }
}
