using Domain.Entities;
using Domain.Interfaces;
using EfCoreDAL.Context;
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

    }
}
