using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WFM.Controllers
{
    [HandleError]
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Error404(string error)
        {
            return View();
        }

        public ActionResult General(string error)
        {
            error = error.Replace("\r\n", "<br/>");
            ViewBag.Message = error;

            return View();
        }

        public ActionResult Message(string Error)
        {
            ViewBag.Message = Error;
            return View();
        }
    }
}