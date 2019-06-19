using System.Web;
using System.Web.Optimization;

namespace tccGoodEyes
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/jquery.mask.js",
                        "~/Content/main.js",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/pagErro.css",
            //          "~/Content/geral.css",
            //          "~/Content/pagHome.css",
            //          "~/Content/pagLogin.css",
            //          "~/Content/pagAtend.css",
            //          "~/Content/pagMinhaConta.css",
            //          "~/Content/layout.css"));

            bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory(
                "~/Content/otica", "*.css"));
        }
    }
}
