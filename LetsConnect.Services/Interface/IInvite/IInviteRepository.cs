using LetsConnect.Data.Domains.Invite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.IInvite
{
   public interface IInviteRepository
    {
        List<Invite> GetAll(int PageNumber, int PageSize);
        Invite GetById(int Id);
        int AddNew(Invite invite);
        int Update(Invite invite);
        bool Delete(int Id);
    }
}
