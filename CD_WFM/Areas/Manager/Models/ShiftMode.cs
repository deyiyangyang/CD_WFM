using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WFM.Areas.Manager.Models
{
    public class ShiftModel
    {
        public int ShiftID { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "※シフト名をご入力ください。")]
        [MaxLength(20)]
        [Display(Name = "シフト名")]
        public string ShiftName { get; set; }

        [AllowHtml]
        [MaxLength(5)]
        [Required(ErrorMessage = "※開始時間をご入力ください。")]
        [RegularExpression(@"([0-1]?[0-9]|2[0-3]):([0-5][0-9])", ErrorMessage = "開始時間無効")] 
        [Display(Name = "開始時間")]
        public string StartTime { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "※終了時間をご入力ください。")]
        [RegularExpression(@"([0-1]?[0-9]|2[0-3]):([0-5][0-9])", ErrorMessage = "終了時間無効")] 
        [MaxLength(5)]
        [Display(Name = "終了時間")]
        public string EndTime { get; set; }
    }
}