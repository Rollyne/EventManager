namespace EventManager.Web
{
    using System.Web.Optimization;

    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/indexScript").Include("~/Scripts/CustomScripts/index.js"));
            bundles.Add(new ScriptBundle("~/bundles/scriptScript").Include("~/Scripts/CustomScripts/script.js"));
            bundles.Add(new ScriptBundle("~/bundles/sideBarScript").Include("~/Scripts/CustomScripts/sideBar.js"));
            bundles.Add(new ScriptBundle("~/bundles/DatepickerScript").Include("~/Scripts/CustomScripts/jquery-ui-datepicker.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/ImagePreviewScript").Include("~/Scripts/CustomScripts/imagePreview.js"));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/indexStyle").Include("~/Content/CustomStyles/index.css"));
            bundles.Add(new StyleBundle("~/Content/globalStyle").Include("~/Content/CustomStyles/global.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/Content/SitebarStyle").Include("~/Content/CustomStyles/sidebarSheet.css"));
            bundles.Add(new StyleBundle("~/Content/UserProfileStyle").Include("~/Content/CustomStyles/userPage.css"));
            bundles.Add(new StyleBundle("~/Content/CreateEventStyle").Include("~/Content/CustomStyles/createEvent.css"));
            bundles.Add(new StyleBundle("~/Content/CalendarStyle").Include("~/Content/CustomStyles/calendar.css"));
            bundles.Add(new StyleBundle("~/Content/MyProfileStyle").Include("~/Content/CustomStyles/myProfile.css"));
        }
    }
}
