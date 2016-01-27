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
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            string updatedTime = DateTime.Now.ToString(AppConst.Const_Format_YMD) + " 00:00:00";
            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                List<uspWFMGetDataSynchroResult> lst = db.uspWFMGetDataSynchro(this.TenantID).ToList();
                if (lst.Count > 0)
                {
                    updatedTime = lst[0].dtUpdated.Value.ToString(AppConst.Const_Format_YMDHMS);
                }
            }
            ViewBag.UpdatedTime = updatedTime;
            return View();
        }

        public JsonResult LineChart(string type)
        {
            WFMDBDataContext db = new WFMDBDataContext();
            List<tblChart> lstCalls = db.uspWFMGetHistoryInboundCallForChart(this.TenantID, type).ToList();
            LineChart line = new LineChart();
            List<Dataset> datasets = new List<Dataset>();
            Dataset ds = new Dataset();
            string lables = "";
            int[] datas = new int[lstCalls.Count];
            int index = 0;
            foreach (var item in lstCalls)
            {
                lables += "," + item.dtCreatedCall.ToString("MM/dd")+"("+AppConst.Const_Weekday[Convert.ToInt16(item.dtCreatedCall.DayOfWeek)]+")";
                datas[index] = item.iCountOfInboundCall;
                index++;
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