using Domain.DTO.Info;
using Domain.Entities;
using Domain.Interfaces;
using EfCoreDAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreDAL.Repositories
{
    public class UserTypeRepository : GenericRepository<UserTypeDomain>, IUserTypeDomain
    {
        public ApplicationContext _context { get; }
        public UserTypeRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UserTypeDTO>> GetAllDTOAsync()
        {
            return await _context.UserTypes.Select(r => new UserTypeDTO()
            {
                Id = r.Id,
                Title = r.Title
            }).ToListAsync();
        }
    }
}
