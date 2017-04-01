using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Web.Framework.ViewModels.Rating
{
    public partial class RatingViewModel
    {
        [Required(ErrorMessage = "RatedBy is required")]
        public long ratedBy { get; set; }

        [Required(ErrorMessage = "RatedTo is required")]
        public long ratedTo { get; set; }

        //[Required(ErrorMessage = "Rating is required")]
        //public float rating { get; set; }

        //[Required(ErrorMessage = "Comment is required")]
        //public string comment { get; set; }
    }
}
