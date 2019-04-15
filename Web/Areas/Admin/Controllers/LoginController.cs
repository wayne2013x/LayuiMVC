using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Common;

namespace Web.Areas.Admin.Controllers
{
    public class LoginController : BaseController
    {
        AccountLogic accountLogic = new AccountLogic();
        // GET: Admin/Login
        public ActionResult Index()
        {
            //BaseClass.SetSession("")
            return View();
        }
        [HttpPost]
        public JsonResult Checked(string userName, string userPwd)
        {
            accountLogic.Checked(userName, userPwd);
            return Success(new
            {
                msg = "登录成功！",
                url = AppConfig.HomePageUrl// + "#!%u9996%u9875#!/Admin/Home/Index/"
            });
            //return RedirectToAction("Index", "Home", new { area = "Admin" });
            //return Redirect("Admin/Home");
             
            
            
        }

    }
}