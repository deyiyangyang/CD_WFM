using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WFM.Common
{
    public partial class WFMDBDataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.uspWFMSearchShiftAgentPaged")]
        [ResultType(typeof(tblAgentWeekShift))]
        [ResultType(typeof(tblDataPaged))]
        public IMultipleResults uspWFMSearchShiftAgentPaged([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_pageIndex", DbType = "Int")] System.Nullable<int> iN_pageIndex, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_onePageRows", DbType = "Int")] System.Nullable<int> iN_onePageRows, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_orderBy", DbType = "NVarChar(255)")] string iN_orderBy, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_sort", DbType = "NVarChar(5)")] string iN_sort, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_vTenantID", DbType = "VarChar(32)")] string iN_vTenantID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_dtStart", DbType = "DateTime")] System.Nullable<System.DateTime> iN_dtStart, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_vTenantSpecialFlag", DbType = "VarChar(20)")] string iN_vTenantSpecialFlag, string iN_vAgentID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iN_pageIndex, iN_onePageRows, iN_orderBy, iN_sort, iN_vTenantID, iN_dtStart, iN_vTenantSpecialFlag, iN_vAgentID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.uspWFMSearchTestPaged")]
        [ResultType(typeof(tbCpfAgentDetailV3))]
        [ResultType(typeof(tbCpfCallDetailV3))]
        public IMultipleResults uspWFMSearchTestPaged(string vTenantID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vTenantID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.uspWFMSearchShiftAgentWeekPaged")]
        [ResultType(typeof(tblAgentWeekShift))]
        [ResultType(typeof(tblDataPaged))]
        public IMultipleResults uspWFMSearchShiftAgentWeekPaged([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_pageIndex", DbType = "Int")] System.Nullable<int> iN_pageIndex, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_onePageRows", DbType = "Int")] System.Nullable<int> iN_onePageRows, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_orderBy", DbType = "NVarChar(255)")] string iN_orderBy, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_sort", DbType = "NVarChar(5)")] string iN_sort, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_vTenantID", DbType = "VarChar(32)")] string iN_vTenantID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_vTenantSpecialFlag", DbType = "VarChar(20)")] string iN_vTenantSpecialFlag, string iN_vAgentID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iN_pageIndex, iN_onePageRows, iN_orderBy, iN_sort, iN_vTenantID, iN_vTenantSpecialFlag, iN_vAgentID);
            return ((IMultipleResults)(result.ReturnValue));
        }


        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.uspWFMSearchAHTGroupBySkillPaged")]
        [ResultType(typeof(tblAgentAHT))]
        [ResultType(typeof(tblDataPaged))]
        public IMultipleResults uspWFMSearchAHTGroupBySkillPaged([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_pageIndex", DbType = "Int")] System.Nullable<int> iN_pageIndex, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_onePageRows", DbType = "Int")] System.Nullable<int> iN_onePageRows, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_orderBy", DbType = "NVarChar(255)")] string iN_orderBy, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_sort", DbType = "NVarChar(5)")] string iN_sort, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_vTenantID", DbType = "VarChar(32)")] string iN_vTenantID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_dtStart", DbType = "DateTime")] System.Nullable<System.DateTime> iN_dtStart, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_dtEnd", DbType = "DateTime")] System.Nullable<System.DateTime> iN_dtEnd, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_vTenantSpecialFlag", DbType = "VarChar(20)")] string iN_vTenantSpecialFlag, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_vAgentID", DbType = "VarChar(32)")] string iN_vAgentID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iN_pageIndex, iN_onePageRows, iN_orderBy, iN_sort, iN_vTenantID, iN_dtStart, iN_dtEnd, iN_vTenantSpecialFlag, iN_vAgentID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.uspWFMSearchAHTGroupByAggregationPaged")]
        [ResultType(typeof(tblAgentAHT))]
        [ResultType(typeof(tblDataPaged))]
        public IMultipleResults uspWFMSearchAHTGroupByAggregationPaged([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_pageIndex", DbType = "Int")] System.Nullable<int> iN_pageIndex, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_onePageRows", DbType = "Int")] System.Nullable<int> iN_onePageRows, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_orderBy", DbType = "NVarChar(255)")] string iN_orderBy, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_sort", DbType = "NVarChar(5)")] string iN_sort, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_vTenantID", DbType = "VarChar(32)")] string iN_vTenantID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_dtStart", DbType = "DateTime")] System.Nullable<System.DateTime> iN_dtStart, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_dtEnd", DbType = "DateTime")] System.Nullable<System.DateTime> iN_dtEnd, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_vTenantSpecialFlag", DbType = "VarChar(20)")] string iN_vTenantSpecialFlag, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_vAgentID", DbType = "VarChar(32)")] string iN_vAgentID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iN_pageIndex, iN_onePageRows, iN_orderBy, iN_sort, iN_vTenantID, iN_dtStart, iN_dtEnd, iN_vTenantSpecialFlag, iN_vAgentID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.uspWFMRptOfRealAndPredictionInboundCall")]
        [ResultType(typeof(tblPedictionCall))]
        [ResultType(typeof(tblDataPaged))]
        public IMultipleResults uspWFMRptOfRealAndPredictionInboundCall([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_pageIndex", DbType = "Int")] System.Nullable<int> iN_pageIndex, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_onePageRows", DbType = "Int")] System.Nullable<int> iN_onePageRows, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_orderBy", DbType = "NVarChar(255)")] string iN_orderBy, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_sort", DbType = "NVarChar(5)")] string iN_sort, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(32)")] string vTenantID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] System.Nullable<System.DateTime> dtST, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] System.Nullable<System.DateTime> dtEND, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(16)")] string type, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "isByAggregation", DbType = "Int")] System.Nullable<int> isByAggregation, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(1000)")] string vSkillIDs, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(1000)")] string iAggregationIDs)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iN_pageIndex, iN_onePageRows, iN_orderBy, iN_sort, vTenantID, dtST, dtEND, type,isByAggregation, vSkillIDs, iAggregationIDs);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.uspWFMGetAgentDetailV3")]
        [ResultType(typeof(tblAgentDetailV3))]
        [ResultType(typeof(tblDataPaged))]
        public IMultipleResults uspWFMGetAgentDetailV3([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_pageIndex", DbType = "Int")] System.Nullable<int> iN_pageIndex, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_onePageRows", DbType = "Int")] System.Nullable<int> iN_onePageRows, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_orderBy", DbType = "NVarChar(255)")] string iN_orderBy, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_sort", DbType = "NVarChar(5)")] string iN_sort, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(32)")] string vTenant1, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(32)")] string dtSt1, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(32)")] string dtEnd1, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(32)")] string vLogin1)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iN_pageIndex, iN_onePageRows, iN_orderBy, iN_sort, vTenant1, dtSt1, dtEnd1, vLogin1);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.uspWFMGetCallDetailV3")]
        [ResultType(typeof(tblCallDetailV3))]
        [ResultType(typeof(tblDataPaged))]
        public IMultipleResults uspWFMGetCallDetailV3([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_pageIndex", DbType = "Int")] System.Nullable<int> iN_pageIndex, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_onePageRows", DbType = "Int")] System.Nullable<int> iN_onePageRows, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_orderBy", DbType = "NVarChar(255)")] string iN_orderBy, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IN_sort", DbType = "NVarChar(5)")] string iN_sort, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(32)")] string vTenant1, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(32)")] string dtSt1, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(32)")] string dtEnd1, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iSkillGroupID1, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(32)")] string vCalleeID1, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(32)")] string vCallerID1, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iSessionProfileID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iConntype, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iCompletedCall, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iQuecall, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iHasTrans, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iHasACD, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iGroupID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iN_pageIndex, iN_onePageRows, iN_orderBy, iN_sort, vTenant1, dtSt1, dtEnd1, iSkillGroupID1, vCalleeID1, vCallerID1, iSessionProfileID, iConntype, iCompletedCall, iQuecall, iHasTrans,iHasACD,iGroupID);
            return ((IMultipleResults)(result.ReturnValue));
        }


        //    [Function(Name = "dbo.SearchClientPaged")]
        //    [ResultType(typeof(tbCPFPersonalProfile))]
        //    [ResultType(typeof(tblDataPaged))]
        //    public IMultipleResults SearchClientPaged(
        //        int iN_pageIndex, int iN_onePageRows, string iN_OrderBy, string iN_sort, string iN_name, DateTime? iN_UpdStart,
        //        DateTime? iN_UpdEnd, int iN_iLevel, string iN_DisplayName, string iN_OfficeCompany, string iN_OfficeDepartment)
        //    {
        //        IExecuteResult result =
        //            this.ExecuteMethodCall(this,
        //                                   ((MethodInfo)(MethodInfo.GetCurrentMethod())),
        //                                   iN_pageIndex, iN_onePageRows, iN_OrderBy, iN_sort, iN_name, iN_UpdStart,
        //                                   iN_UpdEnd, iN_iLevel, iN_DisplayName, iN_OfficeCompany, iN_OfficeDepartment);

        //        return (IMultipleResults)(result.ReturnValue);
        //    }

        //    [Function(Name = "dbo.SearchStaffPaged")]
        //    [ResultType(typeof(tbCPFStaffProfile))]
        //    [ResultType(typeof(tblDataPaged))]
        //    public IMultipleResults SearchStaffPaged(
        //        int iN_pageIndex, int iN_onePageRows, string iN_OrderBy, string iN_sort, string iN_name, short iN_sex, string iN_login)
        //    {
        //        IExecuteResult result =
        //            this.ExecuteMethodCall(this,
        //                                   ((MethodInfo)(MethodInfo.GetCurrentMethod())),
        //                                   iN_pageIndex, iN_onePageRows, iN_OrderBy, iN_sort, iN_name, iN_sex, iN_login);

        //        return (IMultipleResults)(result.ReturnValue);
        //    }

        //    [Function(Name = "dbo.SearchIncidentPaged")]
        //    [ResultType(typeof(tbCPFIncidentProfile))]
        //    [ResultType(typeof(tblDataPaged))]
        //    public IMultipleResults SearchIncidentPaged(
        //        int iN_pageIndex, int iN_onePageRows, string iN_OrderBy, string iN_sort, string iN_vTitle, string iN_vDetail,
        //        int iN_iStatus, int iN_iTypeOfIncidentID, int iN_iDetailOfIncidentID,
        //        DateTime? iN_dtDeadline_S, DateTime? iN_dtDeadline_E)
        //    {
        //        IExecuteResult result =
        //            this.ExecuteMethodCall(this,
        //                                   ((MethodInfo)(MethodInfo.GetCurrentMethod())),
        //                                   iN_pageIndex, iN_onePageRows, iN_OrderBy, iN_sort, iN_vTitle, iN_vDetail,
        //                                   iN_iStatus, iN_iTypeOfIncidentID, iN_iDetailOfIncidentID, iN_dtDeadline_S, iN_dtDeadline_E);

        //        return (IMultipleResults)(result.ReturnValue);
        //    }

        //    [Function(Name = "dbo.SearchResponsePaged")]
        //    [ResultType(typeof(tbCPFResponseProfile))]
        //    [ResultType(typeof(tblDataPaged))]
        //    public IMultipleResults SearchResponsePaged(
        //        int iN_pageIndex, int iN_onePageRows, string iN_OrderBy, string iN_sort, string iN_vQuery, string iN_vResponse,
        //        int iN_iMedia, int iN_iStaffProfileID, int iN_iTypeOfResponseID, int iN_iDetailOfResponseID)
        //    {
        //        IExecuteResult result =
        //            this.ExecuteMethodCall(this,
        //                                   ((MethodInfo)(MethodInfo.GetCurrentMethod())),
        //                                   iN_pageIndex, iN_onePageRows, iN_OrderBy, iN_sort, iN_vQuery, iN_vResponse,
        //                                   iN_iMedia, iN_iStaffProfileID, iN_iTypeOfResponseID, iN_iDetailOfResponseID);

        //        return (IMultipleResults)(result.ReturnValue);
        //    }
    }
}