using Stores.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stores.Controllers
{
    public class HomeController : Controller
    {
        private Security _security = new Security();

        // GET: Home
        public ActionResult Index()
        {
            bool s = _security.AsUser();
            if (s ==true )
            {
                return View();

            }
            return RedirectToAction("Login", "Employee");

        }
    }
}