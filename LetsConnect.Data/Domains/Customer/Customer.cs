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
    [MetadataType(typeof(CustomerViewModel))]
    public partial class Customer : BaseDateEntity
    {
        [Key]
        public long customerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailId { get; set; }
        public string mobileNo { get; set; }
        public string address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string socialId { get; set; }
        public int? socialType { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public Nullable<long> TotalRows { get; set; }
    }
}
