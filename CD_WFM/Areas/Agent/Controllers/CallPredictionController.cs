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
        public ActionResult Index(int? currentShiftID)
        {
            if (currentShiftID.HasValue)
            {
                GetShiftDDList(currentShiftID.Value.ToString(), false, true);
                WFMDBDataContext db = new WFMDBDataContext();
                List<uspWFMGetSingleShiftResult> lstShift = db.uspWFMGetSingleShift(currentShiftID.Value).ToList();
                GetAgentListWithShiftNameForOneDay(DateTime.Parse(DateTime.Now.AddDays(1).ToString(AppConst.Const_Format_YMD)), lstShift[0].vShiftName, false, true);
            }
                
            else
            {
                GetShiftDDList("-1", false, true);
                var currentShiftId = int.Parse((ViewData["lstShift"] as List<SelectListItem>)[0].Value);
                WFMDBDataContext db = new WFMDBDataContext();
                List<uspWFMGetSingleShiftResult> lstShift = db.uspWFMGetSingleShift(currentShiftId).ToList();
                GetAgentListWithShiftNameForOneDay(DateTime.Parse(DateTime.Now.AddDays(1).ToString(AppConst.Const_Format_YMD)), lstShift[0].vShiftName, false, true);
            }
                


            
            //GetAgentListWithShiftName("-1", false, true);
            return View();
        }


        public JsonResult UpdateAgentShift(string txtTargetDate, int ddlShift, string agentShift)
        {
            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                db.uspWFMInsertOrUpdateShiftAgentSpecial(agentShift, DateTime.Parse(txtTargetDate), DateTime.Parse(txtTargetDate), ddlShift);
            }
            return this.Json(new { status="ok"}, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult LineChart(string txtTargetDate, int ddlShift, int Interval, int agentCount)
        //{
        //    WFMDBDataContext db = new WFMDBDataContext();
        //    List<tblChart> lstCalls = db.uspWFMGetMinInboundCallPrediction(this.TenantID, txtTargetDate, txtTargetDate, ddlShift, Interval, 0, 0).ToList();
        //    LineChart line = new LineChart();
        //    List<Dataset> datasets = new List<Dataset>();
        //    Dataset inCallDs = new Dataset();
        //    string lables = "";
        //    int[] datas = new int[lstCalls.Count];
        //    int index = 0;
        //    foreach (var item in lstCalls)
        //    {
        //        lables += "," + item.dtCreatedCall.ToString("HH:mm");
        //        datas[index] = item.iCountOfInboundCall;
        //        index++;
        //    }

        //    inCallDs.data = datas;
        //    line.labels = lables.Substring(1).Split(',');

        //    inCallDs.fillColor = "rgba(151,187,205,0.2)";
        //    inCallDs.strokeColor = "rgba(151,187,205,1)";
        //    inCallDs.pointColor = "rgba(151,187,205,1)";
        //    inCallDs.pointStrokeColor = "#fff";
        //    inCallDs.label = "予測着信数";
        //    datasets.Add(inCallDs);
        //    line.datasets = datasets;
        //    HttpRuntime.Cache.Insert("chatLine", lstCalls);
        //    return this.Json(line, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult LineChartForAgent(string txtTargetDate, int ddlShift, int Interval, int agentCount,bool needRefresh)
        {
            var cacheValue = HttpRuntime.Cache.Get("chatLine");
            List<tblChart> lstCalls = null;
            if (cacheValue == null || needRefresh)
            {
                WFMDBDataContext db = new WFMDBDataContext();
                lstCalls = db.uspWFMGetMinInboundCallPrediction(this.TenantID, txtTargetDate, txtTargetDate, ddlShift, Interval, 0, 0).ToList();
            }
            else
            {
                lstCalls = cacheValue as List<tblChart>;
            }

            LineChart line = new LineChart();
            List<Dataset> datasets = new List<Dataset>();
            Dataset inCallDs = new Dataset();
            Dataset zero = new Dataset();
            string lables = "";
            int[] datas = new int[lstCalls.Count];
            int[] zeroDatas = new int[lstCalls.Count];
            int index = 0;
            foreach (var item in lstCalls)
            {
                lables += "," + item.dtCreatedCall.ToString("HH:mm");
                datas[index] = item.iCountOfInboundCall - agentCount;
                zeroDatas[index] = 0;
                index++;
            }

            inCallDs.data = datas;
            line.labels = lables.Substring(1).Split(',');

            inCallDs.fillColor = "rgba(151,187,205,0.2)";
            inCallDs.strokeColor = "rgba(151,187,205,1)";
            inCallDs.pointColor = "rgba(151,187,205,1)";
            inCallDs.pointStrokeColor = "#fff";
            inCallDs.label = "予測着信数";

            zero.data = zeroDatas;
            zero.fillColor = "rgba(255,0,0,1)";
            zero.strokeColor = "rgba(255,0,0,1)";
            zero.pointColor = "rgba(255,0,0,1)";
            zero.pointStrokeColor = "#FF0";
            zero.label = "基准";
            datasets.Add(inCallDs);
            datasets.Add(zero);
            line.datasets = datasets;
            HttpRuntime.Cache.Insert("chatLine", lstCalls);
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