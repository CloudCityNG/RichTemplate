<!--#INCLUDE FILE="../db_connection.asp"-->

<%	

'------------------------->> Open Database Connection <<-------------------------	

	set Con = Server.CreateObject("adodb.connection")
	con.open ConnectionString

'------------------------->> Pull banner information from rotator module <<-------------------------	

    BannerSQL = "Select  * FROM BANNER_MODULE where pageID = "&Request.Querystring("ID")&""

	'RESPONSE.WRITE BannerSQL
	SET RS = CON.EXECUTE (BannerSQL )
	
	IF NOT RS.EOF THEN
	
'------------------------->> Display rotating banner at set interval <<-------------------------	


		If Session("platform") = 1 then
			currentDate = Now()
			currentDate	= "#"&CurrentDate&"#"
		else
			currentDate =GetDate()
		end if
	
'------------------------->> Define Season Start <<-------------------------	
	
		SpringStart = "3/20/"&year(date)
		SpringStart = "#"&SpringStart&"#"
		
		SummerStart = "6/21/"&year(date)
		SummerStart	= "#"&SummerStart&"#"
		
		FallStart	= "9/22/"&year(date)
		FallStart	= "#"&FallStart&"#"
		
		WinterStart	= "12/21/"&year(date)
		WinterStart	= "#"&WinterStart&"#"
		
		WinterEnd	= "3/19/"&year(date)+1
		WinterEnd	= "#"&WinterEnd&"#"
		
'------------------------->> Define Season End <<-------------------------	

'------------------------->> Call Banner For Quartly Display <<-------------------------	

		if currentDate >= SpringStart and CurrentDate < SummerStart then
			
			    BannerRank = "Select  * FROM BANNER_MODULE where rank = 1 and pageID = "&Request.Querystring("id")&""
		
		elseif currentDate >= SummerStart and currentDate < FallStart then
		
			    BannerRank = "Select  * FROM BANNER_MODULE where rank = 2 and pageID = "&Request.Querystring("id")&""
		
		elseif currentDate >= FallStart and currentDate < WinterStart then
		
			    BannerRank = "Select  * FROM BANNER_MODULE where rank = 3 and pageID = "&Request.Querystring("id")&""

		elseif currentDate >= WinterStart and currentDate <= WinterEnd then
		
			    BannerRank = "Select  * FROM BANNER_MODULE where rank = 4 and pageID = "&Request.Querystring("id")&""
				SET RS = CON.EXECUTE (BannerRank)
				If NOT RS.EOF THEN
				
					Response.Write "<img border='0' src='data/images/banners/"&RS("BANNER_NAME")&"'>"
				
				ELSE
				
					Response.Write "<img border='0' src='data/images/banners/default_banner.jpg'>"
				
				END IF


		
		END IF
	
'------------------------->> Call Banner if none is selected <<-------------------------	
	
	ELSE
		
		Response.Write "<img border='0' src='data/images/banners/default_banner.jpg'>"
		
   END IF%>