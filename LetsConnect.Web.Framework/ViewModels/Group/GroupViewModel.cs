using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Web.Framework.ViewModels.Group
{
    public partial class GroupViewModel
    {
        [Required(ErrorMessage = "Group is required")]
        public string groupName { get; set; }
    }
}
