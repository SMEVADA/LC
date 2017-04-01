using LetsConnect.Data.Domains.BusinessCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.IBusinessCategory
{
   public interface IBusinessCategoryRepository
    {
        List<BusinessCategory> GetAll(int PageNumber, int PageSize);
        BusinessCategory GetById(int Id);
        int AddNew(BusinessCategory businessCategory);
        int Update(BusinessCategory businessCategory);
        bool Delete(int Id);
    }
}
