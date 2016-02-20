using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WFM.Areas.Manager.Models
{
    public class SkillAgregationModel
    {
        public int iAggregationID { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "※スキルアグリゲーション名をご入力ください。")]
        [MaxLength(20)]
        [Display(Name = "スキルアグリゲーション名")]
        public string vAggregationName { get; set; }
    }
}