using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFM.Common;
using WFM.Controllers;

namespace WFM.Areas.Data.Controllers
{
    public class DataSynchroController : BaseController
    {
        // GET: Data/DataSynchro
        public ActionResult Index()
        {
            GetDataSynchroDate();
            return View();
        }

        public ActionResult Update_Ajax()
        {
            try
            {
                //using (WFMDBDataContext db = new WFMDBDataContext())
                using (WFMDBDataContext db = new WFMDBDataContext(string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["SpecialConnection"].ConnectionString, DBServer)))
                {
                    db.uspWFMDataSynchro(this.TenantID, null, null);
                    //db.uspRptForAgentDetailV3OneDay(this.TenantID, null, null);
                    //db.uspRptForCallDetailV3OneDay(this.TenantID, null, null);
                    
                    //db.uspWFMUpdateOrInsertDataSynchro(this.TenantID);
                }

                return Json(new { StatusCode = AppConst.Enum_JsonStatus.Edit }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog("DataSynchroController:Update_Ajax system error:"+ex.Message+ex.StackTrace);
                return Json(new { StatusCode = AppConst.Enum_JsonStatus.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// get info from report.txt
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateReportFileInfo_Ajax()
        {
            try
            {
                string reportPath = "\\cte1repot\\Report.txt";
                string[] lines = System.IO.File.ReadAllLines(Server.MapPath(reportPath));
                using (WFMDBDataContext db = new WFMDBDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    foreach(var line in lines)
                    {
                        var report = line.Trim();
                        if(string.IsNullOrEmpty(report) || !report.Contains("="))
                        {
                            continue;
                        }
                        var reportId = report.Split('=')[0];
                        var reportName = report.Split('=')[1];
                    }
                    db.uspWFMDataSynchro(this.TenantID, null, null);
                }
                return Json(new { StatusCode = AppConst.Enum_JsonStatus.Edit }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog("DataSynchroController:Update_Ajax system error:" + ex.Message + ex.StackTrace);
                return Json(new { StatusCode = AppConst.Enum_JsonStatus.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}