using LetsConnect.Data.Domains.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.IPermission
{
   public interface IPermissionRepository
    {
        List<Permission> GetAll(int PageNumber, int PageSize);
        List<CustomPermission> GetById(int Id);
        int AddNew(Permission permission);
        int Update(Permission permission);
        bool Delete(int Id);
        int UpdateMultiple(List<Permission> permissionList, string tableName);
    }
}
