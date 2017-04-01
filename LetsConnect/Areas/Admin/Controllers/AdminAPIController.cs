using LetsConnect.Data.Domains.Administator;
using LetsConnect.Services.Repository.RActivity;
using LetsConnect.Services.Repository.RAdministator;
using LetsConnect.Services.Interface.IAdministator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static LetsConnect.Core.Generic.Enums;

namespace LetsConnect.Areas.Admin.Controllers
{
    public class AdminAPIController : ApiController
    {
        IAdministatorRepository administatorRepository = new AdministatorRepository();

        [Route("api/AdminAPI/Add")]
        [HttpPost]
        public Administator Add(Administator administator)
        {
            //Administator administator = new AdministatorRepository().ConvertToModel(administatorViewModel);
            Administator admin = new Administator();

            try
            {
                admin = ((IAdministatorRepository)administatorRepository).Add(administator);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return admin;
        }

        [Route("api/AdminAPI/GetAll")]
        [HttpGet]
        public List<Administator> GetAll(int PageNumber = 1, int PageSize = 10)
        {
            List<Administator> adminList = new List<Administator>();
            try
            {
                adminList = ((IAdministatorRepository)administatorRepository).GetAll(PageNumber, PageSize);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return adminList;
        }

        [Route("api/AdminAPI/GetAdminById")]
        [HttpGet]
        public Administator GetAdminById(int administratorId)
        {
            Administator admin = new Administator();
            try
            {
                admin = ((IAdministatorRepository)administatorRepository).GetById(administratorId);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return admin;
        }

        [Route("api/AdminAPI/Update")]
        [HttpPost]
        public Administator Update(Administator administator)
        {
            Administator admin = new Administator();
            try
            {
                admin = ((IAdministatorRepository)administatorRepository).Update(administator);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return admin;
        }

        [Route("api/AdminAPI/Delete")]
        [HttpGet]
        public bool Delete(int administratorId)
        {
            bool returnValue = false;
            try
            {
                returnValue = ((IAdministatorRepository)administatorRepository).Delete(administratorId);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }
    }
}
