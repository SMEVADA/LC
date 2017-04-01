using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Web.Framework.ViewModels.Customer
{
    public partial class CustomerViewModel
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "EmailID is required")]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Enter Valid EmailID.")]
        [DataType(DataType.EmailAddress)]
        public string emailId { get; set; }

        [Required(ErrorMessage = "MobileNo is required")]
        [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Please Enter Valid MobileNo.")]
        public string mobileNo { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }

        [Required(ErrorMessage = "Latitude is required")]
        public double latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        public double longitude { get; set; }

        [Required(ErrorMessage = "SocialId is required")]
        public string socialId { get; set; }

        [Required(ErrorMessage = "SocialType is required")]
        public int? socialType { get; set; }

        //[Required(ErrorMessage = "UserName is required")]
        //public bool isActive { get; set; }

        //[Required(ErrorMessage = "UserName is required")]
        //public bool isDeleted { get; set; }
    }
}
