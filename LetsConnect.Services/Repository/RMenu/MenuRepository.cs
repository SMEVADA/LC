using LetsConnect.Services.Interface.IInvite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsConnect.Data.Domains.Invite;
using LetsConnect.Services.Interface.IMenu;
using LetsConnect.Data.Domains.Menu;
using System.Data.SqlClient;
using System.Data;
using LetsConnect.Core.Generic;
using System.Web.Configuration;
using LetsConnect.Services.Repository.RActivity;
using static LetsConnect.Core.Generic.Enums;
using System.Security.Claims;
using System.Threading;

namespace LetsConnect.Services.Repository.RMenu
{

    public class MenuRepository : IMenuRepository
    {
        public static string connection = WebConfigurationManager.AppSettings["connection"].ToString();

        public int AddNew(Menu menu)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<MenuPermissions> GetAll(int PageNumber, int PageSize)
        {
            List<MenuPermissions> returnValue = new List<MenuPermissions>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@flag", '4'),
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Menu", sqlParameter);

                returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<MenuPermissions>(dt);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, Convert.ToInt64(DateTime.Now.Millisecond));
                return returnValue;
                throw ex;
            }
            return returnValue;
        }

        public List<MenuPermissions> GetAllBuUserId(int PageNumber, int PageSize)
        {
            List<MenuPermissions> returnValue = new List<MenuPermissions>();
            try
            {
                var identity1 = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var UserID = identity1.Claims.Where(c => c.Type == ClaimTypes.Sid)
                                   .Select(c => c.Value).SingleOrDefault();

                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@administratorId", UserID),
                new SqlParameter("@flag", '5'),
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Menu", sqlParameter);

                returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<MenuPermissions>(dt);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, Convert.ToInt64(DateTime.Now.Millisecond));
                return returnValue;
                throw ex;
            }
            return returnValue;
        }

        public Menu GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<MenuPermissions> GetMenuByAdministratorId(long administratorId)
        {
            List<MenuPermissions> returnValue = new List<MenuPermissions>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@administratorId", administratorId),
                new SqlParameter("@flag", '5'),
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Menu", sqlParameter);

                returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<MenuPermissions>(dt);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, Convert.ToInt64(DateTime.Now.Millisecond));
                return returnValue;
                throw ex;
            }
            return returnValue;
        }

        public int Update(Menu menu)
        {
            throw new NotImplementedException();
        }
    }
}
