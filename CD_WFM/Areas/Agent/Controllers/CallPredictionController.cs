using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFM.Common;
using WFM.Controllers;
using WFM.Models;

namespace WFM.Areas.Agent.Controllers
{
    public class CallPredictionController : BaseController
    {
        // GET: CallPrediction
        public ActionResult Index()
        {
            GetShiftDDList("-1", false, true);
            GetAgentListWithShiftName("-1", false, true);
            return View();
        }


        public JsonResult Search_Ajax(string txtTargetDate, string checkType)
        {
            LineChart line = new LineChart();
            return this.Json(line, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LineChart(string txtTargetDate, int ddlShift, int Interval, int agentCount)
        {
            WFMDBDataContext db = new WFMDBDataContext();
            List<tblChart> lstCalls = db.uspWFMGetMinInboundCallPrediction(this.TenantID, txtTargetDate, txtTargetDate, ddlShift, Interval, 0, 0).ToList();
            LineChart line = new LineChart();
            List<Dataset> datasets = new List<Dataset>();
            Dataset inCallDs = new Dataset();
            string lables = "";
            int[] datas = new int[lstCalls.Count];
            int index = 0;
            foreach (var item in lstCalls)
            {
                lables += "," + item.dtCreatedCall.ToString("HH:mm");
                datas[index] = item.iCountOfInboundCall;
                index++;
            }

            inCallDs.data = datas;
            line.labels = lables.Substring(1).Split(',');

            inCallDs.fillColor = "rgba(151,187,205,0.2)";
            inCallDs.strokeColor = "rgba(151,187,205,1)";
            inCallDs.pointColor = "rgba(151,187,205,1)";
            inCallDs.pointStrokeColor = "#fff";
            inCallDs.label = "着信数";
            datasets.Add(inCallDs);
            line.datasets = datasets;
            HttpRuntime.Cache.Insert("chatLine", lstCalls);
            return this.Json(line, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LineChartForAgent(string txtTargetDate, int ddlShift, int Interval, int agentCount)
        {
            var cacheValue = HttpRuntime.Cache.Get("chatLine");
            List<tblChart> lstCalls = null;
            if (cacheValue == null)
            {
                WFMDBDataContext db = new WFMDBDataContext();
                lstCalls = db.uspWFMGetMinInboundCall(this.TenantID, txtTargetDate, txtTargetDate, ddlShift, Interval, 0, 0).ToList();
            }
            else
            {
                lstCalls = cacheValue as List<tblChart>;
            }

            LineChart line = new LineChart();
            List<Dataset> datasets = new List<Dataset>();
            Dataset inCallDs = new Dataset();
            string lables = "";
            int[] datas = new int[lstCalls.Count];
            int index = 0;
            foreach (var item in lstCalls)
            {
                lables += "," + item.dtCreatedCall.ToString("HH:mm");
                datas[index] = item.iCountOfInboundCall - agentCount;
                index++;
            }

            inCallDs.data = datas;
            line.labels = lables.Substring(1).Split(',');

            inCallDs.fillColor = "rgba(151,187,205,0.2)";
            inCallDs.strokeColor = "rgba(151,187,205,1)";
            inCallDs.pointColor = "rgba(151,187,205,1)";
            inCallDs.pointStrokeColor = "#fff";
            inCallDs.label = "着信数";
            datasets.Add(inCallDs);
            line.datasets = datasets;

            return this.Json(line, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DoCallPredict(string txtTargetDate, int dataSouceType, double factor1,double factor2)
        {
            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                db.uspWFMcreateCallPrediction(dataSouceType, this.TenantID, DateTime.Parse(txtTargetDate), factor1, factor2);
            }
            return this.Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
        }
    }
}