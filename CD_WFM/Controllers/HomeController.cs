using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WFM.Common;
using WFM.Models;

namespace WFM.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            GetDataSynchroDate();
            return View();
        }

        public JsonResult LineChart(string type)
        {
            
            //WFMDBDataContext db = new WFMDBDataContext();
            WFMDBDataContext db = new WFMDBDataContext(string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["SpecialConnection"].ConnectionString, DBServer));
            List<tblChart> lstCalls = db.uspWFMGetHistoryInboundCallForChart(this.TenantID, type).ToList();
            LineChart line = new LineChart();
            List<Dataset> datasets = new List<Dataset>();
            Dataset inCallDs = new Dataset();
            Dataset inCompletedCallDs = new Dataset();
            string lables = "";
            int[] dataInCalls = new int[lstCalls.Count];
            int[] dataCompleteds = new int[lstCalls.Count];
            int index = 0;
            foreach (var item in lstCalls)
            {
                lables += "," + item.dtCreatedCall.ToString("MM/dd")+"("+AppConst.Const_Weekday[Convert.ToInt16(item.dtCreatedCall.DayOfWeek)]+")";
                dataInCalls[index] = item.iCountOfInboundCall;
                dataCompleteds[index] = item.iCountOfCompletedInCall;
                index++;
            }
            line.labels = lables.Substring(1).Split(',');

            inCallDs.data = dataInCalls;
            inCallDs.fillColor = "rgba(151,187,205,0.2)";
            inCallDs.strokeColor = "rgba(151,187,205,1)";
            inCallDs.pointColor = "rgba(151,187,205,1)";
            inCallDs.pointStrokeColor = "#fff";
            inCallDs.label = "総着信";


            inCompletedCallDs.data = dataCompleteds;
            inCompletedCallDs.fillColor = "rgba(220,220,220,0.5)";
            inCompletedCallDs.strokeColor = "rgba(220,220,220,1)";
            inCompletedCallDs.pointColor = "rgba(220,220,220,1)";
            inCompletedCallDs.pointStrokeColor = "#fff";
            inCompletedCallDs.label = "完了呼";

            datasets.Add(inCallDs);
            datasets.Add(inCompletedCallDs);
            line.datasets = datasets;

            return this.Json(line, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            try
            {
                System.Version version = Assembly.GetExecutingAssembly().GetName().Version;
                ViewBag.Version = version.Major + "." + version.Minor + "." + version.Build;
            }
            catch (Exception)
            {
                ViewBag.Version = "1.0.0";
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}