using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WFM.Models;
using WFM.Common;

namespace WFM.Controllers
{
    //共通機能を実装したコントローラ
    [Authorize]
    public abstract class BaseController : Controller
    {
        #region TenantID

        public string const_Session_Tenant_Key = "vTenantID";
        public string const_Session_TenantSpecial_Key = "vTenantSpecialFlag";
        public string TenantID
        {
            get
            {
                if (Session[const_Session_Tenant_Key] == null)
                {
                    return "";
                }
                else
                {
                    return Session[const_Session_Tenant_Key].ToString();
                }
            }
        }

        public string TenantSpecialFlag
        {
            get
            {
                if (Session[const_Session_TenantSpecial_Key] == null)
                {
                    return "";
                }
                else
                {
                    return Session[const_Session_TenantSpecial_Key].ToString();
                }
            }
        }
        #endregion

        #region Sort

        public string const_View_SortField = "View_SortField";
        public string const_View_Sort = "View_Sort";

        //SortField
        public int m_SortField
        {
            get
            {
                if (ViewData[const_View_SortField] == null)
                {
                    ViewData[const_View_SortField] = 0;
                }

                return (int)ViewData[const_View_SortField];
            }

            set
            {
                ViewData[const_View_SortField] = value;
            }
        }

        //Sort Method
        public int m_Sort
        {
            get
            {
                if (ViewData[const_View_Sort] == null)
                {
                    ViewData[const_View_Sort] = 0;
                }

                return (int)ViewData[const_View_Sort];
            }

            set
            {
                ViewData[const_View_Sort] = value;
            }
        }
        #endregion

        #region Pager

        //Page Size List
        public const string const_PageSizeList = "pageSizeList";
        //Page List
        public const string const_PageList = "pageList";

        public const string View_TotlePageCount = "View_TotlePageCount";
        public const string View_CurrentPageIndex = "View_CurPageIndex";
        public const string View_CurrentPageSize = "View_CurPageSize";

        private int nTotalRowCount = 0;
        private int nTotalPageCount = 1;
        private int nCurrentPageIndex = 1;
        private int nCurrentPageSize = AppConst.Const_DEFAULT_PAGE_ROWS;

        public int m_CurrentPageSize
        {
            get { return nCurrentPageSize; }
            set { nCurrentPageSize = value; }
        }

        public int m_CurPageIndex
        {
            get { return nCurrentPageIndex; }
            set { nCurrentPageIndex = value; }
        }

        public int m_TotlePageCount
        {
            get { return nTotalPageCount; }
            set { nTotalPageCount = value; }
        }

        public int m_TotalRowCount
        {
            get { return nTotalRowCount; }
            set { nTotalRowCount = value; }
        }

        #endregion

        //Sort Script
        [ChildActionOnly]
        public ActionResult SortScript()
        {
            return PartialView();
        }

        //Sort Header
        [ChildActionOnly]
        public ActionResult SortHeader(string sortAction)
        {
            string action = AppConst.Const_Default_SortAction;
            if (!string.IsNullOrEmpty(sortAction))
            {
                action = sortAction;
            }

            return PartialView(action);
        }

        //Sort Item
        [ChildActionOnly]
        public ActionResult SortItem(SortDataModel model)
        {
            return PartialView(model);
        }

        //検索結果なし
        [ChildActionOnly]
        public ActionResult NoResultFound()
        {
            return PartialView();
        }

        //改ページの上部
        [ChildActionOnly]
        public ActionResult PagerHeader(PagerDataModel model)
        {
            return PartialView(model);
        }

        //改ページの下部
        [ChildActionOnly]
        public ActionResult PageFooter(PagerDataModel model)
        {
            return PartialView(model);
        }

        //改ページの下部
        [ChildActionOnly]
        public ActionResult JqueryDialog()
        {
            return PartialView();
        }

