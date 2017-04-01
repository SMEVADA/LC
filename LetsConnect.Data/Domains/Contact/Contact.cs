using LetsConnect.Data.Domains.common;
using LetsConnect.Web.Framework.ViewModels.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.Contact 
{
    [MetadataType(typeof(ContactViewModel))]
    public partial class Contact : BaseDateEntity
    {
        [Key]
        public long contactId { get; set; }
        public long customerId { get; set; }
        public string name { get; set; }
        public string emailId { get; set; }
        public string mobileNo { get; set; }
        public string website { get; set; }
        public Nullable<long> TotalRows { get; set; }
    }

    public partial class Contact
    {
        public string customerName { get; set; }
        
    }
}
