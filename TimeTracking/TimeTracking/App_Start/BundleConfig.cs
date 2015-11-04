using System;
using System.Web;
using System.Web.Optimization;

namespace TimeTracking
{
    public class BundleConfig
    {
        private const String BOWER_FOLDER = @"~/Content/bower_components/";

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                BOWER_FOLDER + "angular/angular.min.js",
                BOWER_FOLDER + "angular-route/angular-route.min.js",
                BOWER_FOLDER + "angular-resource/angular-resource.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .IncludeDirectory("~/app/", "*.module.js", true)
                .IncludeDirectory("~/app/", "*.js", true));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
