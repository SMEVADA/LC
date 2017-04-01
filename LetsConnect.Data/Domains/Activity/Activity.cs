using LetsConnect.Web.Framework.ViewModels.Activity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.Activity
{
    [MetadataType(typeof(ActivityViewModel))]
    public partial class Activity
    {
        [Key]
        public long activityId { get; set; }
        public Int16 activityType { get; set; }
        public string activity { get; set; }
        public long activityDate { get; set; }
        public Nullable<long> TotalRows { get; set; }
    }
}
