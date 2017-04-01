using LetsConnect.Services.Interface.IRating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsConnect.Data.Domains.Rating;

namespace LetsConnect.Services.Repository.RRating
{
    public partial class RatingRepository : IRatingRepository
    {
        public int AddNew(Data.Domains.Rating.Rating rating)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Data.Domains.Rating.Rating> GetAll(int PageNumber, int PageSize)
        {
            throw new NotImplementedException();
        }

        public Data.Domains.Rating.Rating GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public int Update(Data.Domains.Rating.Rating rating)
        {
            throw new NotImplementedException();
        }
    }
}
