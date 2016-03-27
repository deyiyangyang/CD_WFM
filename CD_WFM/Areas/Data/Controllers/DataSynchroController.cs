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
                using (WFMDBDataContext db = new WFMDBDataContext())
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
    }
}