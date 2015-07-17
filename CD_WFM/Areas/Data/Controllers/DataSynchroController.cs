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
            string updatedTime = DateTime.Now.ToString(AppConst.Const_Format_YMD)+" 00:00:00";
            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                List<uspWFMGetDataSynchroResult> lst = db.uspWFMGetDataSynchro(this.TenantID).ToList();
                if(lst.Count>0)
                {
                    updatedTime = lst[0].dtUpdated.Value.ToString(AppConst.Const_Format_YMDHMS);
                }
            }
            ViewBag.UpdatedTime = updatedTime;
            return View();
        }
    }
}