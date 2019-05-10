using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Application;

namespace Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (!this.currentUser.UserID.ValidZero())
                return RedirectToAction("Index", "Login", new { area = "Admin" });
            var menuLogic = new MenuLogic();
            var menu = menuLogic.CreateMenu(0);
            ViewData["MenuHtml"] = menu;
            return View(currentUser);
        }
        public ActionResult Main()
        {
            return View();
        }

    }
}