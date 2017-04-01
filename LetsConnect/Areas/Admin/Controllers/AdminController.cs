using LetsConnect.Data.Domains.Administator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LetsConnect.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult AdminList()
        {
            return View();
        }

        public ActionResult PemissionList(long id = 0)
        {
            return View();
        }

        public ActionResult InquiryList()
        {
            return View();
        }

    }
}