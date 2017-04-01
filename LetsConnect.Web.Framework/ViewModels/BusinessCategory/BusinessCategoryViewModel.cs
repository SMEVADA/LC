using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Web.Framework.ViewModels.BusinessCategory
{
    public partial class BusinessCategoryViewModel
    {
        [Required(ErrorMessage = "Business Category Name is required")]
        public string businessCategoryName { get; set; }

        //[Required(ErrorMessage = "IsActive is required")]
        //public bool isActive { get; set; }
    }
}
