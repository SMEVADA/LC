using LetsConnect.Data.Domains.Administator;
using LetsConnect.Services.Interface.ICommon;
using LetsConnect.Services.Repository.RActivity;
using LetsConnect.Services.Repository.RCommon;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using static LetsConnect.Core.Generic.Enums;

namespace LetsConnect.Controllers
{
    public class CommonController : Controller
    {

        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string Login(string emailAddress, string password)
        {
            try
            {
                ICommonRepository repository = new CommonRepository();
                Administator administator = ((ICommonRepository)repository).Login(emailAddress, password);
                if (administator.administratorId != 0)
                {
                    //    claim is added only one time so written here instead of at common repository
                    Session["CurrentUserId"] = Convert.ToString(administator.administratorId);
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Sid, Convert.ToString(administator.administratorId)));
                    claims.Add(new Claim(ClaimTypes.Name, Convert.ToString(administator.fullName)));
                    claims.Add(new Claim(ClaimTypes.Email, Convert.ToString(administator.emailID)));

                    var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    Thread.CurrentPrincipal = claimsPrincipal;
                    var ctx = Request.GetOwinContext();
                    var authenticationManager = ctx.Authentication;
                    authenticationManager.SignIn(identity);
                    return "success";
                }
                return "false";
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
                return "error";
            }
        }

        [HttpGet]
        public string ForGetPassword(string emailAddress)
        {
            try
            {
                ICommonRepository repository = new CommonRepository();
                Administator administator = ((ICommonRepository)repository).Forgetpassword(emailAddress);
                if (administator.administratorId != 0)
                {
                    return "success";
                }
                return "false";
            }
            catch (Exception ex)
            {
                new ActivityRepository().AddNew(Convert.ToInt16(EnumactivityType.Error), ex, LetsConnect.Core.Generic.EpochTimeExtensions.ToEpochTime(DateTime.Now));
                return "error";
            }
        }


        [HttpGet]
        public bool LogOut()
        {
            try
            {
                Request.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);

                Session.Clear();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}