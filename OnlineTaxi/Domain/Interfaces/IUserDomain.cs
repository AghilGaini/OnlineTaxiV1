using Domain.DTO.Account;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserDomain : IGenericDomain<UserDomain>
    {
        Task<UserDomain> GetByUsernameAsync(string username);
    }
}
