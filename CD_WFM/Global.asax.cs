using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WFM.Common;
using WFM.Controllers;

namespace WFM
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            //String Errormsg = String.Empty;
            Exception unhandledException = Server.GetLastError();
            //例外ログを記録する。
            AppLog.TraceLog(unhandledException);

            //エラーメール
            try
            {
                WFMDBDataContext db = new WFMDBDataContext();
                Guid? emailId = Guid.Empty;
                //db.InsertEmailReserve(ref emailId, AppFunction.GetSystemEmail(), string.Empty, string.Empty, "エラー", unhandledException.ToString(), User.Identity.Name);
            }
            catch { };

            Response.Clear();
            HttpException httpException = unhandledException as HttpException;

            if (httpException != null)
            {
                RouteData routeData = new RouteData();
                routeData.Values.Add("controller", "Error");

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found                        
                        routeData.Values.Add("action", "Error404");
                        break;
                    default:
                        routeData.Values.Add("action", "General");
                        break;
                }
                // Pass exception details to the target error View.
                routeData.Values.Add("Error", unhandledException);
                // clear error on server 
                Server.ClearError();
                // Call target Controller and pass the routeData.
                IController errorController = new ErrorController();
                errorController.Execute(new RequestContext(
                    new HttpContextWrapper(Context), routeData));
            }
            else
            {
                // clear error on server 
                Server.ClearError();
                RouteData routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                routeData.Values.Add("action", "General");
                routeData.Values.Add("Error", unhandledException);
                // Call target Controller and pass the routeData.
                IController errorController = new ErrorController();
                errorController.Execute(new RequestContext(
                    new HttpContextWrapper(Context), routeData));
            }
        }  
    }
}
