using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.common
{
   public partial class BaseDateEntity
    {
        public long createdDate { get; set; }
        public long? modifiedDate { get; set; }
    }
}
