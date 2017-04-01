using LetsConnect.Services.Interface.IAdministator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsConnect.Data.Domains.Administator;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using LetsConnect.Core.Generic;
using static LetsConnect.Core.Generic.Enums;
using LetsConnect.Services.Repository.RActivity;
using System.Security.Claims;
using System.Threading;
using LetsConnect.Web.Framework.ViewModels.Administator;

namespace LetsConnect.Services.Repository.RAdministator
{
    public class AdministatorRepository : IAdministatorRepository
    {
        public static string connection = WebConfigurationManager.AppSettings["connection"].ToString();
        public static string encryptadminPasswordValue = WebConfigurationManager.AppSettings["encryptCookieValue"].ToString();

        public Administator Add(Administator administator)
        {
            Administator returnValue = new Administator();
            try
            {
                string password = SecureValues.Encrypt(administator.password, true, encryptadminPasswordValue);

                var identity1 = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var UserID = identity1.Claims.Where(c => c.Type == ClaimTypes.Sid)
                                   .Select(c => c.Value).SingleOrDefault();
                long Date = LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now);

                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@userName", administator.userName),
                new SqlParameter("@password", password),
                new SqlParameter("@fullName", administator.fullName),
                new SqlParameter("@EmailAddress", administator.emailID),
                new SqlParameter("@mobileNo", administator.mobileNo),
                new SqlParameter("@isActive", true),
                new SqlParameter("@administratorType", administator.administratorType),
                new SqlParameter("@createdBy", UserID),
                new SqlParameter("@createdDate", Date),
                new SqlParameter("@flag", '3')
                };

                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Administrator", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Administator>(dt)[0];

                    //adding default null permissions
                    Task.Factory.StartNew(() =>
                    {
                        SqlParameter[] newsqlParameter = new SqlParameter[]{
                        new SqlParameter("@administratorId", returnValue.administratorId),
                         new SqlParameter("@flag", '6')
                         };
                        DataAccess.ExecuteNonQuery(connection, "sp_Permission", newsqlParameter);
                    });
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));

            }
            return returnValue;
        }

        public bool Delete(int Id)
        {
            bool returnValue = false;
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                     new SqlParameter("@administratorId", Id),
                new SqlParameter("@flag", '7')
                };
                DataAccess.ExecuteNonQuery(connection, "sp_Administrator", sqlParameter);
                returnValue = true;
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        public List<Administator> GetAll(int PageNumber, int PageSize)
        {
            List<Administator> returnValue = new List<Administator>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                     new SqlParameter("@PageNumber", PageNumber),
                      new SqlParameter("@PageSize", PageSize),
                new SqlParameter("@flag", '4')
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Administrator", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Administator>(dt);
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;

        }

        /// <summary>
        /// get by admin id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Administator GetById(int Id)
        {
            Administator returnValue = new Administator();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@administratorId", Id),
                new SqlParameter("@flag", '5')
                };

                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Administrator", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Administator>(dt)[0];
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));

            }
            return returnValue;
        }

        public Administator Update(Administator administator)
        {
            Administator returnValue = new Administator();
            try
            {
                string password = SecureValues.Encrypt(administator.password, true, encryptadminPasswordValue);

                var identity1 = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var UserID = identity1.Claims.Where(c => c.Type == ClaimTypes.Sid)
                                   .Select(c => c.Value).SingleOrDefault();
                long Date = LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now);

                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@administratorId", administator.administratorId),
                new SqlParameter("@userName", administator.userName),
                new SqlParameter("@password", password),
                new SqlParameter("@fullName", administator.fullName),
                new SqlParameter("@EmailAddress", administator.emailID),
                new SqlParameter("@mobileNo", administator.mobileNo),
                new SqlParameter("@isActive", true),
                new SqlParameter("@administratorType", administator.administratorType),
                new SqlParameter("@updatedBy", UserID),
                new SqlParameter("@modifiedDate", Date),
                new SqlParameter("@flag", '6')
                };

                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Administrator", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Administator>(dt)[0];
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));

            }
            return returnValue;
        }

        public Administator ConvertToModel(AdministatorViewModel administatorViewModel)
        {
            Administator Admin = new Administator();
            Admin.administratorType = 2;
            Admin.emailID = administatorViewModel.emailID;
            Admin.fullName = administatorViewModel.fullName;
            Admin.mobileNo = administatorViewModel.mobileNo;
            Admin.userName = administatorViewModel.userName;
            return Admin;
        }

    }
}
