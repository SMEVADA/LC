using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.Mapping
{
  public partial  class CustomerMapping
    {
        public long customerId { get; set; }
        public long lastLogin { get; set; }
        public Int16 currentStatus { get; set; }
    }
}
