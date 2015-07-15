using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFM.Models
{
    public class LineChart
    {
        public string[] labels { get; set; }
        public List<Dataset> datasets { get; set; }
    }

    public class Dataset
    {
        public string fillColor { get; set; }
        public string strokeColor { get; set; }
        public string pointColor { get; set; }
        public string pointStrokeColor { get; set; }
        public int[] data { get; set; }
        public string label { get; set; }
    }
}