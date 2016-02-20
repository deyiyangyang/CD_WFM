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
            return View();
        }


        public JsonResult Search_Ajax(string txtBaseDate, string checkType)
        {
            LineChart line = new LineChart();
            return this.Json(line, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LineChart(string txtBaseDate, int ddlShift, int Interval)
        {
            WFMDBDataContext db = new WFMDBDataContext();
            List<tblChart> lstCalls = db.uspWFMGetMinInboundCall(this.TenantID, txtBaseDate, txtBaseDate, ddlShift, Interval, 0, 0).ToList();
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

            return this.Json(line, JsonRequestBehavior.AllowGet);
        }
    }
}