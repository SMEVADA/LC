using LetsConnect.Data.Domains.Contact;
using LetsConnect.Services.Interface.IContact;
using LetsConnect.Services.Repository.RActivity;
using LetsConnect.Services.Repository.RContact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static LetsConnect.Core.Generic.Enums;

namespace LetsConnect.Areas.Admin.Controllers
{
    public class ContactAPIController : ApiController
    {
        IContactRepository contactRepository = new ContactRepository();

        [Route("api/ContactAPI/Add")]
        [HttpPost]
        public int Add(Contact contact)
        {
            int returnValue = 0;
            try
            {
                returnValue = ((IContactRepository)contactRepository).AddNew(contact);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        [Route("api/ContactAPI/GetAll")]
        [HttpGet]
        public List<Contact> GetAll(int PageNumber = 1, int PageSize = 10)
        {
            List<Contact> NewContactList = new List<Contact>();
            try
            {
                NewContactList = ((IContactRepository)contactRepository).GetAll(PageNumber, PageSize);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return NewContactList;
        }

        [Route("api/ContactAPI/GetById")]
        [HttpGet]
        public Contact GetById(int Id)
        {
            Contact NewContact = new Contact();
            try
            {
                NewContact = ((IContactRepository)contactRepository).GetById(Id);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return NewContact;
        }

        [Route("api/ContactAPI/Update")]
        [HttpPost]
        public int Update(Contact contact)
        {
            int returnValue = 0;
            try
            {
                returnValue = ((IContactRepository)contactRepository).Update(contact);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        [Route("api/ContactAPI/Delete")]
        [HttpGet]
        public bool Delete(int Id)
        {
            bool returnValue = false;
            try
            {
                returnValue = ((IContactRepository)contactRepository).Delete(Id);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        [Route("api/ContactAPI/GetAllByParameters")]
        [HttpGet]
        public List<Contact> GetAllByParameters(int PageNumber = 1, int PageSize = 10, string Name = null, string MobileNo = null, string EmailId = null, long CustomerId = 0)
        {
            List<Contact> NewContactList = new List<Contact>();
            try
            {
                NewContactList = ((IContactRepository)contactRepository).GetAllByParameters(PageNumber, PageSize, Name, MobileNo, EmailId, CustomerId);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return NewContactList;
        }
    }
}
