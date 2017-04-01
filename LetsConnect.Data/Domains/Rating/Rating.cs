using LetsConnect.Data.Domains.common;
using LetsConnect.Web.Framework.ViewModels.Rating;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.Rating
{
    [MetadataType(typeof(RatingViewModel))]
    public partial class Rating : BaseDateEntity
    {
        public long ratedBy { get; set; }
        public long ratedTo { get; set; }
        public float rating { get; set; }
        public string comment { get; set; }
        public Nullable<long> TotalRows { get; set; }
    }
}
