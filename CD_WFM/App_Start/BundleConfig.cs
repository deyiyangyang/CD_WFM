using System.Web;
using System.Web.Optimization;

namespace WFM
{
    public class BundleConfig
    {
        // バンドルの詳細については、http://go.microsoft.com/fwlink/?LinkId=301862  を参照してください
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //JS Dialog,Orientation
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/common.js"));

            // 開発と学習には、Modernizr の開発バージョンを使用します。次に、実稼働の準備が
            // できたら、http://modernizr.com にあるビルド ツールを使用して、必要なテストのみを選択します。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/js/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/site.css"));

            //JQueryUI Flick
            bundles.Add(new StyleBundle("~/Content/jqueryuicss").Include(
                        "~/Content/themes/flick/jquery-ui.css"
                //"~/Content/themes/flick/jquery.ui.base.css",
                //"~/Content/themes/flick/jquery.ui.core.css",
                //"~/Content/themes/flick/jquery.ui.resizable.css",
                //"~/Content/themes/flick/jquery.ui.selectable.css",
                //"~/Content/themes/flick/jquery.ui.accordion.css",
                //"~/Content/themes/flick/jquery.ui.autocomplete.css",
                //"~/Content/themes/flick/jquery.ui.button.css",
                //"~/Content/themes/flick/jquery.ui.dialog.css",
                //"~/Content/themes/flick/jquery.ui.slider.css",
                //"~/Content/themes/flick/jquery.ui.tabs.css",
                //"~/Content/themes/flick/jquery.ui.datepicker.css",
                //"~/Content/themes/flick/jquery.ui.progressbar.css",
                //"~/Content/themes/flick/jquery.ui.theme.css"
                ));

            // デバッグを行うには EnableOptimizations を false に設定します。詳細については、
            // http://go.microsoft.com/fwlink/?LinkId=301862 を参照してください
            BundleTable.EnableOptimizations = false;
        }
    }
}
