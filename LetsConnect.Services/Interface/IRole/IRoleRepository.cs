using LetsConnect.Data.Domains.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.IRole
{
   public interface IRoleRepository
    {
        List<Role> GetAll(int PageNumber, int PageSize);
        Role GetById(int Id);
        int AddNew(Role role);
        int Update(Role role);
        bool Delete(int Id);
    }
}
