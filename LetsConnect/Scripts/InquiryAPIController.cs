using LetsConnect.Data.Domains.Inquiry;
using LetsConnect.Services.Interface.IInquiry;
using LetsConnect.Services.Repository.RActivity;
using LetsConnect.Services.Repository.RInquiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static LetsConnect.Core.Generic.Enums;

namespace LetsConnect.Controllers
{
    public class InquiryAPIController : ApiController
    {
        IInquiryRepository repository = new InquiryRepository();

        [HttpGet]
        public int AddInquiry(string Name, string mobile, string Message, string Email, string Address)
        {
            int result = 0;
            try
            {
                Inquiry inquiryModel = new Inquiry();
                inquiryModel.Address = Address;
                inquiryModel.Email = Email;
                inquiryModel.Message = Message;
                inquiryModel.Mobile = mobile;
                inquiryModel.Name = Name;
                result = ((IInquiryRepository)repository).AddInquiry(inquiryModel);
              
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return result;
        }

        [HttpGet]
        public AllInquiryModelData GetAllInquiry()
        {
            AllInquiryModelData result = new AllInquiryModelData();
            try
            {
                int readdatacount = 0;
                int unreaddatacount = 0;
                List<Inquiry> resultModel = ((IInquiryRepository)repository).GetAllInquiry();
                readdatacount = resultModel.Count(i => i.Isread == true);
                unreaddatacount = resultModel.Count(i => i.Isread == false);
                result.status = "success";
                result.Inquiry = resultModel;
                result.readdatacount = readdatacount;
                result.unreaddatacount = unreaddatacount;
                return result;
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return result;
        }


        [HttpGet]
        public Inquiry GetInquiryByInquityId(int inquiryID)
        {
            Inquiry result = new Inquiry();
            try
            {
                result = ((IInquiryRepository)repository).GetInquiryByInquityId(inquiryID);
            }

            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return result;
        }
    }
}
