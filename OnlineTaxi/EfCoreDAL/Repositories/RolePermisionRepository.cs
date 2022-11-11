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
    public class RolePermisionRepository : GenericRepository<RolePermisionDomain>, IRolePermisionDomain
    {
        private readonly ApplicationContext _context;

        public RolePermisionRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeletePermisionsByRoleIdAsync(long roleId)
        {
            try
            {
                var rolePermisions = await _context.RolePermisions.Where(r => r.RoleId == roleId).ToListAsync();
                _context.RolePermisions.RemoveRange(rolePermisions);

                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<IEnumerable<long>> GetAllPermisionsIdByRoleIdAsync(long roleId)
        {
            return await _context.RolePermisions.Where(r => r.RoleId == roleId).Select(r => r.PermisionId).ToListAsync();
        }

        public async Task<bool> InsertRolePermisionRangeDTOAsync(PermisionDTO model)
        {
            try
            {
                var newRolePermisions = new List<RolePermisionDomain>();
                foreach (var item in model.Permisions)
                {
                    newRolePermisions.Add(new RolePermisionDomain()
                    {
                        PermisionId = item.Id,
                        RoleId = model.RoleId
                    });
                }

                await _context.RolePermisions.AddRangeAsync(newRolePermisions);

                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}
