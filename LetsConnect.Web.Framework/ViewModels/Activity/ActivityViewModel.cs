using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Web.Framework.ViewModels.Activity
{
    public partial class ActivityViewModel
    {
        [Required(ErrorMessage = "Activity Type is required")]
        public Int16 activityType { get; set; }

        [Required(ErrorMessage = "Activity is required")]
        public string activity { get; set; }

        [Required(ErrorMessage = "ActivityDate Type is required")]
        public long activityDate { get; set; }
    }
}
