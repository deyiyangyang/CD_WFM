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
    public class SkillAgregationController : BaseController
    {
        // GET: SkillAgregation
        public ActionResult Index()
        {
            List<uspWFMGetAggregationResult> lstSkillAgregation = new List<uspWFMGetAggregationResult>();
            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                lstSkillAgregation = db.uspWFMGetAggregation(this.TenantID, this.TenantSpecialFlag).ToList();
            }
            return View(lstSkillAgregation);
        }


        public ActionResult Create()
        {
            GetSkillGroupListForDDL(0);
            return View();
        }

        [HttpPost]
        public ActionResult Create(string vAggregationName)
        {
            if (ModelState.IsValid)
            {
                string skillGroupIDs = Request.Form["selectSkillGroup"];

                using (WFMDBDataContext db = new WFMDBDataContext())
                {
                    db.uspWFMInsertAggregation(vAggregationName, skillGroupIDs, this.TenantID, this.TenantSpecialFlag);
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            uspWFMGetSingleAggregationResult result;
            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                result = db.uspWFMGetSingleAggregation(id).FirstOrDefault();
            }
            GetSkillGroupListForDDL(id);
            if (result != null)
            {
                SkillAgregationModel model = new SkillAgregationModel() { iAggregationID = result.iAggregationID, vAggregationName = result.vAggregationName };
                return View(model);
            }
            else
            {
                return RedirectToAction("Message", "Error", new { Error = "スキルアグリゲーション在しません。" });
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, SkillAgregationModel skillAgregation)
        {
            if (ModelState.IsValid)
            {
                if (id != skillAgregation.iAggregationID)
                {
                    return RedirectToAction("Message", "Error", new { Error = "スキルアグリゲーション在しません。" });
                }
            }
            string skillGroupIDs = Request.Form["selectSkillGroup"];
            WFMDBDataContext db = new WFMDBDataContext();
            db.uspWFMUpdateAggregation(id, skillAgregation.vAggregationName, skillGroupIDs);
            return RedirectToAction("Index");
        }

        public ActionResult Delete_Ajax(string id)
        {
            WFMDBDataContext db = new WFMDBDataContext();
            db.uspWFMDelSingleAggregation(int.Parse(id));

            return Json(new { StatusCode = AppConst.Enum_JsonStatus.Remove }, JsonRequestBehavior.AllowGet);
        }
    }
}