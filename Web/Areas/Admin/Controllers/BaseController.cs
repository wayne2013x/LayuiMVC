using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        public JsonResult Success(object _object = null)
        {
            if (_object == null)
                return Json(new { status = 1 }, JsonRequestBehavior.DenyGet);
            return Json(_object, JsonRequestBehavior.DenyGet);
        }
    }
}