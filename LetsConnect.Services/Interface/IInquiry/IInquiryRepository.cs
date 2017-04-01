using LetsConnect.Data.Domains.Inquiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.IInquiry
{
    public interface IInquiryRepository
    {
        int AddInquiry(Inquiry inquiryModel);
        List<Inquiry> GetAllInquiry();
        Inquiry GetInquiryByInquityId(int inquiryID);
    }
}
