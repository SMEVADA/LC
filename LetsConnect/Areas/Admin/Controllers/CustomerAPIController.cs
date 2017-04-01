using LetsConnect.Data.Domains.Customer;
using LetsConnect.Services.Interface.ICustomer;
using LetsConnect.Services.Repository.RActivity;
using LetsConnect.Services.Repository.RCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static LetsConnect.Core.Generic.Enums;

namespace LetsConnect.Areas.Admin.Controllers
{
    public class CustomerAPIController : ApiController
    {
        ICustomerRepository customerRepository = new CustomerRepository();

        [Route("api/CustomerAPI/Add")]
        [HttpPost]
        public int Add(Customer customer)
        {
            int returnValue = 0;
            try
            {
                returnValue = ((ICustomerRepository)customerRepository).AddNew(customer);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        [Route("api/CustomerAPI/GetAll")]
        [HttpGet]
        public List<Customer> GetAll(int PageNumber =1,int PageSize =10)
        {
            List<Customer> NewCustomerList = new List<Customer>();
            try
            {
                NewCustomerList = ((ICustomerRepository)customerRepository).GetAll(PageNumber, PageSize);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return NewCustomerList;
        }

        [Route("api/CustomerAPI/GetById")]
        [HttpGet]
        public Customer GetById(int Id)
        {
            Customer NewCustomer = new Customer();
            try
            {
                NewCustomer = ((ICustomerRepository)customerRepository).GetById(Id);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return NewCustomer;
        }

        [Route("api/CustomerAPI/Update")]
        [HttpPost]
        public int Update(Customer customer)
        {
            int returnValue = 0;
            try
            {
                returnValue = ((ICustomerRepository)customerRepository).Update(customer);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        [Route("api/CustomerAPI/Delete")]
        [HttpGet]
        public bool Delete(int Id)
        {
            bool returnValue = false;
            try
            {
                returnValue = ((ICustomerRepository)customerRepository).Delete(Id);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return returnValue;
        }

        [Route("api/CustomerAPI/GetAllByParameters")]
        [HttpGet]
        public List<Customer> GetAllByParameters(int PageNumber = 1, int PageSize = 10,string Name =null,string MobileNo=null,string EmailId =null,string Address =null)
        {
            List<Customer> NewCustomerList = new List<Customer>();
            try
            {
                NewCustomerList = ((ICustomerRepository)customerRepository).GetAllByParameters(PageNumber, PageSize, Name,MobileNo,EmailId,Address);
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
            }
            return NewCustomerList;
        }

    }
}
