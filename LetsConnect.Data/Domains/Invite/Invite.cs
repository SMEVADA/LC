using LetsConnect.Web.Framework.ViewModels.Invite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.Invite
{
    [MetadataType(typeof(InviteViewModel))]
    public partial class Invite
    {
        public long customerId { get; set; }
        public string mobileNo { get; set; }
        public string emailId { get; set; }
        public Int16 status { get; set; }
        public Nullable<long> TotalRows { get; set; }

    }
}
