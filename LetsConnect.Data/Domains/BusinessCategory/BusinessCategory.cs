using LetsConnect.Data.Domains.common;
using LetsConnect.Web.Framework.ViewModels.BusinessCategory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.BusinessCategory
{
    [MetadataType(typeof(BusinessCategoryViewModel))]
    public partial class BusinessCategory : BaseEntity
    {
        [Key]
        public long businessCategoryId { get; set; }
        public string businessCategoryName { get; set; }
        public bool isActive { get; set; }
        public Nullable<long> TotalRows { get; set; }
    }
}
