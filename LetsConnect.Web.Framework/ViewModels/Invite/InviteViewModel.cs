using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Web.Framework.ViewModels.Invite
{
    public partial class InviteViewModel
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public long customerId { get; set; }

        [Required(ErrorMessage = "MobileNo is required")]
        [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Please Enter Valid MobileNo.")]
        public string mobileNo { get; set; }

        [Required(ErrorMessage = "EmailID is required")]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Enter Valid EmailID.")]
        [DataType(DataType.EmailAddress)]
        public string emailId { get; set; }

        [Required(ErrorMessage = "status is required")]
        public Int16 status { get; set; }
    }
}
