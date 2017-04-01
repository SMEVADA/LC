using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Data.Domains.Inquiry
{
  public partial class Inquiry
    {
        public int InquiryID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public bool? Isread { get; set; }
    }

    public partial class AllInquiryModelData
    {
        public List<Inquiry> Inquiry { get; set; }
        public long readdatacount { get; set; }
        public long unreaddatacount { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}
