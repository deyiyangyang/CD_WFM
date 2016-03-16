using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFM.Controllers;
using WFM.Common;
using System.Data.Linq;
using WFM.Attribute;

namespace WFM.Areas.Report.Controllers
{
    public class CallDetailController : BaseController
    {
        private static string[] const_SortFields = { "dtCreatedCall" };

        // GET: Report/CallDetail
        public ActionResult Index(string dtStart, string dtEnd, string vCalleeid,string iSessionProfileID, int? iConntype, int? iCompletedCall)
        {
            try
            {
                GetSkillGroupListForDDL(0, true);

                if (dtStart == null)
                    dtStart = DateTime.Now.ToString(AppConst.Const_Format_YMD);

                if (dtEnd == null)
                    dtEnd = DateTime.Now.ToString(AppConst.Const_Format_YMD);

                int isessionprofileidValue = 0;
                if (!string.IsNullOrEmpty(iSessionProfileID))
                {
                    try
                    {
                        int a = int.Parse(iSessionProfileID);
                        isessionprofileidValue = a;
                    }
                    catch
                    {
                        isessionprofileidValue = 0;
                    }
                }
                if (iConntype == null)
                    iConntype = -1;
                if (iCompletedCall == null)
                    iCompletedCall = -1;

                List<tblCallDetailV3> lstData = SearchData("1", m_CurrentPageSize.ToString(), "1", DateTime.Parse(dtStart), DateTime.Parse(dtEnd), "", "", 0, isessionprofileidValue, iConntype.Value, iCompletedCall.Value);
                //ページ情報
                CalcPagerData();

                ViewBag.iSessionProfileID = "";
                ViewBag.iConntype = -1;
                ViewBag.iCompletedCall = -1;
                return View(lstData);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog("CallDetailController index system error:" + ex.Message + ex.StackTrace);
            }
            return View();
        }



        [HttpPost]
        [ValidateInput(false)]
        [MultiActionAttribute("firstPage,nextPage,prePage,lastPage,pageChanged,pageChanged_Buttom,Search,pageSizeChanged")]
        public ActionResult Search(string pageIndex, string ddlPageSize, string pageTotal, string sortIndex, string sort, string dtStart, string dtEnd, string vCalleeid, string vCallerid, string selectSkillGroup, string iSessionProfileID, int? iConntype, int? iCompletedCall)
        {
            try
            {
                if (iConntype == null)
                    iConntype = -1;
                if (iCompletedCall == null)
                    iCompletedCall = -1;

                ViewBag.iSessionProfileID = iSessionProfileID;
                ViewBag.iConntype = iConntype.Value;
                ViewBag.iCompletedCall = iCompletedCall.Value;
                int isessionprofileidValue = 0;
                if (!string.IsNullOrEmpty(iSessionProfileID))
                {
                    try
                    {
                        int a = int.Parse(iSessionProfileID);
                        isessionprofileidValue = a;
                    }
                    catch
                    {
                        isessionprofileidValue = 0;
                    }
                }

                List<tblCallDetailV3> lstData = new List<tblCallDetailV3>();
                int nSortIndex = 0;
                int nSort = 0;
                int.TryParse(sortIndex, out nSortIndex);
                int.TryParse(sort, out nSort);
                string skillGroupIDs = Request.Form["selectSkillGroup"];
                if (string.IsNullOrEmpty(skillGroupIDs))
                    GetSkillGroupListForDDL(0, true);
                else
                    GetSkillGroupListForDDL(int.Parse(skillGroupIDs), true);
                m_SortField = nSortIndex;
                m_Sort = nSort;

                if (string.IsNullOrEmpty(selectSkillGroup))
                    lstData = SearchData(pageIndex, ddlPageSize, pageTotal, DateTime.Parse(dtStart), DateTime.Parse(dtEnd), vCalleeid,vCallerid, 0, isessionprofileidValue, iConntype.Value, iCompletedCall.Value);
                else
                    lstData = SearchData(pageIndex, ddlPageSize, pageTotal, DateTime.Parse(dtStart), DateTime.Parse(dtEnd), vCalleeid,vCallerid, int.Parse(selectSkillGroup), isessionprofileidValue, iConntype.Value, iCompletedCall.Value);

                //ページ情報
                CalcPagerData();

                return View("Index", lstData);
            }
            catch (Exception ex)
            {
                AppLog.WriteLog("CallDetailController index system error:" + ex.Message + ex.StackTrace);
            }
            return View("Index");
        }

        //検索処理
        private List<tblCallDetailV3> SearchData(string pageIndex, string pageSize, string pageTotal, DateTime dtST, DateTime dtEnd, string vCalleeid, string vCallerid, int skillID,int isessionprofileid,int iConntype, int iCompletedCall)
        {
            AppLog.WriteLog(string.Format("CallDetailController SearchData paramter is pageIndex:{0},pageSize:{1},pageTotal:{2},dtST:{3},dtEnd:{4},vCalleeid:{5},skillID{6}", pageIndex,pageSize, pageTotal, dtST.ToString(AppConst.Const_Format_YMD)
                ,dtEnd.ToString(AppConst.Const_Format_YMD),vCalleeid,skillID.ToString()));
            List<tblCallDetailV3> result = new List<tblCallDetailV3>();

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
                IMultipleResults results = db.uspWFMGetCallDetailV3(m_CurPageIndex, m_CurrentPageSize, strSortField, strSort,
                    this.TenantID, dtST.ToString("yyyy/MM/dd"), dtEnd.ToString("yyyy/MM/dd"), skillID, vCalleeid, vCallerid, isessionprofileid, iConntype, iCompletedCall);

                result = results.GetResult<tblCallDetailV3>().ToList();
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