using LetsConnect.Data.Domains.Menu;
using LetsConnect.Services.Interface.IMenu;
using LetsConnect.Services.Repository.RActivity;
using LetsConnect.Services.Repository.RMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static LetsConnect.Core.Generic.Enums;

namespace LetsConnect.Areas.Admin.Controllers
{
    public class MenuAPIController : ApiController
    {
        IMenuRepository menuRepository = new MenuRepository();

        [Route("api/MenuAPI/GetAll")]
        [HttpGet]
        public List<MenuPermissions> GetAll(int PageNumber = 1, int PageSize = 10)
        {
            List<MenuPermissions> NewMenuList = new List<MenuPermissions>();
            try
            {
                NewMenuList = ((IMenuRepository)menuRepository).GetAll(PageNumber, PageSize);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return NewMenuList;
        }

        [Route("api/MenuAPI/GetAllByUserId")]
        [HttpGet]
        public List<MenuPermissions> GetAllByUserId(int PageNumber = 1, int PageSize = 10)
        {
            List<MenuPermissions> NewMenuList = new List<MenuPermissions>();
            try
            {
                NewMenuList = ((IMenuRepository)menuRepository).GetAllBuUserId(PageNumber, PageSize);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return NewMenuList;
        }

    }
}
