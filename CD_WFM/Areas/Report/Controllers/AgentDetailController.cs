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
    public class AgentDetailController : BaseController
    {
        private static string[] const_SortFields = { "dtStatusStart" };

        // GET: Report/CallDetail
        public ActionResult Index(string dtStart, string dtEnd, string vCalleeid)
        {
            GetAgentListForDDL("-1", true, false);
            if (dtStart == null)
                dtStart = DateTime.Now.ToString(AppConst.Const_Format_YMD);

            if (dtEnd == null)
                dtEnd = DateTime.Now.ToString(AppConst.Const_Format_YMD) + " 23:59:59";

            ViewBag.iAgentStatus = -1;
            List<tblAgentDetailV3> lstData = SearchData("1", m_CurrentPageSize.ToString(), "1", DateTime.Parse(dtStart), DateTime.Parse(dtEnd), null,-1);
            //ページ情報
            CalcPagerData();
            return View(lstData);
        }



        [HttpPost]
        [ValidateInput(false)]
        [MultiActionAttribute("firstPage,nextPage,prePage,lastPage,pageChanged,pageChanged_Buttom,Search,pageSizeChanged")]
        public ActionResult Search(string pageIndex, string ddlPageSize, string pageTotal, string sortIndex, string sort, string dtStart, string dtEnd, string lstAgent, int? iAgentStatus)
        {
            List<tblAgentDetailV3> lstData = new List<tblAgentDetailV3>();
            int nSortIndex = 0;
            int nSort = 0;
            int.TryParse(sortIndex, out nSortIndex);
            int.TryParse(sort, out nSort);
            string vlogin = Request.Form["lstAgent"];
            if (string.IsNullOrEmpty(vlogin))
                GetAgentListForDDL(vlogin, true, false);
            else
                GetAgentListForDDL("-1", true, false);
            m_SortField = nSortIndex;
            m_Sort = nSort;
            if (iAgentStatus == null)
                iAgentStatus = -1;
            ViewBag.iAgentStatus = iAgentStatus.Value;

            lstData = SearchData(pageIndex, ddlPageSize, pageTotal, DateTime.Parse(dtStart), DateTime.Parse(dtEnd), vlogin, iAgentStatus.Value);

            //ページ情報
            CalcPagerData();

            return View("Index", lstData);
        }

        //検索処理
        private List<tblAgentDetailV3> SearchData(string pageIndex, string pageSize, string pageTotal, DateTime dtST, DateTime dtEnd, string vLogin, int iAgentStatus)
        {
            List<tblAgentDetailV3> result = new List<tblAgentDetailV3>();

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

            using (WFMDBDataContext db = new WFMDBDataContext(string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["SpecialConnection"].ConnectionString, DBServer)))
            {
                IMultipleResults results = db.uspWFMGetAgentDetailV3(m_CurPageIndex, m_CurrentPageSize, strSortField, strSort,
                    this.TenantID, dtST.ToString(AppConst.Const_Format_YMDHMS), dtEnd.ToString(AppConst.Const_Format_YMDHMS), vLogin, iAgentStatus);

                result = results.GetResult<tblAgentDetailV3>().ToList();
                tblDataPaged tblPage = results.GetResult<tblDataPaged>().FirstOrDefault();

                m_TotalRowCount = tblPage.TotalRowCount;
                m_CurPageIndex = tblPage.CurPageIndex;
                m_TotlePageCount = tblPage.TotalPageCount;
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