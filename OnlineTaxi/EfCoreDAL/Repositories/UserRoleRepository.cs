﻿using Domain.Entities;
using Domain.Interfaces;
using EfCoreDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreDAL.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRoleDomain>, IUserRoleDomain
    {
        private readonly ApplicationContext _context;

        public UserRoleRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