        //JQueryのダイアログを閉じる
        public ActionResult CloseDialog(string dialogID)
        {
            return Content("<script language='javascript' type='text/javascript'>window.parent.$('" + dialogID + "').dialog('close');</script>");
        }
        // ページリスト
        private void GetPageList(PagerDataModel model)
        {
            //ページ
            List<SelectListItem> lstPage = new List<SelectListItem>();
            for (int i = 1; i <= model.TotalPageCount; i++)
            {
                var item = new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = (i == model.CurrentPageIndex)
                };

                lstPage.Add(item);
            }

            ViewData[const_PageList] = lstPage;
        }

        //ページサイズリスト
        private void GetPageSizeList(PagerDataModel model)
        {
            //ページ毎の表示件数
            List<string> lstItem = new List<string>();
            foreach (int p in AppConst.Const_PAGESIZE_DEFINES)
            {
                lstItem.Add(p.ToString());
            }

            List<SelectListItem> lstPageSize = new List<SelectListItem>();

            for (int i = 0; i < lstItem.Count; i++)
            {
                var item = new SelectListItem
                {
                    Text = lstItem[i].ToString() + "件",
                    Value = lstItem[i].ToString(),
                    Selected = (lstItem[i] == model.CurrentPageSize.ToString())
                };

                lstPageSize.Add(item);
            }

            ViewData[const_PageSizeList] = lstPageSize;
        }

        //改ページを計算する
        protected void CalcPagerData()
        {
            PagerDataModel pagerDataModel = new PagerDataModel();
            pagerDataModel.CurrentPageIndex = m_CurPageIndex;
            pagerDataModel.CurrentPageSize = m_CurrentPageSize;
            pagerDataModel.TotalPageCount = m_TotlePageCount;
            pagerDataModel.TotalRowCount = nTotalRowCount;
            ViewBag.PagerDataModel = pagerDataModel;

            //ページサイズリスト
            GetPageSizeList(pagerDataModel);

            // ページリスト
            GetPageList(pagerDataModel);
        }

        //改ページ処理
        protected void CalcPage(string pageTotal)
        {
            int nTotalPage = int.Parse(pageTotal);
            m_TotlePageCount = nTotalPage;

            //検索するとき、リセット
            if (Request.Params.AllKeys.Contains("Search"))
            {
                m_CurPageIndex = 1;
                return;
            }

            if (Request.Params.AllKeys.Contains("firstPage"))
                m_CurPageIndex = 1;
            else if (Request.Params.AllKeys.Contains("nextPage"))
            {
                if (m_CurPageIndex < m_TotlePageCount)
                    m_CurPageIndex++;
            }
            else if (Request.Params.AllKeys.Contains("prePage"))
            {
                if (m_CurPageIndex > 1)
                    m_CurPageIndex--;
            }
            else if (Request.Params.AllKeys.Contains("lastPage"))
                m_CurPageIndex = m_TotlePageCount;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session[const_Session_Tenant_Key] == null)
                Session[const_Session_Tenant_Key] = "05058085468";
            Session[const_Session_TenantSpecial_Key] = "";
            base.OnActionExecuting(filterContext);

