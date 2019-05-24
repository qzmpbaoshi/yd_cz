using System.Configuration;

namespace CommonLibrary
{
    public class ReadConfigHelper
    {
        public static string BaseUrl = ConfigurationManager.AppSettings["BaseWebApiUrl"];
    }
}
