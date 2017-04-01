using LetsConnect.Web.Framework.ViewModels.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.Permission
{
    [MetadataType(typeof(PermissionViewModel))]
    public partial  class Permission
    {
        public long administratorId { get; set; }
        public long menuId { get; set; }
        public bool isView { get; set; }
        public bool isCreate { get; set; }
        public bool isDelete { get; set; }
        public bool isModify { get; set; }
        public Nullable<long> TotalRows { get; set; }
    }

    public partial class CustomPermission
    {
        public string fullName { get; set; }
        public string menuName { get; set; }
        public long administratorId { get; set; }
        public long menuId { get; set; }
        public bool isView { get; set; }
        public bool isCreate { get; set; }
        public bool isDelete { get; set; }
        public bool isModify { get; set; }
        public Nullable<long> TotalRows { get; set; }
    }

    public partial class BasePermission
    {
        public long administratorId { get; set; }
        public long menuId { get; set; }
        public bool isView { get; set; }
        public bool isCreate { get; set; }
        public bool isDelete { get; set; }
        public bool isModify { get; set; }
    }
}
