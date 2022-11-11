using Domain.DTO.Security;
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
    public class RoleRepository : GenericRepository<RoleDomain>, IRoleDomain
    {
        private readonly ApplicationContext _context;

        public RoleRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleInfoDTO>> GetAllDTOAsync()
        {
            return await _context.Roles.Select(r => new RoleInfoDTO()
            {
                Id = r.Id,
                Title = r.Title
            }).ToListAsync();
        }
    }
}
