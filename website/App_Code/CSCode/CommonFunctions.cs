using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CommonFunctions
/// </summary>
public class CommonFunctions
{
    public static string AppendToUrl(string Url, string key, string value)
    {
        if (string.IsNullOrEmpty(Url))
            Url = ReadConfiguration.SiteUrl + "default.aspx";
        //Url = Url.ToLower();
        string PageUrl;
        string[] qsKeys;
        if (Url.Contains("?"))
        {
            PageUrl = Url.Substring(0, Url.IndexOf('?') + 1);
            qsKeys = Url.Substring(Url.IndexOf('?') + 1).Split('&');
            foreach (string sParam in qsKeys)
            {
                if (!string.IsNullOrEmpty(sParam) && !sParam.StartsWith(key + "="))
                {
                    PageUrl += sParam + "&";
                }
            }
        }
        else
        {
            PageUrl = Url + "?";
        }
        if (!string.IsNullOrEmpty(value))
            PageUrl += key + "=" + value;


        if (PageUrl.EndsWith("?"))
            PageUrl = PageUrl.Substring(0, PageUrl.Length - 1);
        if (PageUrl.EndsWith("&"))
            PageUrl = PageUrl.Substring(0, PageUrl.Length - 1);
        return PageUrl;
    }
    public static string AppendToUrlPager(string Url, string key, string value)
    {
        if (string.IsNullOrEmpty(Url))
            Url = ReadConfiguration.SiteUrl + "default.aspx";
        //Url = Url.ToLower();
        string PageUrl;
        string[] qsKeys;
        if (Url.Contains("?"))
        {
            PageUrl = Url.Substring(0, Url.IndexOf('?') + 1);
            //qsKeys = Url.Substring(Url.IndexOf('?') + 1).Split('&');
            //foreach (string sParam in qsKeys)
            //{
            //    if (!string.IsNullOrEmpty(sParam) && !sParam.StartsWith(key + "="))
            //    {
            //        PageUrl += sParam + "&";
            //    }
            //}
        }
        else
        {
            PageUrl = Url + "?";
        }
        if (!string.IsNullOrEmpty(value))
            PageUrl += key + "=" + value;


        if (PageUrl.EndsWith("?"))
            PageUrl = PageUrl.Substring(0, PageUrl.Length - 1);
        if (PageUrl.EndsWith("&"))
            PageUrl = PageUrl.Substring(0, PageUrl.Length - 1);
        return PageUrl;
    }

    public static string AppendToUrlPager2(string Url, string key, string value)
    {
        if (string.IsNullOrEmpty(Url))
            Url = ReadConfiguration.SiteUrl + "default.aspx";
        //Url = Url.ToLower();
        string PageUrl;
        string[] qsKeys;
        if (Url.Contains("?"))
        {
            PageUrl = Url.Substring(0, Url.IndexOf('?') + 1);
            qsKeys = Url.Substring(Url.IndexOf('?') + 1).Split('&');
            foreach (string sParam in qsKeys)
            {
                if (!string.IsNullOrEmpty(sParam) && !sParam.StartsWith(key + "="))
                {
                    PageUrl += sParam + "&";
                }
            }
        }
        else
        {
            PageUrl = Url + "?";
        }
        if (!string.IsNullOrEmpty(value))
            PageUrl += key + "=" + value;


        if (PageUrl.EndsWith("?"))
            PageUrl = PageUrl.Substring(0, PageUrl.Length - 1);
        if (PageUrl.EndsWith("&"))
            PageUrl = PageUrl.Substring(0, PageUrl.Length - 1);
        return PageUrl;
    }

}
