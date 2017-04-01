using LetsConnect.Data.Domains.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.IRating
{
   public interface IRatingRepository
    {
        List<Rating> GetAll(int PageNumber, int PageSize);
        Rating GetById(int Id);
        int AddNew(Rating rating);
        int Update(Rating rating);
        bool Delete(int Id);
    }
}
