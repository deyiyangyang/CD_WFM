using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFM.Attribute;
using WFM.Common;
using WFM.Controllers;

namespace WFM.Areas.Report.Controllers
{
    public class CallReportController : BaseController
    {
        //検索からの一覧のソートフィールドリスト
        private static string[] const_SortFields = { "vCompany2" };

        // GET: Report/CallReport
        public ActionResult Index(string dtStart, string dtEnd, string groupRadioOptions)
        {
            bool isGroupBySkill = true;
            GetSkillGroupListNoWithSkillAgregationForDDL("");
            GetSkillAgregationListForDDL("");
            GetDateTypForDDL("");

            if (dtStart == null)
                dtStart = DateTime.Now.ToString(AppConst.Const_Format_YMD);

            if (dtEnd == null)
                dtEnd = DateTime.Now.ToString(AppConst.Const_Format_YMD);

            if (groupRadioOptions == "byAgregation")
            {
                isGroupBySkill = false;
                ViewBag.RadioChecked = "byAgregation";
            }

            List<tblPedictionCall> lstData = SearchData("1", m_CurrentPageSize.ToString(), "1", DateTime.Parse(dtStart), DateTime.Parse(dtEnd), "dd", "", "");
            //ページ情報
            CalcPagerData();
            return View(lstData);
        }


        [HttpPost]
        [ValidateInput(false)]
        [MultiActionAttribute("firstPage,nextPage,prePage,lastPage,pageChanged,pageChanged_Buttom,Search,pageSizeChanged")]
        public ActionResult Search(string pageIndex, string ddlPageSize, string pageTotal, string sortIndex, string sort, string dtStart, string dtEnd, string ddlDateType, string groupRadioOptions)
        {
            int nSortIndex = 0;
            int nSort = 0;
            int.TryParse(sortIndex, out nSortIndex);
            int.TryParse(sort, out nSort);
            string skillGroupIDs = Request.Form["selectSkillGroup"];
            string skillAgregationIDs = Request.Form["selectSkillAgregation"];
            m_SortField = nSortIndex;
            m_Sort = nSort;
            GetSkillGroupListNoWithSkillAgregationForDDL(skillGroupIDs);
            GetSkillAgregationListForDDL(skillAgregationIDs);
            GetDateTypForDDL(ddlDateType);
            if (groupRadioOptions == "byAgregation")
            {
                ViewBag.RadioChecked = "byAgregation";
            }

            List<tblPedictionCall> lstData = SearchData(pageIndex, ddlPageSize, pageTotal, DateTime.Parse(dtStart), DateTime.Parse(dtEnd), ddlDateType, skillGroupIDs, skillAgregationIDs);


            //ページ情報
            CalcPagerData();

            return View("Index", lstData);
        }

        //検索処理
        private List<tblPedictionCall> SearchData(string pageIndex, string pageSize, string pageTotal, DateTime dtST, DateTime dtEnd, string dateType, string vSkillIDs, string vSkillAgregationIDs)
        {
            AppLog.WriteLog("CallReportController SearchData start");
            List<tblPedictionCall> result = new List<tblPedictionCall>();

            try
            {
                int currentPageIndex = m_CurPageIndex;
                int currentPageSize = m_CurrentPageSize;

                int.TryParse(pageIndex, out currentPageIndex);
                int.TryParse(pageSize, out currentPageSize);
                int isByAggregation = 0;
                m_CurPageIndex = currentPageIndex;
                m_CurrentPageSize = currentPageSize;

                string strSortField = GetSortField(m_SortField);
                string strSort = AppFunction.GetSortDefine(m_Sort);
                ViewBag.vTenantID = this.TenantID;
                ViewBag.vTenantSpeciaFlag = this.TenantSpecialFlag;
                if (ViewBag.RadioChecked == "byAgregation")
                {
                    isByAggregation = 1;
                }
                //ページを再計算する
                CalcPage(pageTotal);

                AppLog.WriteLog("CallReportController SearchData  db excute start");
                using (WFMDBDataContext db = new WFMDBDataContext())
                {
                    IMultipleResults results = db.uspWFMRptOfRealAndPredictionInboundCall(m_CurPageIndex, m_CurrentPageSize, strSortField, strSort,
                        this.TenantID, dtST, dtEnd, dateType, isByAggregation, vSkillIDs, vSkillAgregationIDs);

                    result = results.GetResult<tblPedictionCall>().ToList();
                    tblDataPaged tblPage = results.GetResult<tblDataPaged>().FirstOrDefault();

                    m_TotalRowCount = tblPage.TotalRowCount;
                    m_CurPageIndex = tblPage.CurPageIndex;
                    m_TotlePageCount = tblPage.TotalPageCount;
                }
            }
            catch(Exception ex)
            {
                AppLog.WriteLog(ex.Message + ex.StackTrace);
            }
            

            return result;
        }

        #region ソート

        //画面の並び順の列を取得
        private static string GetSortField(int index)
        {
            if (index >= 0 && index < const_SortFields.Length)
                return const_SortFields[index];
            else
                return string.Empty;
        }

        #endregion
    }
}