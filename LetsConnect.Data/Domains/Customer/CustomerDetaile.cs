using LetsConnect.Data.Domains.common;
using LetsConnect.Web.Framework.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.Customer
{
    [MetadataType(typeof(CustomerDetaileViewModel))]
    public partial  class CustomerDetaile : BaseDateEntity
    {
        [Key]
        public long customerDetaileId  { get; set; }
        public long customerId { get; set; }
        public string need { get; set; }
        public string provide { get; set; }
        public long businessCategoryId { get; set; }
        public Nullable<long> TotalRows { get; set; }

    }
}
