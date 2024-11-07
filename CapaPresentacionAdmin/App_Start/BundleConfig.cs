using System.Web;
using System.Web.Optimization;

namespace CapaPresentacionAdmin
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new Bundle("~/bundles/complementos").Include(
                        "~/Scripts/fontawesome/all.min.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.responsive.js",
                         "~/Scripts/loadingoverlay/loadingoverlay.min.js",
                        "~/Scripts/scripts.js",
                        "~/Scripts/loadingoverlay.min.js",
                        "~/Scripts/jquery.validate.js",
                         "~/Scripts/jquery.validate.js",
                          "~/Scripts/jquery-ui-1.13.2.js",
                      "~/Scripts/sweetalert.min.js"));



            bundles.Add(new Bundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/DataTables/css/jquery.dataTables.css",
                "~/Content/DataTables/css/responsive.dataTables.css",
                "~/Contents/themes/base/jquery-ui.css",
                "~/Content/sweetalert.css"));
        }
    }
}
