using LetsConnect.Web.Framework.ViewModels.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.Role
{
    [MetadataType(typeof(RoleViewModel))]
    public partial class Role
    {
        [Key]
        public long roleId { get; set; }
        public string roleName { get; set; }
        public Nullable<long> TotalRows { get; set; }
    }
}
