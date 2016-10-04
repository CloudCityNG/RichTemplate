<%@ Application Language="VB" %>
<%@ Import Namespace="System.Web.Http" %>
<%@ Import Namespace="System.Web.Routing" %>
<script RunAt="server">

    Sub Application_PreRequestHandlerExecute(ByVal sender As Object, ByVal e As EventArgs)

        'Function allows us to share Session's across different SubDomains, so we can get the currently logged in member from any site in different sub-domains
        If TypeOf (Context.Handler) Is IRequiresSessionState Or TypeOf (Context.Handler) Is IReadOnlySessionState Then

            ' Ensure ASP.NET Session Cookies are accessible throughout subdomains
            Dim strAspNetSessionID_CookieName As String = "ASP.NET_SessionId"
            If (Not Request.Cookies(strAspNetSessionID_CookieName) Is Nothing) AndAlso (Not Session Is Nothing) AndAlso (Not Session.SessionID Is Nothing) Then

                Response.Cookies(strAspNetSessionID_CookieName).Value = Session.SessionID
                Response.Cookies(strAspNetSessionID_CookieName).Domain = ConfigurationManager.AppSettings("SiteDomainForSessionSharing").ToString() ' Requires FULL-STOP Prefix to denote all sub domains
                Response.Cookies(strAspNetSessionID_CookieName).Path = "/" ' Default Session Cookie path Root

            End If
        End If

    End Sub

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        RegisterRoutes(RouteTable.Routes)
    End Sub

    Sub RegisterRoutes(ByVal routes As RouteCollection)
        '' MapRoute takes the following parameters, in order:
        '' (1) Route name
        '' (2) URL with parameters
        '' (3) Parameter defaults
        routes.Ignore("{resource}.axd/{*pathInfo}")
        routes.MapHttpRoute(name:="DefaultApi",
        routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = System.Web.Http.RouteParameter.Optional})

    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)

        ' Code that runs when an unhandled error occurs
        Dim ex As Exception = HttpContext.Current.Server.GetLastError().GetBaseException()
        RichTemplateCentralDAL.Error_LogError(ex)
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub

    Sub Application_EndRequest(ByVal sender As Object, ByVal e As EventArgs)
        ''check the page if, the page is an unauthenticated page do nothing (ALLOWED), if the user agent is the ultimateSearchIndexer do nothing(ALLOWED), else redirect to the login page
        'If (Response.StatusCode = 401 AndAlso Request.IsAuthenticated = False AndAlso (Not Request.UserAgent.ToLower() = ConfigurationManager.AppSettings("ActiveDirectory_UltimateSearchUserAgent").ToString().ToLower())) Then


        '    'The current request, the user is not authenticated
        '    Response.ClearContent()

        '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '    ' NOT REQUIRED, as site is hosted inside vpn, see next custom code below
        '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '    'Get the users IP Address if it exists in our WebserverIPAddresses configuration setting, then redirect to the windows authentication page, else redirect to the login.aspx page
        '    'Dim boolRequestCameFromIntranet As Boolean = False
        '    'Dim strUsersIPAddress As String = Request.UserHostAddress
        '    'Dim listIntranetIPAddressPrefix() As String = ConfigurationManager.AppSettings("IntranetIPAddressPrefix").ToString().Split("|")
        '    'For Each strIntranetIPAddressPrefix As String In listIntranetIPAddressPrefix
        '    '    If strUsersIPAddress.StartsWith(strIntranetIPAddressPrefix) Then
        '    '        boolRequestCameFromIntranet = True
        '    '        Exit For
        '    '    End If
        '    'Next

        '    'CUSTOM - as this site is hosted inside a vpn, so we can not do an ip address prefix check, instead we do a url check
        '    Dim boolRequestCameFromIntranet As Boolean = True
        '    'Dim strUsersRequestedUrlAddress As String = Request.Url.AbsoluteUri
        '    'Dim listIntranetExternalUrlPrefixPrefix() As String = ConfigurationManager.AppSettings("ActiveDirectory_IntranetExternalUrlPrefix").ToString().Split("|")
        '    'For Each strIntranetExternalUrlPrefixPrefix As String In listIntranetExternalUrlPrefixPrefix
        '    '    If strUsersRequestedUrlAddress.StartsWith(strIntranetExternalUrlPrefixPrefix) Then
        '    '        boolRequestCameFromIntranet = False
        '    '        Exit For
        '    '    End If
        '    'Next

        '    Dim strLoginPage As String = ""
        '    If boolRequestCameFromIntranet Then
        '        If (Not ConfigurationManager.AppSettings("ActiveDirectory_LoginPage_WindowsAuthentication") Is Nothing) AndAlso (ConfigurationManager.AppSettings("ActiveDirectory_LoginPage_WindowsAuthentication").ToString().Length > 0) Then
        '            strLoginPage = ConfigurationManager.AppSettings("ActiveDirectory_LoginPage_WindowsAuthentication").ToString()
        '            Server.Execute(strLoginPage)
        '        Else
        '            strLoginPage = ConfigurationManager.AppSettings("ActiveDirectory_LoginPage_FormsAuthentication").ToString()
        '            Server.Execute(strLoginPage)
        '        End If

        '    Else
        '        strLoginPage = ConfigurationManager.AppSettings("ActiveDirectory_LoginPage_FormsAuthentication").ToString()
        '        Response.Redirect(strLoginPage)

        '    End If
        '    End If

    End Sub


</script>
