using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.Common
{
   public class CustomUserClass
    {
        public long administratorId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
        public string emailID { get; set; }
        public string mobileNo { get; set; }
        public bool isActive { get; set; }
        public Int16 administratorType { get; set; }
        public Nullable<long> TotalRows { get; set; }

    }
}
