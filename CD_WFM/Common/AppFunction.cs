using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFM.Common
{
    public class AppFunction
    {
        #region ユーザー名前を取得

        public static string GetUserName(string vTenantID)
        {
            string strRet = string.Empty;

            WFMDBDataContext db = new WFMDBDataContext();
            List<uspWFMGetTenantNameResult> lstUsers = db.uspWFMGetTenantName(vTenantID).ToList();

            if (lstUsers.Count > 0)
            {
                strRet = lstUsers[0].vDisplayName;
            }

            return strRet;
        }
        #endregion

        #region SORTの文字列を取得

        public static string GetSortDefine(int index)
        {
            if (index >= 0 && index < AppConst.Const_SORTS.Length)
                return AppConst.Const_SORTS[index];
            else
                return AppConst.Const_SORTS[(int)AppConst.SortMethod.OTHER];
        }
        #endregion
    }
}