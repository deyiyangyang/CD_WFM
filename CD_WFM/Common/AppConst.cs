using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFM.Common
{
    public class AppConst
    {
        public static string Const_SiteName = System.Configuration.ConfigurationManager.AppSettings["SiteName"];

        public struct RolesInSite
        {
            public const string Level_Admin = "Level1";
            public const string Level_Normal = "Level2";
        }

        public static string[] Const_Weekday = new string[] { "日", "月", "火", "水", "木", "金", "土" };
        /// <summary>
        /// 金額のフォーマット
        /// </summary>
        public const string Const_Format_Money = "#,##0";

        public const string Const_Format_YM = "yyyy/MM";
        public const string Const_Format_YMD = "yyyy/MM/dd";
        public const string Const_Format_YMDHMS = "yyyy/MM/dd HH:mm:ss";
        public const string Const_Format_YMDHM = "yyyy/MM/dd HH:mm";
        public const string Const_Format_YMDHMS_NoSlash = "yyyyMMddHHmmssfff";

        public const string Const_SORT_ASC = "ASC";
        public const string Const_SORT_DESC = "DESC";
        public const string Const_SORT_OTHER = "";
        public static string[] Const_SORTS = { Const_SORT_ASC, Const_SORT_DESC, Const_SORT_OTHER };
        public const string Const_Default_SortAction = "sortBy";

        //区切り
        public const string Const_SPLIT_DELIMITER = ",";

        //ページのデフォルト表示行数
        public const int Const_DEFAULT_PAGE_ROWS = 50;
        //デフォルトのページサイズ
        public static int[] Const_PAGESIZE_DEFINES = { 5, 10, 50, 100 };

        public const string Const_Session_DBServer_Key = "DBServer";

        //並び順定義
        public enum SortMethod : int
        {
            ASC = 0,
            DESC = 1,
            OTHER = 2
        }

        //AJAXでJson返すエラーコード
        public enum Enum_JsonStatus : int
        {
            Error = -1,
            List = 0,
            Add = 1,
            Edit = 2,
            Remove = 3
        }
    }
}