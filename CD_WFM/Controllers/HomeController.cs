using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFM.Common;
using WFM.Models;

namespace WFM.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LineChart()
        {
            WFMDBDataContext db = new WFMDBDataContext();
            List<uspWFMGetOneWeekInboundCallResult> lstCalls = db.uspWFMGetOneWeekInboundCall(this.TenantID).ToList();
            LineChart line = new LineChart();
            List<Dataset> datasets = new List<Dataset>();
            Dataset ds = new Dataset();
            string lables = "";
            int[] datas = new int[7];
            int index = 0;
            foreach (var item in lstCalls)
            {
                lables += "," + item.dtCreatedCall.Value.ToString("MM/dd")+"("+AppConst.Const_Weekday[Convert.ToInt16(item.dtCreatedCall.Value.DayOfWeek)]+")";
                datas[index] = item.iCountOfInboundCall.Value;
                index++;
            }

            if (string.IsNullOrEmpty(lables))
            {
                lables += DateTime.Now.AddDays(-7).ToString("MM/dd") + "(" + AppConst.Const_Weekday[Convert.ToInt16(DateTime.Now.AddDays(-7).DayOfWeek)] + ")";
                lables += "," + DateTime.Now.AddDays(-6).ToString("MM/dd") + "(" + AppConst.Const_Weekday[Convert.ToInt16(DateTime.Now.AddDays(-6).DayOfWeek)] + ")";
                lables += "," + DateTime.Now.AddDays(-5).ToString("MM/dd") + "(" + AppConst.Const_Weekday[Convert.ToInt16(DateTime.Now.AddDays(-5).DayOfWeek)] + ")";
                lables += "," + DateTime.Now.AddDays(-4).ToString("MM/dd") + "(" + AppConst.Const_Weekday[Convert.ToInt16(DateTime.Now.AddDays(-4).DayOfWeek)] + ")";
                lables += "," + DateTime.Now.AddDays(-3).ToString("MM/dd") + "(" + AppConst.Const_Weekday[Convert.ToInt16(DateTime.Now.AddDays(-3).DayOfWeek)] + ")";
                lables += "," + DateTime.Now.AddDays(-2).ToString("MM/dd") + "(" + AppConst.Const_Weekday[Convert.ToInt16(DateTime.Now.AddDays(-2).DayOfWeek)] + ")";
                lables += "," + DateTime.Now.AddDays(-1).ToString("MM/dd") + "(" + AppConst.Const_Weekday[Convert.ToInt16(DateTime.Now.AddDays(-1).DayOfWeek)] + ")";
            }
            ds.data = datas;
            line.labels = lables.Substring(1).Split(',');

            ds.fillColor = "rgba(151,187,205,0.2)";
            ds.strokeColor = "rgba(151,187,205,1)";
            ds.pointColor = "rgba(151,187,205,1)";
            ds.pointStrokeColor = "#fff";
            datasets.Add(ds);
            line.datasets = datasets;

            return this.Json(line, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}