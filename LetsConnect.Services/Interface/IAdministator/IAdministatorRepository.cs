using LetsConnect.Data.Domains.Administator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.IAdministator
{
   public interface IAdministatorRepository
    {
        List<Administator> GetAll(int PageNumber, int PageSize);
        Administator GetById(int Id);
        Administator Add(Administator administator);
        Administator Update(Administator administator);
        bool Delete(int Id);
    }
}
