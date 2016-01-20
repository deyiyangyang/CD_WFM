using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFM.Controllers;

namespace WFM.Areas.Report.Controllers
{
    public class AgentReportController : BaseController
    {
        // GET: Report/Agent
        public ActionResult Index()
        {
            return View();
        }
    }
}