using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        protected Account currentUser = new Account();
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.currentUser = BaseClass.GetSession<Account>("User");
        }


        public JsonResult Success(object obj = null)
        {
            if (obj == null)
                return Json(new { status = 1 }, JsonRequestBehavior.DenyGet);
            return Json(obj, JsonRequestBehavior.DenyGet);
        }
    }
}