using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFM.Areas.Manager.Models;
using WFM.Attribute;
using WFM.Common;
using WFM.Controllers;

namespace WFM.Areas.Manager.Controllers
{
    public class AHTController : BaseController
    {
        //検索からの一覧のソートフィールドリスト
        private static string[] const_SortFields = { "vAgentID" };

        enum SortFields : int
        {
            vAgentID = 0
        }

        // GET: Manager/AHT
        public ActionResult Index(string dtStart, string dtEnd, string groupRadioOptions)
        {
            bool isGroupBySkill = true;
            GetAgentListForDDL("-1", true, false);
            //初期時の並び順
            m_SortField = (int)SortFields.vAgentID;

            //初期時の並び順
            m_Sort = (int)AppConst.SortMethod.ASC;

            if (dtStart == null)
                dtStart = DateTime.Now.ToString(AppConst.Const_Format_YMD);

            if (dtEnd == null)
                dtEnd = DateTime.Now.ToString(AppConst.Const_Format_YMD);

            if (groupRadioOptions == "byAgregation")
            {
                isGroupBySkill = false;
                ViewBag.RadioChecked = "byAgregation";
            }

            List<tblAgentAHT> lstData = SearchData("1", m_CurrentPageSize.ToString(), "1", DateTime.Parse(dtStart), DateTime.Parse(dtEnd), "", isGroupBySkill);
            //ページ情報
            CalcPagerData();
            return View(lstData);
        }

        [HttpPost]
        [ValidateInput(false)]
        [MultiActionAttribute("firstPage,nextPage,prePage,lastPage,pageChanged,pageChanged_Buttom,Search,pageSizeChanged")]
        public ActionResult Search(string pageIndex, string ddlPageSize, string pageTotal, string sortIndex, string sort, string dtStart, string dtEnd, string groupRadioOptions, string ddlAgentID)
        {
            int nSortIndex = 0;
            int nSort = 0;
            int.TryParse(sortIndex, out nSortIndex);
            int.TryParse(sort, out nSort);
            bool isGroupBySkill = true;
            m_SortField = nSortIndex;
            m_Sort = nSort;
            GetAgentListForDDL(ddlAgentID, true, false);

            if (groupRadioOptions == "byAgregation")
            {
                isGroupBySkill = false;
                ViewBag.RadioChecked = "byAgregation";
            }

            List<tblAgentAHT> lstData = SearchData(pageIndex, ddlPageSize, pageTotal, DateTime.Parse(dtStart), DateTime.Parse(dtEnd), ddlAgentID, isGroupBySkill);

            //ページ情報
            CalcPagerData();

            return View("Index", lstData);
        }

        //検索処理
        private List<tblAgentAHT> SearchData(string pageIndex, string pageSize, string pageTotal, DateTime dtST, DateTime dtEnd, string vAgentID, bool isGroupBySkill)
        {
            List<tblAgentAHT> result = new List<tblAgentAHT>();

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
                if (isGroupBySkill)
                {
                    IMultipleResults results = db.uspWFMSearchAHTGroupBySkillPaged(m_CurPageIndex, m_CurrentPageSize, strSortField, strSort,
                    this.TenantID, dtST, dtEnd, this.TenantSpecialFlag, vAgentID);

                    result = results.GetResult<tblAgentAHT>().ToList();
                    tblDataPaged tblPage = results.GetResult<tblDataPaged>().FirstOrDefault();

                    m_TotalRowCount = tblPage.TotalRowCount;
                    m_CurPageIndex = tblPage.CurPageIndex;
                    m_TotlePageCount = tblPage.TotalPageCount;
                }
                else
                {
                    IMultipleResults results = db.uspWFMSearchAHTGroupBySkillAgregationPaged(m_CurPageIndex, m_CurrentPageSize, strSortField, strSort,
                    this.TenantID, dtST, dtEnd, this.TenantSpecialFlag, vAgentID);

                    result = results.GetResult<tblAgentAHT>().ToList();
                    tblDataPaged tblPage = results.GetResult<tblDataPaged>().FirstOrDefault();

                    m_TotalRowCount = tblPage.TotalRowCount;
                    m_CurPageIndex = tblPage.CurPageIndex;
                    m_TotlePageCount = tblPage.TotalPageCount;
                }

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