using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFM.Attribute;
using WFM.Common;
using WFM.Controllers;

namespace WFM.Areas.Agent.Controllers
{
    public class ShiftAgentWeekController : BaseController
    {
        //検索からの一覧のソートフィールドリスト
        private static string[] const_SortFields = { "vAgentID" };

        enum SortFields : int
        {
            vAgentID = 0
        }

        // GET: Agent/ShiftAgentWeek
        public ActionResult Index()
        {
            ViewBag.vTenantID = this.TenantID;
            ViewBag.vTenantSpeciaFlag = this.TenantSpecialFlag;

            GetAgentListForDDL("-1", true, false);
            //初期時の並び順
            m_SortField = (int)SortFields.vAgentID;

            //初期時の並び順
            m_Sort = (int)AppConst.SortMethod.ASC;

            List<tblAgentWeekShift> lstData = SearchData("1", m_CurrentPageSize.ToString(), "1", "");
            //ページ情報
            CalcPagerData();
            return View(lstData);
        }


        [HttpPost]
        [ValidateInput(false)]
        [MultiActionAttribute("firstPage,nextPage,prePage,lastPage,pageChanged,pageChanged_Buttom,Search,pageSizeChanged")]
        public ActionResult Search(string pageIndex, string ddlPageSize, string pageTotal, string sortIndex, string sort, string ddlAgentID)
        {
            int nSortIndex = 0;
            int nSort = 0;
            int.TryParse(sortIndex, out nSortIndex);
            int.TryParse(sort, out nSort);

            m_SortField = nSortIndex;
            m_Sort = nSort;
            GetAgentListForDDL(ddlAgentID, true, false);
            List<tblAgentWeekShift> lstData = SearchData(pageIndex, ddlPageSize, pageTotal, ddlAgentID);

            //ページ情報
            CalcPagerData();

            return View("Index", lstData);
        }


        //検索処理
        private List<tblAgentWeekShift> SearchData(string pageIndex, string pageSize, string pageTotal, string vAgentID)
        {
            List<tblAgentWeekShift> result = new List<tblAgentWeekShift>();

            int currentPageIndex = m_CurPageIndex;
            int currentPageSize = m_CurrentPageSize;

            int.TryParse(pageIndex, out currentPageIndex);
            int.TryParse(pageSize, out currentPageSize);

            m_CurPageIndex = currentPageIndex;
            m_CurrentPageSize = currentPageSize;

            string strSortField = GetSortField(m_SortField);
            string strSort = AppFunction.GetSortDefine(m_Sort);
            ViewBag.vTenantID = this.TenantID;
            ViewBag.vTenantSpeciaFlag = this.TenantSpecialFlag;
            //ページを再計算する
            CalcPage(pageTotal);

            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                IMultipleResults results = db.uspWFMSearchShiftAgentWeekPaged(m_CurPageIndex, m_CurrentPageSize, strSortField, strSort,
                    this.TenantID, this.TenantSpecialFlag, vAgentID);

                result = results.GetResult<tblAgentWeekShift>().ToList();
                tblDataPaged tblPage = results.GetResult<tblDataPaged>().FirstOrDefault();

                m_TotalRowCount = tblPage.TotalRowCount;
                m_CurPageIndex = tblPage.CurPageIndex;
                m_TotlePageCount = tblPage.TotalPageCount;
            }

            return result;
        }

        //画面の並び順の列を取得
        private static string GetSortField(int index)
        {
            if (index >= 0 && index < const_SortFields.Length)
                return const_SortFields[index];
            else
                return string.Empty;
        }

        [HttpGet]
        public ActionResult Edit(string vAgentID, string vAgentName)
        {
            GetShiftDDList("-1", false, true);
            ViewBag.vAgentID = vAgentID;
            ViewBag.vAgentName = vAgentName;
            return View();
        }



        [HttpPost]
        public ActionResult Edit(string ddlShift, string chkSun, string chkMon, string chkTue, string chkWed, string chkThur, string chkFri, string chkSat)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Request.Form["vAgentID"]))
                {

                }
                string weekDays = "";
                if (!string.IsNullOrEmpty(chkSun))
                    weekDays += "," + chkSun;
                if (!string.IsNullOrEmpty(chkMon))
                    weekDays += "," + chkMon;
                if (!string.IsNullOrEmpty(chkTue))
                    weekDays += "," + chkTue;
                if (!string.IsNullOrEmpty(chkWed))
                    weekDays += "," + chkWed;
                if (!string.IsNullOrEmpty(chkThur))
                    weekDays += "," + chkThur;
                if (!string.IsNullOrEmpty(chkFri))
                    weekDays += "," + chkFri;
                if (!string.IsNullOrEmpty(chkSat))
                    weekDays += "," + chkSat;
                if (!string.IsNullOrEmpty(weekDays))
                {
                    weekDays = weekDays.Substring(1);
                }

                string agentIDs = Request.Form["vAgentID"];
                //int index = 0;
                //while (agentIDs.Length>2000)
                //{
                //    string insertAgentIDs = agentIDs.Substring(index * 2000, 2000);
                using (WFMDBDataContext db = new WFMDBDataContext())
                {
                    db.uspWFMUpdateShiftAgentWeek(agentIDs, weekDays, int.Parse(ddlShift));
                }
                //}


            }
            return RedirectToAction("index");
        }

        public ActionResult Create()
        {
            GetAgentListWithShiftName("-1", false, true);
            GetShiftDDList("-1", false, true);
            return View();
        }

        [HttpPost]
        public ActionResult Create(string ddlShift, string chkSun, string chkMon, string chkTue, string chkWed, string chkThur, string chkFri, string chkSat)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Request.Form["selectAgent"]))
                {

                }
                string weekDays = "";
                if (!string.IsNullOrEmpty(chkSun))
                    weekDays += "," + chkSun;
                if (!string.IsNullOrEmpty(chkMon))
                    weekDays += "," + chkMon;
                if (!string.IsNullOrEmpty(chkTue))
                    weekDays += "," + chkTue;
                if (!string.IsNullOrEmpty(chkWed))
                    weekDays += "," + chkWed;
                if (!string.IsNullOrEmpty(chkThur))
                    weekDays += "," + chkThur;
                if (!string.IsNullOrEmpty(chkFri))
                    weekDays += "," + chkFri;
                if (!string.IsNullOrEmpty(chkSat))
                    weekDays += "," + chkSat;
                if (!string.IsNullOrEmpty(weekDays))
                {
                    weekDays = weekDays.Substring(1);
                }

                string agentIDs = Request.Form["selectAgent"];
                //int index = 0;
                //while (agentIDs.Length>2000)
                //{
                //    string insertAgentIDs = agentIDs.Substring(index * 2000, 2000);
                using (WFMDBDataContext db = new WFMDBDataContext())
                {
                    db.uspWFMInsertShiftAgentWeek(agentIDs, weekDays, int.Parse(ddlShift));
                }
                //}


            }
            return RedirectToAction("index");
            //return View("Index");
        }
    }
}