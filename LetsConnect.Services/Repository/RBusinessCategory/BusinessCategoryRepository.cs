using LetsConnect.Services.Interface.IBusinessCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsConnect.Data.Domains.BusinessCategory;

namespace LetsConnect.Services.Repository.RBusinessCategory
{
    public class BusinessCategoryRepository : IBusinessCategoryRepository
    {
        public int AddNew(BusinessCategory businessCategory)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<BusinessCategory> GetAll(int PageNumber, int PageSize)
        {
            throw new NotImplementedException();
        }

        public BusinessCategory GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public int Update(BusinessCategory businessCategory)
        {
            throw new NotImplementedException();
        }
    }
}
