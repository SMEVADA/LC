using LetsConnect.Services.Interface.IPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsConnect.Data.Domains.Permission;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using LetsConnect.Core.Generic;
using LetsConnect.Services.Repository.RActivity;
using static LetsConnect.Core.Generic.Enums;

namespace LetsConnect.Services.Repository.RPermission
{
    public partial class PermissionRepository : IPermissionRepository
    {
        public static string connection = WebConfigurationManager.AppSettings["connection"].ToString();
        public static string encryptadminPasswordValue = WebConfigurationManager.AppSettings["encryptCookieValue"].ToString();

        public int AddNew(Permission permission)
        {
            int returnValue = 0;
            try
            {
                long Date = LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now);

                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@administratorId", permission.administratorId),
               new SqlParameter("@menuId", permission.menuId),
               new SqlParameter("@isCreate", permission.isCreate),
               new SqlParameter("@isDelete", permission.isDelete),
               new SqlParameter("@isModify", permission.isModify),
               new SqlParameter("@isView", permission.isView),
                new SqlParameter("@flag", '4')
                };

                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Permission", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = 1;
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
                new SqlParameter("@flag", '3')
                };
                DataAccess.ExecuteNonQuery(connection, "sp_Permission", sqlParameter);
                returnValue = true;
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        public List<Permission> GetAll(int PageNumber, int PageSize)
        {
            List<Permission> returnValue = new List<Permission>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@flag", '1')
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Permission", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<Permission>(dt);
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
        public List<CustomPermission> GetById(int Id)
        {
            List<CustomPermission> returnValue = new List<CustomPermission>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]{
                     new SqlParameter("@administratorId", Id),
                new SqlParameter("@flag", '7')
                };
                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Permission", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = LetsConnect.Core.Generic.Extensions.FromDataTableToList<CustomPermission>(dt);
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        public int Update(Permission permission)
        {
            int returnValue = 0;
            try
            {
                long Date = LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now);

                SqlParameter[] sqlParameter = new SqlParameter[]{
                new SqlParameter("@administratorId", permission.administratorId),
               new SqlParameter("@menuId", permission.menuId),
               new SqlParameter("@isCreate", permission.isCreate),
               new SqlParameter("@isDelete", permission.isDelete),
               new SqlParameter("@isModify", permission.isModify),
               new SqlParameter("@isView", permission.isView),
                new SqlParameter("@flag", '5')
                };

                DataTable dt = DataAccess.ExecuteDataTable(connection, "sp_Permission", sqlParameter);

                if (dt.Rows.Count > 0)
                {
                    returnValue = 1;
                }
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));

            }
            return returnValue;
        }

        public int UpdateMultiple(List<Permission> permissions, string tableName)
        {
            List<BasePermission> permissionList = ConvertToPList(permissions);
            int returnValue = 0;
            try
            {
                long id = permissionList.Select(c => c.administratorId).FirstOrDefault();
               bool isDeleted = Delete(Convert.ToInt32(id));
                if (isDeleted)
                {
                    #region Bulk Insert
                    using (var newconnection = new SqlConnection(connection))
                    {
                        newconnection.Open();
                        SqlTransaction transaction = newconnection.BeginTransaction();

                        using (var bulkCopy = new SqlBulkCopy(newconnection, SqlBulkCopyOptions.Default, transaction))
                        {
                            bulkCopy.BatchSize = 100;
                            bulkCopy.DestinationTableName = tableName;
                            try
                            {
                                bulkCopy.WriteToServer(permissionList.FromListToDataTable());
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                newconnection.Close();
                            }
                        }

                        transaction.Commit();
                        returnValue = 1;
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));

            }
            return returnValue;
        }

        public List<BasePermission> ConvertToPList(List<Permission> list)
        {
            List<BasePermission> returnValue = new List<BasePermission>();
            foreach (Permission basePermission in list)
            {
                BasePermission permission = new BasePermission();
                permission.administratorId = basePermission.administratorId;
                permission.menuId = basePermission.menuId;
                permission.isCreate = basePermission.isCreate;
                permission.isDelete = basePermission.isDelete;
                permission.isModify = basePermission.isModify;
                permission.isView = basePermission.isView;
                returnValue.Add(permission);
            }
            return returnValue;
        }
    }
}
