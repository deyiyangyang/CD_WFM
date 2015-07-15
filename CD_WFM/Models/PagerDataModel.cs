using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WFM.Common;

namespace WFM.Models
{
    public class PagerDataModel
    {
        public int TotalRowCount { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int CurrentPageSize { get; set; }
    }

    public class SortDataModel
    {
        //デフォルト
        private string sortActon = AppConst.Const_Default_SortAction;
        public int TargetSortField { get; set; }
        public int CurrentSortField { get; set; }
        public int CurrentSortMethod { get; set; }
        public string AscDisplay { get; set; }
        public string DescDisplay { get; set; }
        public Boolean Revert { get; set; }
        public string SortAction
        {
            get { return sortActon; }
            set { sortActon = value; }
        }
    }
}