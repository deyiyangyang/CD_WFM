using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFM.Common;
using WFM.Controllers;
using WFM.Models;

namespace WFM.Areas.Data.Controllers
{
    public class ChartController : BaseController
    {
        // GET: Data/Chart
        public ActionResult PredictionChart()
        {
            GetShiftDDList("-1", false, true);
            GetSkillGroupListNoWithSkillAgregationForDDL("",true);
            GetSkillAgregationListForDDL("", true);
            return View();
        }

        public JsonResult LineChart(string date,int shiftId,int skillGroupid,int skillAggregationId)
        {
            WFMDBDataContext db = new WFMDBDataContext();
            List<tblChart> lstCalls = db.uspWFMGetPredictionAndActualInboundCallForChart(this.TenantID, date, shiftId, skillGroupid, skillAggregationId).ToList();
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
                lables += "," + item.dtCreatedCall.ToString("HH:mm");
                dataInCalls[index] = item.iCountOfInboundCall;
                dataCompleteds[index] = item.iCountOfCompletedInCall;
                index++;
            }
            if (!string.IsNullOrEmpty(lables))
                line.labels = lables.Substring(1).Split(',');

            inCallDs.data = dataInCalls;
            inCallDs.fillColor = "rgba(151,187,205,0.2)";
            inCallDs.strokeColor = "rgba(151,187,205,1)";
            inCallDs.pointColor = "rgba(151,187,205,1)";
            inCallDs.pointStrokeColor = "#fff";
            inCallDs.label = "実績";


            inCompletedCallDs.data = dataCompleteds;
            inCompletedCallDs.fillColor = "rgba(220,220,220,0.5)";
            inCompletedCallDs.strokeColor = "rgba(220,220,220,1)";
            inCompletedCallDs.pointColor = "rgba(220,220,220,1)";
            inCompletedCallDs.pointStrokeColor = "#fff";
            inCompletedCallDs.label = "予測";

            datasets.Add(inCallDs);
            datasets.Add(inCompletedCallDs);
            line.datasets = datasets;

            return this.Json(line, JsonRequestBehavior.AllowGet);
        }

    }
}