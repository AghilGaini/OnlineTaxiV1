﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreDAL.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<PersonDomain> People { get; set; }
        public DbSet<UserDomain> Users { get; set; }
        public DbSet<RoleDomain> Roles { get; set; }
        public DbSet<PermisionDomain> Permisions { get; set; }
        public DbSet<RolePermisionDomain> RolePermisions { get; set; }
        public DbSet<UserRoleDomain> UserRoles { get; set; }

    }
}
