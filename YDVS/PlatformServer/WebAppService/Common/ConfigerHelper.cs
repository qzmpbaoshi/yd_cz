using System.Configuration;

namespace WebAppService
{
    public static class ConfigerHelper
    {
        public static string GetBaseAttPath()
        {
            string BaseAttPath = ConfigurationManager.AppSettings["AnalysisReportAttachment_Path"];
            if (!string.IsNullOrWhiteSpace(BaseAttPath))
                return BaseAttPath + @"\AnalysisReportAttachment\";
            else
                return System.Web.Hosting.HostingEnvironment.MapPath("~/AnalysisReportAttachment/");
        }
    }
}