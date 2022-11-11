using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IPersonDomain _person { get; set; }
        public IUserDomain _user { get; set; }
        public IRoleDomain _role { get; set; }
        public IPermisionDomain _permision { get; set; }
        public IRolePermisionDomain _rolePermision { get; set; }
        public IUserRoleDomain _userRole { get; set; }
        void Complete();
    }
}