            AppLog.WriteLog(string.Format("Action Start! Action=[{0}], Controller=[{1}]，TenantID=[{2}]", filterContext.ActionDescriptor.ActionName, filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, Session[const_Session_Tenant_Key] == null ? "noTenantID" : Session[const_Session_Tenant_Key].ToString()));
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            AppLog.WriteLog(string.Format("Action End! Action=[{0}], Controller=[{1}]，TenantID=[{2}]", filterContext.ActionDescriptor.ActionName, filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, Session[const_Session_Tenant_Key] == null ? "noTenantID" : Session[const_Session_Tenant_Key].ToString()));
        }

        /// <summary>
        /// get agents show in dropdown list 
        /// </summary>
        /// <param name="strAgentID">selected agentid</param>
        /// <param name="hasExtraHeader"> whether has 指定なし </param>
        /// <param name="isShowDisplayName">AgentID + Agent Display name</param>
        protected void GetAgentListForDDL(string strAgentID, bool hasExtraHeader, bool isShowDisplayName)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            List<uspWFMGetAgentResult> lstAgent = new List<uspWFMGetAgentResult>();
            if (hasExtraHeader)
            {
                lstItem.Insert(0, new SelectListItem { Text = "指定なし", Value = "" });
            }

            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                lstAgent = db.uspWFMGetAgent(this.TenantID).ToList();
            }
            foreach (var item in lstAgent)
            {
                if (!isShowDisplayName)
                    lstItem.Add(new SelectListItem { Text = item.vLogin, Value = item.vLogin, Selected = (item.vLogin.Equals(strAgentID)) });
                else
                    lstItem.Add(new SelectListItem { Text = item.vLogin + "_" + item.vDisplayName, Value = item.vLogin, Selected = (item.vLogin.Equals(strAgentID)) });
            }
            ViewData["lstAgent"] = lstItem;
        }

        /// <summary>
        /// get all the skill group 
        /// </summary>
        /// <param name="skillAgregationID">skill group in current skill agregation </param>
        protected void GetSkillGroupListForDDL(int skillAgregationID, bool hasExtraHeader = false)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            List<uspWFMGetSkillGroupResult> lstSkillGroup = new List<uspWFMGetSkillGroupResult>();

            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                lstSkillGroup = db.uspWFMGetSkillGroup(this.TenantID).ToList();
            }
            if (hasExtraHeader)
            {
                lstItem.Insert(0, new SelectListItem { Text = "指定なし", Value = "0" });
            }
            foreach (var item in lstSkillGroup)
            {
                lstItem.Add(new SelectListItem { Text = item.vCompany, Value = item.iGroupProfileID.ToString(), Selected = (item.iAggregationID.Equals(skillAgregationID)) });
            }
            ViewData["lstSkillGroup"] = lstItem;
        }

        /// <summary>
        /// get all the 局番 group 
        /// </summary>
        /// <param name="skillAgregationID">skill group in current skill agregation </param>
        protected void GetKyoGroupListForDDL(int groupID,bool hasExtraHeader = false)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            List<uspWFMGetKyokuGroupResult> lstSkillGroup = new List<uspWFMGetKyokuGroupResult>();

            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                lstSkillGroup = db.uspWFMGetKyokuGroup(this.TenantID).ToList();
            }
            if (hasExtraHeader)
            {
                lstItem.Insert(0, new SelectListItem { Text = "指定なし", Value = "0" });
            }
            foreach (var item in lstSkillGroup)
            {
                lstItem.Add(new SelectListItem { Text = item.vCompany, Value = item.iGroupProfileID.ToString(), Selected = (item.iGroupProfileID.Equals(groupID)) });
            }
            ViewData["selectJuGroup"] = lstItem;
        }

        /// <summary>
        /// get all the skill group 
        /// </summary>
        /// <param name="selectedGroupIDs">these group ids are seleted </param>
        protected void GetSkillGroupListNoWithSkillAgregationForDDL(string selectedGroupIDs, bool hasExtraHeader = false)
        {
            if (string.IsNullOrEmpty(selectedGroupIDs)) selectedGroupIDs = "None";
            List<SelectListItem> lstItem = new List<SelectListItem>();
            List<uspWFMGetSkillGroupResult> lstSkillGroup = new List<uspWFMGetSkillGroupResult>();
            if (hasExtraHeader)
            {
                lstItem.Insert(0, new SelectListItem { Text = "指定なし", Value = "0" });
            }
            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                lstSkillGroup = db.uspWFMGetSkillGroup(this.TenantID).ToList();
            }
            foreach (var item in lstSkillGroup)
            {
                if (lstItem.Find(p => p.Value == item.iGroupProfileID.ToString()) != null)
                    continue;
                lstItem.Add(new SelectListItem { Text = item.vCompany, Value = item.iGroupProfileID.ToString(), Selected = selectedGroupIDs.Contains(item.iGroupProfileID.ToString()) });
            }
            ViewData["lstSkillGroup"] = lstItem;
        }

        /// <summary>
        /// get skill agregation for list box control
        /// </summary>
        protected void GetSkillAgregationListForDDL(string selectedAgregationIDs, bool hasExtraHeader = false)
        {
            if (string.IsNullOrEmpty(selectedAgregationIDs)) selectedAgregationIDs = "None";
            List<SelectListItem> lstItem = new List<SelectListItem>();

            List<uspWFMGetAggregationResult> lstSkillAgregation = new List<uspWFMGetAggregationResult>();
            if (hasExtraHeader)
            {
                lstItem.Insert(0, new SelectListItem { Text = "指定なし", Value = "0" });
            }
            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                lstSkillAgregation = db.uspWFMGetAggregation(this.TenantID, this.TenantSpecialFlag).ToList();
            }
            foreach (var item in lstSkillAgregation)
            {
                lstItem.Add(new SelectListItem { Text = item.vAggregationName, Value = item.iAggregationID.ToString(), Selected = selectedAgregationIDs.Contains(item.iAggregationID.ToString()) });
            }
            ViewData["lstSkillAgregation"] = lstItem;
        }

        /// <summary>
        /// set date group condition for dropdown list
        /// </summary>
        /// <param name="dateTypeValue"></param>
        protected void GetDateTypForDDL(string dateTypeValue)
        {
            if (string.IsNullOrEmpty(dateTypeValue)) dateTypeValue = "dd";
            List<SelectListItem> lstItem = new List<SelectListItem>();
            lstItem.Add(new SelectListItem { Text = "日別", Value = "dd", Selected = "dd".Equals(dateTypeValue) });
            lstItem.Add(new SelectListItem { Text = "時別", Value = "hh", Selected = "hh".Equals(dateTypeValue) });
            lstItem.Add(new SelectListItem { Text = "月別", Value = "mm", Selected = "mm".Equals(dateTypeValue) });
            lstItem.Add(new SelectListItem { Text = "曜日別", Value = "dw", Selected = "dw".Equals(dateTypeValue) });
            lstItem.Add(new SelectListItem { Text = "週別", Value = "ww", Selected = "ww".Equals(dateTypeValue) });
            ViewData["lstDateType"] = lstItem;
        }

        protected void GetAgentListWithShiftName(string strAgentID, bool hasExtraHeader, bool isShowDisplayName)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            List<tblWFMAgentAndShifName> lstAgent = new List<tblWFMAgentAndShifName>();
            if (hasExtraHeader)
            {
                lstItem.Insert(0, new SelectListItem { Text = "指定なし", Value = "" });
            }

            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                lstAgent = db.uspWFMGetAgentAndShiftName(this.TenantID).ToList();
            }

            foreach (var item in lstAgent)
            {
                string shiftName = "";
                if (!string.IsNullOrEmpty(item.vShiftName1))
                {
                    shiftName += "," + item.vShiftName1;
                }
                if (!string.IsNullOrEmpty(item.vShiftName2))
                {
                    shiftName += "," + item.vShiftName2;
                }
                if (!string.IsNullOrEmpty(item.vShiftName3))
                {
                    shiftName += "," + item.vShiftName3;
                }
                if (!string.IsNullOrEmpty(item.vShiftName4))
                {
                    shiftName += "," + item.vShiftName4;
                }
                if (!string.IsNullOrEmpty(item.vShiftName5))
                {
                    shiftName += "," + item.vShiftName5;
                }
                if (!string.IsNullOrEmpty(item.vShiftName6))
                {
                    shiftName += "," + item.vShiftName6;
                }
                if (!string.IsNullOrEmpty(shiftName))
                {
                    shiftName = " (" + shiftName.Substring(1) + ")";
                }

                if (!isShowDisplayName)
                    lstItem.Add(new SelectListItem { Text = item.vAgentID, Value = item.vAgentID, Selected = (item.vAgentID.Equals(strAgentID)) });
                else
                    lstItem.Add(new SelectListItem { Text = item.vAgentID + "_" + item.vDisplayName + shiftName, Value = item.vAgentID, Selected = (item.vAgentID.Equals(strAgentID)) });
            }
            ViewData["lstAgent"] = lstItem;
        }

        protected void GetAgentListWithShiftNameForOneDay(DateTime dtDay,string currentShiftName, bool hasExtraHeader, bool isShowDisplayName)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            List<tblWFMAgentAndShifName> lstAgent = new List<tblWFMAgentAndShifName>();
            if (hasExtraHeader)
            {
                lstItem.Insert(0, new SelectListItem { Text = "指定なし", Value = "" });
            }

            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                lstAgent = db.uspWFMGetAgentAndShiftNameForOneDay(this.TenantID, dtDay).ToList();
            }

            foreach (var item in lstAgent)
            {
                string shiftName = "";
                if (!string.IsNullOrEmpty(item.vShiftName1))
                {
                    shiftName += "," + item.vShiftName1;
                }
                if (!string.IsNullOrEmpty(item.vShiftName2))
                {
                    shiftName += "," + item.vShiftName2;
                }
                if (!string.IsNullOrEmpty(item.vShiftName3))
                {
                    shiftName += "," + item.vShiftName3;
                }
                if (!string.IsNullOrEmpty(item.vShiftName4))
                {
                    shiftName += "," + item.vShiftName4;
                }
                if (!string.IsNullOrEmpty(item.vShiftName5))
                {
                    shiftName += "," + item.vShiftName5;
                }
                if (!string.IsNullOrEmpty(item.vShiftName6))
                {
                    shiftName += "," + item.vShiftName6;
                }
                if (!string.IsNullOrEmpty(shiftName))
                {
                    shiftName = " (" + shiftName.Substring(1) + ")";
                }

                if (!isShowDisplayName)
                    lstItem.Add(new SelectListItem { Text = item.vAgentID, Value = item.vAgentID, Selected = (shiftName.Contains(currentShiftName)) });
                else
                    lstItem.Add(new SelectListItem { Text = item.vAgentID + "_" + item.vDisplayName + shiftName, Value = item.vAgentID, Selected = (shiftName.Contains(currentShiftName)) });
            }
            ViewData["lstAgent"] = lstItem;
        }

        protected void GetShiftDDList(string shiftID, bool hasExtraHeader, bool isShowTime)
        {
            List<SelectListItem> lstItem = new List<SelectListItem>();
            List<uspWFMGetShiftResult> lstShift = new List<uspWFMGetShiftResult>();
            if (hasExtraHeader)
            {
                lstItem.Insert(0, new SelectListItem { Text = "指定なし", Value = "" });
            }

            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                lstShift = db.uspWFMGetShift(this.TenantID, this.TenantSpecialFlag).ToList();
            }
            foreach (var item in lstShift)
            {
                if (!isShowTime)
                    lstItem.Add(new SelectListItem { Text = item.vShiftName, Value = item.iShiftID.ToString(), Selected = (item.iShiftID.ToString().Equals(shiftID)) });
                else
                    lstItem.Add(new SelectListItem { Text = item.vShiftName + " (" + item.vStartTime + "～" + item.vEndTime + ")", Value = item.iShiftID.ToString(), Selected = (item.iShiftID.ToString().Equals(shiftID)) });
            }
            ViewData["lstShift"] = lstItem;
        }

        /// <summary>
        /// get data SynchroDate
        /// </summary>
        protected void GetDataSynchroDate()
        {
            string today = DateTime.Now.ToString(AppConst.Const_Format_YMD) + " 00:00:00";
            string lastUpdatedTime = today;
            using (WFMDBDataContext db = new WFMDBDataContext())
            {
                List<uspWFMGetDataSynchroResult> lst = db.uspWFMGetDataSynchro(this.TenantID).ToList();
                if (lst.Count > 0)
                {
                    lastUpdatedTime = lst[0].dtUpdated.Value.ToString(AppConst.Const_Format_YMDHMS);
                    if (string.Compare(today, lastUpdatedTime) > 0)
                    {
                        lastUpdatedTime = today;
                    }
                }
            }
            ViewBag.UpdatedTime = lastUpdatedTime;
        }
    }
}
