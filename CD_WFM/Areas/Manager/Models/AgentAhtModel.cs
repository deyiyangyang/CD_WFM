using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WFM.Areas.Manager.Models
{
    public class AgentAhtModel
    {
        [Display(Name = "エジェートID")]
        public string vAgentID { get; set; }

        [Display(Name = "エジェート名")]
        public string vAgentName { get; set; }

        [Display(Name = "局番グループ")]
        public string vGroupName { get; set; }

        [Display(Name = "スキルグループ")]
        public string vSkillGroupName { get; set; }

        [Display(Name = "スキルアグリゲーショ名")]
        public string vSkillAgregationName { get; set; }

        [Display(Name = "着信通話時間")]
        public int iTotalInBoundTalkDru { get; set; }

        [Display(Name = "着信通話回数")]
        public int iCountInBoundTalk { get; set; }

        [Display(Name = "着信平均通話時間")]
        public int iAvgInBoundTalk { get; set; }

        [Display(Name = "後処理時間")]
        public int iTotalWorktimeDru { get; set; }

        [Display(Name = "後処理回数")]
        public int iCountWorktime { get; set; }

        [Display(Name = "平均後処理時間")]
        public int iAvgWorktime { get; set; }

        [Display(Name = "保留時間")]
        public int iTotalHoldDru { get; set; }

        [Display(Name = "保留回数")]
        public int iCountHold { get; set; }

        [Display(Name = "平均保留時間")]
        public int iAvgHold { get; set; }

        [Display(Name = "AHT")]
        public int AHT { get; set; }
    }
}