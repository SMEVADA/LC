using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Web.Framework.ViewModels.Permission
{
    public partial class PermissionViewModel
    {
        [Required(ErrorMessage = "Administrator Id is required")]
        public long administratorId { get; set; }

        [Required(ErrorMessage = "Menu Id is required")]
        public long menuId { get; set; }

        [Required(ErrorMessage = "IsView is required")]
        public bool isView { get; set; }

        [Required(ErrorMessage = "IsCreate is required")]
        public bool isCreate { get; set; }

        [Required(ErrorMessage = "IsDelete is required")]
        public bool isDelete { get; set; }

        [Required(ErrorMessage = "IsModify is required")]
        public bool isModify { get; set; }
    }
}
