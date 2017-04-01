using LetsConnect.Services.Interface.ICommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsConnect.Data.Domains.Administator;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Threading;

namespace LetsConnect.Services.Repository.RCommon
{
   public partial class ClaimsRepository : IClaimsRepository
    {
       // public int AddClaims(Administator administator)
       // {
            //try
            //{
            //    var claims = new List<Claim>();
            //    claims.Add(new Claim(ClaimTypes.Sid, Convert.ToString(administator.administratorId)));
            //    claims.Add(new Claim(ClaimTypes.Name, Convert.ToString(administator.fullName)));
            //    claims.Add(new Claim(ClaimTypes.Email, Convert.ToString(administator.emailID)));

            //    var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            //    var claimsPrincipal = new ClaimsPrincipal(identity);
            //    Thread.CurrentPrincipal = claimsPrincipal;
            //    var ctx = Request.GetOwinContext();
            //    var authenticationManager = ctx.Authentication;
            //    authenticationManager.SignIn(identity);
            //    return 1;
            //}
            //catch(Exception ex)
            //{
            //}
       // }
    }
}
