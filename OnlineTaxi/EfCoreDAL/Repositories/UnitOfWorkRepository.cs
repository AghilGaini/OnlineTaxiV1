using Domain.Interfaces;
using EfCoreDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreDAL.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public IPersonDomain _person { get; set; }

        public UnitOfWorkRepository(ApplicationContext context)
        {
            _context = context;
            _person = new PersonRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
