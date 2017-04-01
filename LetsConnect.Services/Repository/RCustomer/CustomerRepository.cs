using LetsConnect.Services.Interface.ICustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsConnect.Data.Domains.Customer;
using System.Web.Configuration;
using System.Data.SqlClient;
using LetsConnect.Core.Generic;
using System.Data;
using LetsConnect.Services.Repository.RActivity;
using static LetsConnect.Core.Generic.Enums;
using LetsConnect.Services.Repository.RCommon;

namespace LetsConnect.Services.Repository.RCustomer
{
    public class CustomerRepository : ICustomerRepository
    {
        public static string connection = WebConfigurationManager.AppSettings["connection"].ToString();
        public static string encryptadminPasswordValue = WebConfigurationManager.AppSettings["encryptCookieValue"].ToString();
        CommonRepository commonRepository = new CommonRepository();

        public int AddNew(Customer customer)
        {
            int returnValue = 0; ;
            try
            {
                long Date = LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now);

                SqlParameter[] sqlParameter = new SqlParameter[]{

                new SqlParameter("@firstName", customer.firstName),
                new SqlParameter("@lastName", customer.lastName),
                new SqlParameter("@emailId", customer.emailId),
                new SqlParameter("@mobileNo", customer.mobileNo),
                new SqlParameter("@address", customer.address),
                new SqlParameter("@latitude", customer.latitude),
                new SqlParameter("@longitude", customer.longitude),
                new SqlParameter("@socialId", customer.socialId),
                new SqlParameter("@socialType",customer.socialType),
                new SqlParameter("@isActive", true),
                new SqlParameter("@isDeleted", false),
                new SqlParameter("@createdDate", Date),
                new SqlParameter("@flag", '3')
                };

                return DataAccess.ExecuteNonQuery(connection, "sp_Customer", sqlParameter);
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
                     new SqlParameter("@customerId", Id),
                new SqlParameter("@flag", '5')
                };
                DataAccess.ExecuteNonQuery(connection, "sp_Customer", sqlParameter);
                returnValue = true;
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        public List<Customer> GetAll(int PageNumber, int PageSize)
        {
            List<Customer> returnValue = new List<Customer>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                     new SqlParameter("@PageNumber", PageNumber),
                     new SqlParameter("@PageSize", PageSize),
                new SqlParameter("@flag", '2')
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Customer", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Customer>(dt);
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        public List<Customer> GetAllByParameters(int PageNumber = 1, int PageSize = 10, string Name = null, string MobileNo = null, string EmailId = null, string Address = null)
        {
            List<Customer> returnValue = new List<Customer>();
            try
            {
                Name = commonRepository.IsnullOrEmpty(Name);
                MobileNo = commonRepository.IsnullOrEmpty(MobileNo);
                EmailId = commonRepository.IsnullOrEmpty(EmailId);
                Address = commonRepository.IsnullOrEmpty(Address);

                SqlParameter[] sqlParameter = new SqlParameter[]{
                     new SqlParameter("@PageNumber", PageNumber),
                     new SqlParameter("@PageSize", PageSize),
                      new SqlParameter("@firstName",Name ),
                        new SqlParameter("@emailId",EmailId ),
                          new SqlParameter("@mobileNo", MobileNo),
                            new SqlParameter("@address", Address),
                new SqlParameter("@flag", 11)
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Customer", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Customer>(dt);
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        public Customer GetById(int Id)
        {
            Customer returnValue = new Customer();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@customerId", Id),
                new SqlParameter("@flag", '1')
                };

                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Customer", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Customer>(dt)[0];
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));

            }
            return returnValue;
        }

        public int Update(Customer customer)
        {
            int returnValue = 0; ;
            try
            {
                long Date = LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now);

                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@customerId", customer.customerId),
                new SqlParameter("@firstName", customer.firstName),
                new SqlParameter("@lastName", customer.lastName),
                new SqlParameter("@emailId", customer.emailId),
                new SqlParameter("@mobileNo", customer.mobileNo),
                new SqlParameter("@address", customer.address),
                new SqlParameter("@latitude", customer.latitude),
                new SqlParameter("@longitude", customer.longitude),
                new SqlParameter("@socialId", customer.socialId),
                new SqlParameter("@socialType",customer.socialType),
                new SqlParameter("@isActive", true),
                new SqlParameter("@isDeleted", false),
                new SqlParameter("@modifiedDate", Date),
                new SqlParameter("@flag", '4')
                };

                return DataAccess.ExecuteNonQuery(connection, "sp_Customer", sqlParameter);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));

            }
            return returnValue;
        }


    }
}
