using LetsConnect.Data.Domains.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.IMenu
{
   public interface IMenuRepository
    {
        List<MenuPermissions> GetAll(int PageNumber, int PageSize);
        List<MenuPermissions> GetAllBuUserId(int PageNumber, int PageSize);
        Menu GetById(int Id);
        int AddNew(Menu menu);
        int Update(Menu menu);
        bool Delete(int Id);
        List<MenuPermissions> GetMenuByAdministratorId(long Id);
    }
}
