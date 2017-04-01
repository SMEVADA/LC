using LetsConnect.Data.Domains.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.IGroup
{
   public interface IGroupRepository
    {
        List<Group> GetAll(int PageNumber, int PageSize);
        Group GetById(int Id);
        int AddNew(Group group);
        int Update(Group group);
        bool Delete(int Id);
    }
}
