using Domain.DTO.Account;
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
    public class UserRepository : GenericRepository<UserDomain>, IUserDomain
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserInfoDTO>> GetAllUsersDTOAsync()
        {
            return await _context.Users.Select(r => new UserInfoDTO()
            {
                Id = r.Id,
                IsAdmin = r.IsAdmin,
                Username = r.Username,
                UserType = r.UserType
            }).ToListAsync();
        }

        public async Task<UserDomain> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(r => r.Username.ToLower() == username.ToLower());
        }
    }
}
