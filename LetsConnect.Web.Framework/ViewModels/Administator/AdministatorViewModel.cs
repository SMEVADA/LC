using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Web.Framework.ViewModels.Administator
{
    public partial class AdministatorViewModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        public string fullName { get; set; }

        [Required(ErrorMessage = "EmailID is required")]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Enter Valid EmailID.")]
        //[DataType(DataType.EmailAddress)]
        public string emailID { get; set; }

        [Required(ErrorMessage = "MobileNo is required")]
        [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Please Enter Valid MobileNo.")]
        public string mobileNo { get; set; }

        //[Required(ErrorMessage = "IsActive is required")]
        //public bool isActive { get; set; }

        [Required(ErrorMessage = "AdministratorType is required")]
        //public Int16 administratorType { get; set; }
        public Nullable<int> administratorType { get; set; }
    }
}
