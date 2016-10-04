<%@ Page language="c#" AutoEventWireup="true" Inherits="Karamasoft.WebControls.UltimateSearch.Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
	<title>UltimateSearch Admin Page</title>
	<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<style>
		.ButtonText { PADDING-LEFT: 10px; TEXT-ALIGN: left }
	</style>
</head>
<body>
    <form id="form1" runat="server">
	<table cellSpacing="0" cellPadding="0" border="0">
		<tr>
			<td><img src="Karamasoft.gif" width="215" height="40">
				<span style="MARGIN-LEFT: 70px; FONT-WEIGHT: bold; FONT-SIZE: 16px; COLOR: steelblue; FONT-FAMILY: Verdana">UltimateSearch Admin Page<br /><br /></span>
			</td>
		</tr>
		<tr height="30px">
			<td>
				<asp:Button id="btnIndexFull" CssClass="ButtonText" runat="server" Text="Index Full" Width="160px"></asp:Button>
				<asp:Button id="btnStopIndexing" CssClass="ButtonText" runat="server" Text="Stop Indexing" Width="160px" Enabled="False"></asp:Button> 
				<asp:Button id="btnDisplayIndexedPages" CssClass="ButtonText" runat="server" Text="Display Indexed Pages"
							Width="160px"></asp:Button>
			</td>
		</tr>
		<tr height="30px">
			<td>
				<asp:Button id="btnIndexIncremental" CssClass="ButtonText" runat="server" Text="Index Incremental" Width="160px"></asp:Button>
				<asp:Button id="btnLoadCopiedIndex" CssClass="ButtonText" runat="server" Text="Load Copied Index" Width="160px"></asp:Button>
				<asp:Button id="btnDisplayIndexedWords" CssClass="ButtonText" runat="server" Text="Display Indexed Words" Width="160px"></asp:Button>
			</td>
		</tr>
		<tr>
			<td style="color:red">
				<br />
				<asp:Label id="lblStatus" runat="server"></asp:Label>
				<br /><br />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:DataGrid id="dgDisplay" runat="server" AllowSorting="True" PageSize="100" AllowPaging="True"
							BorderColor="Black" BorderWidth="1px" CellPadding="2" Font-Size="X-Small" Font-Names="Arial">
							<AlternatingItemStyle BackColor="LightGray"></AlternatingItemStyle>
							<HeaderStyle Font-Bold="True" BackColor="LightYellow"></HeaderStyle>
							<PagerStyle Position="TopAndBottom" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid>
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
