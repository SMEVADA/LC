using LetsConnect.Data.Domains.Permission;
using LetsConnect.Services.Interface.IPermission;
using LetsConnect.Services.Repository.RActivity;
using LetsConnect.Services.Repository.RPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static LetsConnect.Core.Generic.Enums;

namespace LetsConnect.Areas.Admin.Controllers
{
    public class PermissionAPIController : ApiController
    {
        IPermissionRepository permissionRepository = new PermissionRepository();

        [Route("api/PermissionAPI/Add")]
        [HttpPost]
        public int Add(Permission Permission)
        {
            int permission = 0;

            try
            {
                permission = ((IPermissionRepository)permissionRepository).AddNew(Permission);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return permission;
        }

        [Route("api/PermissionAPI/GetAll")]
        [HttpGet]
        public List<Permission> GetAll(int PageNumber = 1, int PageSize = 10)
        {
            List<Permission> permissionList = new List<Permission>();
            try
            {
                permissionList = ((IPermissionRepository)permissionRepository).GetAll(PageNumber, PageSize);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return permissionList;
        }

        [Route("api/PermissionAPI/GetPermissionsByAdminId")]
        [HttpGet]
        public List<CustomPermission> GetPermissionsByAdminId(int Id)
        {
            List<CustomPermission> admin = new List<CustomPermission>();
            try
            {
                admin = ((IPermissionRepository)permissionRepository).GetById(Id);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return admin;
        }

        [Route("api/PermissionAPI/Update")]
        [HttpPost]
        public int Update(Permission Permission)
        {
            int permission = 0;
            try
            {
                permission = ((IPermissionRepository)permissionRepository).Update(Permission);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return permission;
        }

        [Route("api/PermissionAPI/UpdateMultiple")]
        [HttpPost]
        public int UpdateMultiple(List<Permission> PermissionList)
        {
            int permission = 0;
            try
            {
                permission = ((IPermissionRepository)permissionRepository).UpdateMultiple(PermissionList, "dbo.tblPermission");
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return permission;
        }

        [Route("api/PermissionAPI/Delete")]
        [HttpGet]
        public bool Delete(int Id)
        {
            bool returnValue = false;
            try
            {
                returnValue = ((IPermissionRepository)permissionRepository).Delete(Id);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }
    }
}
