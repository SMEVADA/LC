using LetsConnect.Data.Domains.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.ICustomer
{
  public interface ICustomerRepository
    {
        List<Customer> GetAll(int PageNumber, int PageSize);
        Customer GetById(int Id);
        int AddNew(Customer customer);
        int Update(Customer customer);
        bool Delete(int Id);
        List<Customer> GetAllByParameters(int PageNumber = 1, int PageSize = 10, string Name = null, string MobileNo = null, string EmailId = null, string Address = null);
    }
}
