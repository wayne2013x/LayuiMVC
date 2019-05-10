using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Common;
using Entity;

namespace Web.Areas.Admin.Controllers
{
    public class LoginController : BaseController
    {
        AccountLogic accountLogic = new AccountLogic();
        public ActionResult Index()
        {
            BaseClass.SetSession("User", new Account());
            return View();
        }
        [HttpPost]
        public JsonResult Checked(string userName, string userPwd)
        {
            accountLogic.Checked(userName, userPwd);
            return Success(new
            {
                msg = "登录成功！",
                url = AppConfig.HomePageUrl
            });
             
            
            
        }

    }
}