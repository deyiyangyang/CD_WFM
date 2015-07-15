using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFM.Controllers;
using WFM.Models;

namespace WFM.Areas.Agent.Controllers
{
    public class CallPredictionController : BaseController
    {
        // GET: CallPrediction
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Search_Ajax(string txtTargetdate, string checkType)
        {
            LineChart line = new LineChart();
            return this.Json(line, JsonRequestBehavior.AllowGet);
        }
    }
}