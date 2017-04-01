using LetsConnect.Services.Interface.IActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsConnect.Data.Domains.Activity;
using System.Data.SqlClient;
using LetsConnect.Core.Generic;
using System.Data;
using System.Web.Configuration;

namespace LetsConnect.Services.Repository.RActivity
{
    public partial class ActivityRepository : IActivityRepository
    {
        public static string connection = WebConfigurationManager.AppSettings["connection"].ToString();
        Activity activity = new Activity();

        public int AddNew(Int16 activityType, Exception ex, long activityDate, string ManuallyErrorname = "")
        {
            string ErrorMsg = string.Empty;
            if (!string.IsNullOrEmpty(ManuallyErrorname))
            {
                ErrorMsg = ManuallyErrorname;
            }
            ErrorMsg = ErrorMsg + Convert.ToString(DateTime.Now)
                + "ErrorMessage=" + Convert.ToString(ex.Message) + " < br/>"
                + "StackTrace=" + Convert.ToString(ex.StackTrace) + "<br/>"
                + "InnerException=" + Convert.ToString(ex.InnerException) + "<br/>"
                + "Source=" + Convert.ToString(ex.Source) + "<br/>"
                + "HResult=" + Convert.ToString(ex.HResult) + "<br/>"
                + "TargetSite=" + Convert.ToString(ex.TargetSite) + "<br/>";

            int returnvalue;
            SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@activityType", activityType),
                new SqlParameter("@activity", ErrorMsg),
                  new SqlParameter("@activityDate", activityDate),
                new SqlParameter("@flag", '2'),
                };


            returnvalue = DataAccess.ExecuteNonQuery(connection, CommandType.StoredProcedure, "sp_Activity", sqlParameter);

            return returnvalue;

        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Activity> GetAll(int PageNumber, int PageSize)
        {
            throw new NotImplementedException();
        }

        public Activity GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public int Update(Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}
