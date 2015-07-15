using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WFM.Areas.Agent.Models
{
    public class ShiftAgentModel
    {
        [AllowHtml]
        [Display(Name = "エージェントID")]
        public string vAgentID { get; set; }

        [AllowHtml]
        [Display(Name = "エージェント名")]
        public string vDisplayName { get; set; }

        [AllowHtml]
        [Display(Name = "シフト名1")]
        public bool onShift1 { get; set; }

        [AllowHtml]
        [Display(Name = "シフト名2")]
        public bool onShift2 { get; set; }

        [AllowHtml]
        [Display(Name = "シフト名3")]
        public bool onShift3 { get; set; }

        [AllowHtml]
        [Display(Name = "シフト名4")]
        public bool onShift4 { get; set; }

        [AllowHtml]
        [Display(Name = "シフト名5")]
        public bool onShift5 { get; set; }

        [AllowHtml]
        [Display(Name = "日曜日")]
        public bool onSun { get; set; }

        [AllowHtml]
        [Display(Name = "月曜日")]
        public bool onMon { get; set; }

        [AllowHtml]
        [Display(Name = "火曜日")]
        public bool onTue { get; set; }

        [AllowHtml]
        [Display(Name = "水曜日")]
        public bool onWed { get; set; }

        [AllowHtml]
        [Display(Name = "木曜日")]
        public bool onThur { get; set; }

        [AllowHtml]
        [Display(Name = "金曜日")]
        public bool onFri { get; set; }

        [AllowHtml]
        [Display(Name = "土曜日")]
        public bool onSat { get; set; }
    }
}