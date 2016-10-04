using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class UserControl_Pager : System.Web.UI.UserControl
{
    public int PageNo = 1;
    public int NoOfPages = 1;
    public string PageName = "";
    public int CurrentPage = 1;
    int mPageSize = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void BindPager(int total)
    {
        int i;
        if (!string.IsNullOrEmpty(Request[Constants.PageId]))
            CurrentPage = Convert.ToInt32(Request[Constants.PageId]);
        ArrayList ar = new ArrayList();
        if (total % PageSize == 0)
            NoOfPages = total / PageSize;
        else
            NoOfPages = total / PageSize + 1;
        for (i = 1; i <= NoOfPages; i++)
            ar.Add(i);
        if (NoOfPages < CurrentPage)
            CurrentPage = CurrentPage - 1;
        dlPage.DataSource = ar;
        dlPage.DataBind();
    }

    protected string GetUrl(string PageNo)
    {
        if (Convert.ToInt32(PageNo) == CurrentPage)
            return CommonFunctions.AppendToUrl("#", "", "");
        else
            return CommonFunctions.AppendToUrlPager(Request.RawUrl.ToString(), Constants.PageId, PageNo);
    }

    protected string GetUrlNext()
    {
        CurrentPage = CurrentPage + 1;
        if (CurrentPage > NoOfPages)
            return "#";
        else
            return CommonFunctions.AppendToUrl(Request.RawUrl.ToString(), Constants.PageId, CurrentPage.ToString());
    }

    protected string GetUrlPrevious()
    {
        int PreviousPage = CurrentPage -1;
        if (PreviousPage == 0)
            return "#";
        else
            return CommonFunctions.AppendToUrl(Request.RawUrl.ToString(), Constants.PageId, PreviousPage.ToString());
    }

    public int PageSize
    {
        get { return mPageSize; }
        set { mPageSize = value; }
    }
}
