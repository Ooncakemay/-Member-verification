using System.Web;
using System.Web.Optimization;

namespace WebApplication3
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.js",
                        "~/Scripts/jquery-ui-1.10.4.js",
                          "~/Scripts/jquery.jcarousel.js",
                           "~/Scripts/jcarousel.js",
                           "~/Scripts/jquery.validate.js",
                           "~/Scripts/jquery.validate.min.js",
                           "~/Scripts/main.js",
                           "~/Scripts/jquery - 1.10.2.js"));

             bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

        
            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/te_lrp_reset.css",
                      "~/Content/animate.css",
                      "~/Content/te_lrp_slide_in_panel.css",
                      "~/Content/te_lrp_common.css",
                      "~/Content/te_lrp_index.css",
                      "~/Content/te_lrp_product.css", 
                      "~/Content/te_lrp_login_reg.css",
                     "~/Content/te_lrp_login_reg.css" ,
                     "~/Content/jquery-ui.css")
                      );
            /*
             *   <link href="/Styles/te_lrp_reset.css" rel="stylesheet"/>
<link href="/Styles/te_lrp_common.css" rel="stylesheet"/>
             */
        }
    }
}
