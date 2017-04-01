using LetsConnect.Core.Generic;
using LetsConnect.Data.Domains.Inquiry;
using LetsConnect.Services.Interface.IInquiry;
using LetsConnect.Services.Repository.RActivity;
using LetsConnect.Services.Repository.RCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using static LetsConnect.Core.Generic.Enums;

namespace LetsConnect.Services.Repository.RInquiry
{
    public class InquiryRepository : IInquiryRepository
    {

        public static string connection = WebConfigurationManager.AppSettings["connection"].ToString();

        public int AddInquiry(Inquiry inquiryModel)
        {
            int returnValue = 0; ;
            try
            {
                long Date = LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now);

                SqlParameter[] sqlParameter = new SqlParameter[]{

                new SqlParameter("@Name", inquiryModel.Name),
                new SqlParameter("@Email", inquiryModel.Email),
                new SqlParameter("@Mobile", inquiryModel.Mobile),
                new SqlParameter("@Address", inquiryModel.Address),
                new SqlParameter("@Message", inquiryModel.Message),
                new SqlParameter("@flag", '3')
                };


                returnValue = DataAccess.ExecuteNonQuery(connection, "sp_Inquiry", sqlParameter);

                #region send mail to info@Baadam.com

                #region send mail to User

                string Subject = "New Inquiry  " + Convert.ToString(DateTime.Now);
                string Body = "Name :" + Convert.ToString(inquiryModel.Name) + "<br />" + "Email :" + Convert.ToString(inquiryModel.Name) + "<br />" + "Mobile :" + Convert.ToString(inquiryModel.Mobile) + "<br />" + "Address :" + Convert.ToString(inquiryModel.Address) + "<br />" + "Message :" + Convert.ToString(inquiryModel.Message);
                new InfoMailRepository().SendEmail(Subject, Body, "Inquiry");
                #endregion

                #endregion


            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));

            }
            return returnValue;

        }

        public List<Inquiry> GetAllInquiry()
        {

            List<Inquiry> returnValue = new List<Inquiry>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                     new SqlParameter("@PageNumber", 1),
                     new SqlParameter("@PageSize", int.MaxValue),
                new SqlParameter("@flag", '2')
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Inquiry", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Inquiry>(dt);
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;

        }

        public Inquiry GetInquiryByInquityId(int inquiryID)
        {

            Inquiry returnValue = new Inquiry();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@InquiryID", inquiryID),
                new SqlParameter("@flag", '1')
                };

                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Inquiry", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Inquiry>(dt)[0];
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));

            }
            return returnValue;


        }
    }
}
