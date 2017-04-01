using LetsConnect.Data.Domains.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.IContact
{
   public interface IContactRepository
    {
        List<Contact> GetAll(int PageNumber, int PageSize);
        Contact GetById(int Id);
        int AddNew(Contact contact);
        int Update(Contact contact);
        bool Delete(int Id);
        List<Contact> GetAllByParameters(int PageNumber, int PageSize, string Name, string MobileNo, string EmailId, long CustomerId);
    }
}
