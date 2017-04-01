using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Web.Framework.ViewModels.Role
{
    public partial class RoleViewModel
    {
        [Required(ErrorMessage = "RoleName is required")]
        public string roleName { get; set; }
    }
}
