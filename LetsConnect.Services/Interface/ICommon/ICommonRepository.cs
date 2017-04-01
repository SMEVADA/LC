using LetsConnect.Data.Domains.Administator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.ICommon
{
   public interface ICommonRepository
    {
        Administator Login(string EmailAddress, string password);
        Administator Forgetpassword(string EmailAddress);
    }
}
