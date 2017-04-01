using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Web.Framework.ViewModels.Customer
{
    public partial class CustomerDetaileViewModel
    {
        [Required(ErrorMessage = "Customer is required")]
        public long customerId { get; set; }
    }
}
