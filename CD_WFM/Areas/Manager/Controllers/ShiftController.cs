using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFM.Areas.Manager.Models;
using WFM.Common;
using WFM.Controllers;

namespace WFM.Areas.Manager.Controllers
{
    public class ShiftController : BaseController
    {
        // GET: Shift
        public ActionResult Index()
        {
            WFMDBDataContext db = new WFMDBDataContext();
            List<uspWFMGetShiftResult> lstShift = db.uspWFMGetShift(this.TenantID, null).ToList();
            return View(lstShift);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ShiftModel shift)
        {
            if (ModelState.IsValid)
            {
                WFMDBDataContext db = new WFMDBDataContext();
                db.uspWFMInsertShift(shift.ShiftName, this.TenantID, shift.StartTime, shift.EndTime, this.TenantSpecialFlag);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            WFMDBDataContext db = new WFMDBDataContext();
            List<uspWFMGetSingleShiftResult> lstShift = db.uspWFMGetSingleShift(id).ToList();
            if (lstShift.Count > 0)
            {
                ShiftModel shift = new ShiftModel();
                shift.ShiftID = lstShift[0].iShiftID;
                shift.ShiftName = lstShift[0].vShiftName;
                shift.StartTime = lstShift[0].vStartTime;
                shift.EndTime = lstShift[0].vEndTime;
                return View(shift);
            }
            else
            {
                return RedirectToAction("Message", "Error", new { Error = "シフト在しません。" });
            }

        }

        [HttpPost]
        public ActionResult Edit(int id, ShiftModel shift)
        {
            if (ModelState.IsValid)
            {
                if (id != shift.ShiftID)
                {
                    return RedirectToAction("Message", "Error", new { Error = "シフト在しません。" });
                }
            }
            WFMDBDataContext db = new WFMDBDataContext();
            db.uspWFMUpdateShift(id, shift.ShiftName, shift.StartTime, shift.EndTime);
            return RedirectToAction("Index");
        }
    }
}