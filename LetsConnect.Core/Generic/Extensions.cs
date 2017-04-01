using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LetsConnect.Core.Generic
{
    public static class Extensions
    {

        public static List<TSource> FromDataTableToList<TSource>(this DataTable dataTable) where TSource : new()
        {
            var dataList = new List<TSource>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var objFieldNames = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                                 select new
                                 {
                                     Name = aProp.Name,
                                     Type = Nullable.GetUnderlyingType(aProp.PropertyType) ??
                         aProp.PropertyType
                                 }).ToList();
            var dataTblFieldNames = (from DataColumn aHeader in dataTable.Columns
                                     select new
                                     {
                                         Name = aHeader.ColumnName,
                                         Type = aHeader.DataType
                                     }).ToList();
            var commonFields = objFieldNames.Intersect(dataTblFieldNames).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var aTSource = new TSource();
                foreach (var aField in commonFields)
                {
                    PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
                    var value = (dataRow[aField.Name] == DBNull.Value) ?
                    null : dataRow[aField.Name]; //if database field is nullable
                    propertyInfos.SetValue(aTSource, value, null);
                }
                dataList.Add(aTSource);
            }
            return dataList;
        }

        public static DataTable FromListToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            object[] values = new object[props.Count];
            using (DataTable table = new DataTable())
            {
                long _pCt = props.Count;
                for (int i = 0; i < _pCt; ++i)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
                foreach (T item in data)
                {
                    long _vCt = values.Length;
                    for (int i = 0; i < _vCt; ++i)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    table.Rows.Add(values);
                }
                return table;
            }
        }

        #region Extra Methods for future use

        /// <summary>
        /// Convert our List to a DataSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>DataSet</returns>
        public static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            using (DataSet ds = new DataSet())
            {
                using (DataTable t = new DataTable())
                {
                    ds.Tables.Add(t);
                    //add a column to table for each public property on T
                    PropertyInfo[] _props = elementType.GetProperties();
                    foreach (PropertyInfo propInfo in _props)
                    {
                        Type _pi = propInfo.PropertyType;
                        Type ColType = Nullable.GetUnderlyingType(_pi) ?? _pi;
                        t.Columns.Add(propInfo.Name, ColType);
                    }
                    //go through each property on T and add each value to the table
                    foreach (T item in list)
                    {
                        DataRow row = t.NewRow();
                        foreach (PropertyInfo propInfo in _props)
                        {
                            row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                        }
                        t.Rows.Add(row);
                    }
                }
                return ds;
            }
        }



        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static IList<T> ConvertToList<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue
                            (obj, value.ToString(), null);
                    }
                    catch
                    {
                        // You can log something here
                        throw;
                    }
                }
            }

            return obj;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }

        ///// <summary>
        ///// Converts datatable to list<T> dynamically
        ///// </summary>
        ///// <typeparam name="T">Class name</typeparam>
        ///// <param name="dataTable">data table to convert</param>
        ///// <returns>List<T></returns>
        ////public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        ////{
        ////    try
        ////    {
        ////        List<T> list = new List<T>();

        ////        foreach (var row in table.AsEnumerable())
        ////        {
        ////            T obj = new T();

        ////            foreach (var prop in obj.GetType().GetProperties())
        ////            {
        ////                try
        ////                {
        ////                    PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
        ////                    propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
        ////                }
        ////                catch
        ////                {
        ////                    continue;
        ////                }
        ////            }

        ////            list.Add(obj);
        ////        }

        ////        return list;
        ////    }
        ////    catch
        ////    {
        ////        return null;
        ////    }
        ////}
        //public static IEnumerable<T> DataTableToList<T>(this DataTable table) where T : class, new()
        //{
        //    try
        //    {
        //        var objType = typeof(T);
        //        ICollection<PropertyInfo> properties;

        //        lock (_Properties)
        //        {
        //            if (!_Properties.TryGetValue(objType, out properties))
        //            {
        //                properties = objType.GetProperties().Where(property => property.CanWrite).ToList();
        //                _Properties.Add(objType, properties);
        //            }
        //        }

        //        var list = new List<T>(table.Rows.Count);

        //        foreach (var row in table.AsEnumerable().Skip(1))
        //        {
        //            var obj = new T();

        //            foreach (var prop in properties)
        //            {
        //                try
        //                {
        //                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
        //                    var safeValue = row[prop.Name] == null ? null : Convert.ChangeType(row[prop.Name], propType);

        //                    prop.SetValue(obj, safeValue, null);
        //                }
        //                catch
        //                {
        //                    // ignored
        //                }
        //            }

        //            list.Add(obj);
        //        }

        //        return list;
        //    }
        //    catch
        //    {
        //        return Enumerable.Empty<T>();
        //    }
        //}

        //public static DataTable ConvertToDataTable<TSource>(this IList<TSource> data)
        //{
        //    DataTable dataTable = new DataTable(typeof(TSource).Name);
        //    PropertyInfo[] props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    foreach (PropertyInfo prop in props)
        //    {
        //        dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
        //            prop.PropertyType);
        //    }

        //    foreach (TSource item in data)
        //    {
        //        var values = new object[props.Length];
        //        for (int i = 0; i < props.Length; i++)
        //        {
        //            values[i] = props[i].GetValue(item, null);
        //        }
        //        dataTable.Rows.Add(values);
        //    }
        //    return dataTable;
        //}

        ////private static string ConvertToDateString(object date)
        ////{
        ////    if (date == null)
        ////        return string.Empty;

        ////    return SpecialDateTime.ConvertDate(Convert.ToDateTime(date));
        ////}

        ////private static string ConvertToString(object value)
        ////{
        ////    return Convert.ToString(HelperFunctions.ReturnEmptyIfNull(value));
        ////}

        ////private static int ConvertToInt(object value)
        ////{
        ////    return Convert.ToInt32(HelperFunctions.ReturnZeroIfNull(value));
        ////}

        ////private static long ConvertToLong(object value)
        ////{
        ////    return Convert.ToInt64(HelperFunctions.ReturnZeroIfNull(value));
        ////}

        ////private static decimal ConvertToDecimal(object value)
        ////{
        ////    return Convert.ToDecimal(HelperFunctions.ReturnZeroIfNull(value));
        ////}

        ////private static DateTime convertToDateTime(object date)
        ////{
        ////    return Convert.ToDateTime(HelperFunctions.ReturnDateTimeMinIfNull(date));
        ////}

        #endregion
    }

    public static class EpochTimeExtensions
    {
        /// <summary>
        /// Converts the given date value to epoch time.
        /// </summary>
        public static long ToEpochTime(this DateTime dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts;
        }

        /// <summary>
        /// Converts the given date value to epoch time.
        /// </summary>
        public static long ToEpochTime(this DateTimeOffset dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts;
        }

        /// <summary>
        /// Converts the given epoch time to a <see cref="DateTime"/> with <see cref="DateTimeKind.Utc"/> kind.
        /// </summary>
        public static DateTime ToDateTimeFromEpoch(this long intDate)
        {
            var timeInTicks = intDate * TimeSpan.TicksPerSecond;
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(timeInTicks);
        }

        /// <summary>
        /// Converts the given epoch time to a UTC <see cref="DateTimeOffset"/>.
        /// </summary>
        public static DateTimeOffset ToDateTimeOffsetFromEpoch(this long intDate)
        {
            var timeInTicks = intDate * TimeSpan.TicksPerSecond;
            return new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).AddTicks(timeInTicks);
        }
    }

    public class ValueCookies
    {
        public int AddCookies(string CookiName,string CookiValue,int CookieDay)
        {
            if (HttpContext.Current.Request.Cookies[CookiName] != null)
                HttpContext.Current.Response.Cookies.Remove(CookiName);

            HttpCookie StudentCookies = new HttpCookie(CookiName);
            StudentCookies.Value = CookiValue;
            StudentCookies.Expires = DateTime.Now.AddDays(CookieDay);
            HttpContext.Current.Request.Cookies.Add(StudentCookies);
            return 1;
        }

        public string GetCookies(string CookiName)
        {
            return HttpContext.Current.Request.Cookies[CookiName].Value;
        }
    }

    public class SecureCookies
    {
        public string EncryptCookies(string CookieValue,string CookieKey)
        {
            return string.Empty;
        }

        public string DecryptCookies(string CookieValue, string CookieKey)
        {
            return string.Empty;
        }
    }

    public class SecureValues
    {
        public static string Encrypt(string toEncrypt, bool useHashing,string keyValue = "KeyBaddam")
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = keyValue;
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing, string keyValue = "KeyBaddam")
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = keyValue;

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}




