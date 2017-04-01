using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Web.Framework.ViewModels.Contact
{
    public partial class ContactViewModel
    {
        [Required(ErrorMessage = "Customer is required")]
        public long customerId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "MobileNo is required")]
        //[RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Please Enter Valid MobileNo.")]
        public string mobileNo { get; set; }
      
    }
}
