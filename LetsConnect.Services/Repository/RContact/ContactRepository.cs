using LetsConnect.Services.Interface.IContact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsConnect.Data.Domains.Contact;
using System.Web.Configuration;
using System.Data.SqlClient;
using LetsConnect.Core.Generic;
using LetsConnect.Services.Repository.RActivity;
using static LetsConnect.Core.Generic.Enums;
using System.Data;
using LetsConnect.Services.Repository.RCommon;

namespace LetsConnect.Services.Repository.RContact
{
    public class ContactRepository : IContactRepository
    {
        public static string connection = WebConfigurationManager.AppSettings["connection"].ToString();
        public static string encryptadminPasswordValue = WebConfigurationManager.AppSettings["encryptCookieValue"].ToString();
        CommonRepository commonRepository = new CommonRepository();

        public int AddNew(Contact contact)
        {
            int returnValue = 0; ;
            try
            {
                long Date = LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now);

                SqlParameter[] sqlParameter = new SqlParameter[]{

                new SqlParameter("@customerId", contact.customerId),
                new SqlParameter("@name", contact.name),
                new SqlParameter("@emailId", contact.emailId),
                new SqlParameter("@mobileNo", contact.mobileNo),
                new SqlParameter("@website", contact.website),
                new SqlParameter("@createdDate", Date),
                new SqlParameter("@flag", '3')
                };

                return DataAccess.ExecuteNonQuery(connection, "sp_Contact", sqlParameter);
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
                     new SqlParameter("@contactId", Id),
                new SqlParameter("@flag", '5')
                };
                DataAccess.ExecuteNonQuery(connection, "sp_Contact", sqlParameter);
                returnValue = true;
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        public List<Contact> GetAll(int PageNumber, int PageSize)
        {
            List<Contact> returnValue = new List<Contact>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@flag", '2')
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Contact", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Contact>(dt);
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        public List<Contact> GetAllByParameters(int PageNumber, int PageSize, string Name, string MobileNo, string EmailId, long CustomerId)
        {
            List<Contact> returnValue = new List<Contact>();
            try
            {
                Name = commonRepository.IsnullOrEmpty(Name);
                MobileNo = commonRepository.IsnullOrEmpty(MobileNo);
                EmailId = commonRepository.IsnullOrEmpty(EmailId);
                // CustomerId = commonRepository.IsnullOrEmpty(CustomerId);

                SqlParameter[] sqlParameter = new SqlParameter[]{
                     new SqlParameter("@PageNumber", PageNumber),
                     new SqlParameter("@PageSize", PageSize),
                      new SqlParameter("@name",Name ),
                        new SqlParameter("@emailId",EmailId ),
                          new SqlParameter("@mobileNo", MobileNo),
                            new SqlParameter("@customerId",Convert.ToInt64(CustomerId)),
                new SqlParameter("@flag", 11)
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Contact", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Contact>(dt);
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;

           

            throw new NotImplementedException();
        }

        public Contact GetById(int Id)
        {
            Contact returnValue = new Contact();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@contactId", Id),
                new SqlParameter("@flag", '1')
                };

                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Contact", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Contact>(dt)[0];
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));

            }
            return returnValue;
        }

        public int Update(Contact contact)
        {
            int returnValue = 0; ;
            try
            {
                long Date = LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now);

                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@contactId", contact.contactId),
                new SqlParameter("@customerId", contact.customerId),
                new SqlParameter("@name", contact.name),
                new SqlParameter("@emailId", contact.emailId),
                new SqlParameter("@mobileNo", contact.mobileNo),
                new SqlParameter("@website", contact.website),
                new SqlParameter("@modifiedDate", Date),
                new SqlParameter("@flag", '4')
                };

                return DataAccess.ExecuteNonQuery(connection, "sp_Contact", sqlParameter);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));

            }
            return returnValue;
        }
    }
}
