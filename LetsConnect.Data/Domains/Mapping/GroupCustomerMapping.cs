﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.Mapping
{
   public partial class GroupCustomerMapping
    {
        public long groupId { get; set; }
        public long customerId { get; set; }
    }
}
