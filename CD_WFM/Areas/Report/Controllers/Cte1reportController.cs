using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WFM.Areas.Report.Models;
using WFM.Common;
using WFM.Controllers;

namespace WFM.Areas.Report.Controllers
{
    public class Cte1reportController : BaseController
    {
        public ActionResult Index()
        {
            try
            {
                var url = string.Format("https://ct-e1s.comdesign.co.jp/ngcpsetMS/report.asp?u={0}&p={1}", this.TenantID, AppFunction.GetUserPassword(this.TenantID));
                AppLog.WriteLog("url:" + url);
                var models = parseHtml(url);
                return View(models);
            }
            catch(Exception ex)
            {
                AppLog.WriteLog("Cte1reportController index system error:" + ex.Message + ex.StackTrace);
                return View();
            }

        }

        public string GetHtmlSource2(string url)
        {
            string html = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "*/*";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.1.4322)";
            request.AllowAutoRedirect = true;
            request.Referer = url; //当前页面的引用



            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            html = reader.ReadToEnd();
            stream.Close();


            return html;
        }

        public IEnumerable<Cte1ReportViewModel> parseHtml(string url)
        {
            List<Cte1ReportViewModel> models = new List<Cte1ReportViewModel>();
            string startText = "<SELECT";
            string endText = "</SELECT>";

            string html = GetHtmlSource2(url);
            html = html.Replace("\r\n", "");
            int startIndex = html.IndexOf(startText);
            int endIndex = html.IndexOf(endText);
            var reportText = html.Substring(startIndex, endIndex - startIndex);
            string[] arrReport = reportText.Split(new string[] { "</OPTION>" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in arrReport)
            {
                var report = GetReportInfo(item);
                if (report != null)
                    models.Add(report);
            }
            return models;
        }

        /// <summary>
        /// <OPTION id='rptT1' name='rptT1' value='ReportForInfoDirectOfCallDetailOfAbandonXSecondLimitRealtimeV3' selected>通話呼詳細V3(CSV)(滞留時間含)(Exc_XSecAbn)
        /// </summary>
        /// <returns></returns>
        public Cte1ReportViewModel GetReportInfo(string singleReport)
        {
            Cte1ReportViewModel report = new Cte1ReportViewModel();
            singleReport = singleReport.ToUpper();
            if (!singleReport.Contains("<OPTION") || !singleReport.Contains("VALUE"))
            {
                return null;
            }

            int startIndex = singleReport.IndexOf("VALUE");
            singleReport = singleReport.Substring(startIndex);
            startIndex = singleReport.IndexOf("'");
            if (startIndex < 0) startIndex = singleReport.IndexOf("\"");
            if (startIndex < 0) return null;

            int lastIndex = singleReport.LastIndexOf("'");
            if (lastIndex < 0) lastIndex = singleReport.LastIndexOf("\"");
            if (lastIndex < 0) return null;

            string reportID = singleReport.Substring(startIndex+1, lastIndex - startIndex);
            startIndex = singleReport.IndexOf(">");
            string reportName = singleReport.Substring(startIndex + 1);
            if (string.IsNullOrEmpty(reportID) || string.IsNullOrEmpty(reportName))
                return null;
            report.ReportName = reportName;
            report.ReportID = reportID;
            return report;
        }
    }
}