using LetsConnect.Services.Interface.ICommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsConnect.Data.Domains.Administator;
using System.Security.Claims;
using System.Threading;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Data;
using LetsConnect.Core.Generic;
using LetsConnect.Services.Repository.RActivity;
using static LetsConnect.Core.Generic.Enums;
using System.Reflection;
using LetsConnect.Services.Repository.RCommon;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using LetsConnect.Services.Interface.IMenu;
using LetsConnect.Services.Repository.RMenu;
using LetsConnect.Data.Domains.Menu;

namespace LetsConnect.Services.Repository.RCommon
{
    public partial class CommonRepository : ICommonRepository
    {
        public static string connection = WebConfigurationManager.AppSettings["connection"].ToString();
        public static string encryptCookieValue = WebConfigurationManager.AppSettings["encryptCookieValue"].ToString();
        public static string encryptadminPasswordValue = WebConfigurationManager.AppSettings["encryptCookieValue"].ToString();
        ActivityRepository activityRepository = new ActivityRepository();
        IMenuRepository menuRepository = new MenuRepository();

        public class dater
        {
            public int Dosage { get; set; }
            public string Drug { get; set; }
            public string Patient { get; set; }
            public DateTime Date { get; set; }
        }


        public Administator Login(string EmailAddress, string password)
        {
            Administator administator = new Administator();

            SqlConnection con = new SqlConnection(connection);
            try
            {
                #region testing on method

                //DataTable table = new DataTable();
                //table.Columns.Add("Dosage", typeof(int));
                //table.Columns.Add("Drug", typeof(string));
                //table.Columns.Add("Patient", typeof(string));
                //table.Columns.Add("Date", typeof(DateTime));

                //table.Rows.Add(25, "Indocin", "David", DateTime.Now);
                //table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
                //table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
                //table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
                //table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);


                //List<dater> rt1 = LetsConnect.Core.Generic.Extensions.FromDataTableToList<dater>(table);
                //string json = JsonConvert.SerializeObject(rt1);

                //int returnValue1 = new LetsConnect.Core.Generic.ValueCookies().AddCookies("CkPermission", json, 1);

                //string roll = new LetsConnect.Core.Generic.ValueCookies().GetCookies("CkPermission");

                //List<dater> UserList = JsonConvert.DeserializeObject<List<dater>>(roll);

                #endregion

                string passwordData = SecureValues.Encrypt(password, true, encryptadminPasswordValue);
                string data1 = SecureValues.Decrypt(passwordData, true, encryptadminPasswordValue);

                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@EmailAddress", EmailAddress),
                new SqlParameter("@password", passwordData),
                new SqlParameter("@flag", '1'),
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Administrator", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    administator = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Administator>(dt)[0];

                    #region getting all menu and set to cookies

                    HttpContext ctx = HttpContext.Current;
                    //Thread t = new Thread(new ThreadStart(() =>
                    //{
                    //    HttpContext.Current = ctx;
                    //    // getting all menus 
                    //    List<MenuPermissions> MenuList = ((IMenuRepository)menuRepository).GetMenuByAdministratorId(administator.administratorId);
                    //    // convert to string
                    //    string strMenuList = JsonConvert.SerializeObject(MenuList);
                    //    // encrypt it
                    //    SecureValues.Encrypt(strMenuList, true, encryptCookieValue);
                    //    //save to cookies
                    //    int returnValue = new LetsConnect.Core.Generic.ValueCookies().AddCookies("CkPermission", strMenuList, 1);

                    //}));
                    //t.Start();
                    //// [... do other job ...]
                    //t.Join();

                    Task.Factory.StartNew(() =>
                    {
                        HttpContext.Current = ctx;
                        // getting all menus 
                        List<MenuPermissions> MenuList = ((IMenuRepository)menuRepository).GetMenuByAdministratorId(administator.administratorId);
                        // convert to string
                        string strMenuList = JsonConvert.SerializeObject(MenuList);
                        // encrypt it
                        SecureValues.Encrypt(strMenuList, true, encryptCookieValue);
                        //save to cookies
                        int returnValue = new LetsConnect.Core.Generic.ValueCookies().AddCookies("CkPermission", strMenuList, 1);
                    });
                    #endregion
                }

                return administator;
            }
            catch (Exception ex)
            {

                activityRepository.AddNew(Convert.ToInt16(EnumactivityType.Error), ex, Convert.ToInt64(DateTime.Now.Millisecond));
                return administator;
                throw ex;
            }
        }

        public Administator Forgetpassword(string EmailAddress)
        {
            Administator administator = new Administator();
            SqlConnection con = new SqlConnection(connection);
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@EmailAddress", EmailAddress),
                new SqlParameter("@flag", '2'),
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Administrator", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    administator.administratorId = Convert.ToInt32(dt.Rows[0]["administratorId"]);
                    administator.administratorType = Convert.ToInt16(dt.Rows[0]["administratorType"]);
                    administator.emailID = Convert.ToString(dt.Rows[0]["emailID"]);
                    administator.mobileNo = Convert.ToString(dt.Rows[0]["mobileNo"]);
                    administator.userName = Convert.ToString(dt.Rows[0]["userName"]);
                    administator.password = Convert.ToString(dt.Rows[0]["password"]);

                }
                if (administator != null)
                {
                    string password = SecureValues.Decrypt(administator.password, true, encryptadminPasswordValue);

                    #region send mail to User

                    string Subject = "Forget Password  Request For Let's Connect";
                    string Body = "Password :" + Convert.ToString(password);
                    new InfoMailRepository().SendEmail(Subject, Body, "Forget Password", Convert.ToString(administator.emailID));
                    #endregion

                    return administator;
                }
                return administator;
            }
            catch (Exception ex)
            {

                activityRepository.AddNew(Convert.ToInt16(EnumactivityType.Error), ex, Convert.ToInt64(DateTime.Now.Millisecond), "Error From Send Email");
                return administator;
                throw ex;
            }
        }

        public string IsnullOrEmpty(string strvalue)
        {
            if (string.IsNullOrEmpty(strvalue) || strvalue == "undefined" || strvalue == "null")
                return null;

            return strvalue;
        }
    }
}
