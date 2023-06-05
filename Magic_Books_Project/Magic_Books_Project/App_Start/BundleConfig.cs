using System.Web;
using System.Web.Optimization;

namespace Magic_Books_Project
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/style.css", "~/Content/elegant-icons.css",
                      "~/Content/font-awesome.min.css", "~/Content/jquery-ui.min.css",
                      "~/Content/magnific-popup.css", "~/Content/main.css",
                      "~/Content/owl.carousel.min.css", "~/Content/slicknav.min.css",
                      "~/Content/util.css",
                      "~/Content/PagedList.css"));
        }
    }
}
