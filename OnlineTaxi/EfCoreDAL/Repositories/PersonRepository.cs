using Domain.DTO.Home;
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
    public class PersonRepository : GenericRepository<PersonDomain>, IPersonDomain
    {
        private readonly ApplicationContext _context;
        public PersonRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PersonInfoDTO>> GetAllDTOAsync()
        {
            return await _context.People.Select(r => new PersonInfoDTO()
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();
        }

        public async Task<bool> UpdatePersonDTOAsync(UpdatePersonDTO model)
        {
            try
            {
                var oldPerson = await _context.People.FirstOrDefaultAsync(r => r.Id == model.Id);

                if (oldPerson == null)
                    return false;

                oldPerson.Name = model.Name;

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
