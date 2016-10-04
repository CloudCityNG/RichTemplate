using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;
/// <summary>
/// Summary description for SessionManager
/// </summary>
public class SessionManager
{
    public static string Donation
    {
        get
        {
            if (HttpContext.Current.Session["Donation"] == null)
                return "";
            else
                return Convert.ToString(HttpContext.Current.Session["Donation"]);
        }
        set
        {
            HttpContext.Current.Session["Donation"] = value;
        }
    }
    public static string Thanks
    {
        get
        {
            if (HttpContext.Current.Session["Thanks"] == null)
                return "";
            else
                return Convert.ToString(HttpContext.Current.Session["Thanks"]);
        }
        set
        {
            HttpContext.Current.Session["Thanks"] = value;
        }
    }
}
