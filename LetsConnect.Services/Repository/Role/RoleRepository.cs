using LetsConnect.Services.Interface.IRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsConnect.Data.Domains.Role;

namespace LetsConnect.Services.Repository.Role
{
    public partial class RoleRepository : IRoleRepository
    {
        public int AddNew(Data.Domains.Role.Role role)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Data.Domains.Role.Role> GetAll(int PageNumber, int PageSize)
        {
            throw new NotImplementedException();
        }

        public Data.Domains.Role.Role GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public int Update(Data.Domains.Role.Role role)
        {
            throw new NotImplementedException();
        }
    }
}
