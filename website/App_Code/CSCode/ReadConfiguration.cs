using System.Configuration;
using System;


public class ReadConfiguration
{

    public static string SiteUrl = ConfigurationManager.AppSettings["SiteUrl"].ToString() ?? "";
    public static string SecureSiteUrl = ConfigurationManager.AppSettings["SecureSiteUrl"].ToString() ?? "";
    public static string GraphicsUrl = ConfigurationManager.AppSettings["GraphicsUrl"].ToString() ?? "";
    public static string SurveyEmail = ConfigurationManager.AppSettings["SurveyEmail"].ToString() ?? "";
    public static string DocumentsPath = ConfigurationManager.AppSettings["DocumentsPath"].ToString() ?? "";
    public static string DocumentsTempPath = ConfigurationManager.AppSettings["DocumentsTempPath"].ToString() ?? "";
    public static string ContactEmail = ConfigurationManager.AppSettings["ContactEmail"].ToString() ?? "";
    public static bool siteIsLive = Convert.ToBoolean(ConfigurationManager.AppSettings["siteIsLive"].ToString());

}
