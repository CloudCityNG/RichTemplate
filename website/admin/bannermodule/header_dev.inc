<!--#INCLUDE FILE="../db_connection.asp"-->
<%

'------------------------->> Assign Default Banner <<-------------------------	

	default_banner = "data/images/banners/default_banner.jpg"

'------------------------->> Assign Default Banner <<-------------------------	



'------------------------->> Open Database Connection <<-------------------------	

	set Con = Server.CreateObject("adodb.connection")
	con.open ConnectionString

'------------------------->> Pull banner information from rotator module <<-------------------------	


	If Request.Querystring("page")="" then
	
	Response.Write "<img border='0' src='"&default_banner&"'>"
	
	Else

	

    BannerSQL = "Select  * FROM BANNER_MODULE where pageID = "&Request.Querystring("page")&""
	SET RS = CON.EXECUTE (BannerSQL )
	
	IF NOT RS.EOF THEN
		
		
		If Session("platform") = 1 then
			currentDate = Now()
			currentDate	= "#"&CurrentDate&"#"
		else
			currentDate =GetDate()
		end if
		
	
	If RS("ROTATION")= "1" THEN
	

randomize
random_number = int(rnd*7)+1
response.write random_number


	
	
	BannerSQL = "Select  * FROM BANNER_MODULE where rank = "&random_number&" and pageID = "&Request.Querystring("page")&""
	SET RS2 = CON.EXECUTE (BannerSQL )
	If NOT RS2.EOF THEN
	
	Response.Write "<img border='0' src='data/images/banners/"&RS2("BANNER_NAME")&"'>"
				
	ELSE
				
		Response.Write "<img border='0' src='"&default_banner&"'>"
				
	END IF
	
	
	ELSEIF RS("ROTATION")="2" THEN
	
	Sunday = 1
	Monday = 2
	Tuesday = 3
	Wednesday = 4
	Thursday = 5
	Friday = 6
	Saturday = 7
	
	DayOfTheWeekName = WeekdayName(Weekday(Date)) 
	If DayOfTheWeekName = "Sunday" then
		
		BannerSQL = "Select  * FROM BANNER_MODULE where rank = 1 and pageID = "&Request.Querystring("page")&""

	elseif DayOfTheWeekName = "Monday" then
		
		BannerSQL = "Select  * FROM BANNER_MODULE where rank = 2 and pageID = "&Request.Querystring("page")&""
		
	elseif DayOfTheWeekName = "Tuesday" then

		BannerSQL = "Select  * FROM BANNER_MODULE where rank = 3 and pageID = "&Request.Querystring("page")&""

	elseif DayOfTheWeekName = "Wednesday" then

		BannerSQL = "Select  * FROM BANNER_MODULE where rank = 4 and pageID = "&Request.Querystring("page")&""

	elseif DayOfTheWeekName = "Thursday" then

		BannerSQL = "Select  * FROM BANNER_MODULE where rank = 5 and pageID = "&Request.Querystring("page")&""
		
	elseif DayOfTheWeekName = "Friday" then

		BannerSQL = "Select  * FROM BANNER_MODULE where rank = 6 and pageID = "&Request.Querystring("page")&""

	elseif DayOfTheWeekName = "Saturday" then

		BannerSQL = "Select  * FROM BANNER_MODULE where rank = 7 and pageID = "&Request.Querystring("page")&""

	end if

	SET RS2 = CON.EXECUTE (BannerSQL )
	If NOT RS2.EOF THEN
	
	Response.Write "<img border='0' src='data/images/banners/"&RS2("BANNER_NAME")&"'>"
				
	ELSE
				
		Response.Write "<img border='0' src='"&default_banner&"'>"
				
	END IF
	
	
		
	ELSEIF RS("ROTATION")="3" THEN
	
	weekNumber =  DatePart("ww", Date())
	
	IF weekNumber = 1 or weekNumber = 13 or weekNumber = 25 or weekNumber = 37 or weekNumber = 49 then
		
		BannerRank = "Select  * FROM BANNER_MODULE where rank = 1 and pageID = "&Request.Querystring("page")&""
	
	Elseif weekNumber = 2 or weekNumber = 14 or weekNumber = 26 or weekNumber = 38 or weekNumber = 50 then
	
		BannerRank = "Select  * FROM BANNER_MODULE where rank = 2 and pageID = "&Request.Querystring("page")&""
		
	Elseif weekNumber = 3 or weekNumber = 15 or weekNumber = 27 or weekNumber = 39 or weekNumber = 51 then
	
		BannerRank = "Select  * FROM BANNER_MODULE where rank = 3 and pageID = "&Request.Querystring("page")&""
		
	Elseif weekNumber = 4 or weekNumber = 16 or weekNumber = 28 or weekNumber = 40 or weekNumber = 52 then
	
		BannerRank = "Select  * FROM BANNER_MODULE where rank = 4 and pageID = "&Request.Querystring("page")&""
		
	Elseif weekNumber = 5 or weekNumber = 17 or weekNumber = 29 or weekNumber = 41 then
	
		BannerRank = "Select  * FROM BANNER_MODULE where rank = 5 and pageID = "&Request.Querystring("page")&""
		
	Elseif weekNumber = 6 or weekNumber = 18 or weekNumber = 30 or weekNumber = 42 then
	
		BannerRank = "Select  * FROM BANNER_MODULE where rank = 6 and pageID = "&Request.Querystring("page")&""
		
	Elseif weekNumber = 7 or weekNumber = 19 or weekNumber = 31 or weekNumber = 43 then
	
		BannerRank = "Select  * FROM BANNER_MODULE where rank = 7 and pageID = "&Request.Querystring("page")&""
		
	Elseif weekNumber = 8 or weekNumber = 20 or weekNumber = 32 or weekNumber = 44 then
	
		BannerRank = "Select  * FROM BANNER_MODULE where rank = 8 and pageID = "&Request.Querystring("page")&""
		
	Elseif weekNumber = 9 or weekNumber = 21 or weekNumber = 33 or weekNumber = 45 then
	
		BannerRank = "Select  * FROM BANNER_MODULE where rank = 9 and pageID = "&Request.Querystring("page")&""
		
	Elseif weekNumber = 10 or weekNumber = 22 or weekNumber = 34 or weekNumber = 46 then
	
		BannerRank = "Select  * FROM BANNER_MODULE where rank = 10 and pageID = "&Request.Querystring("page")&""

	Elseif weekNumber = 11 or weekNumber = 23 or weekNumber = 35 or weekNumber = 47 then
	
		BannerRank = "Select  * FROM BANNER_MODULE where rank = 11 and pageID = "&Request.Querystring("page")&""

	Elseif weekNumber = 12 or weekNumber = 24 or weekNumber = 36 or weekNumber = 48 then
	
		BannerRank = "Select  * FROM BANNER_MODULE where rank = 12 and pageID = "&Request.Querystring("page")&""

	End if
					
		SET RS = CON.EXECUTE(BannerRank)
		If NOT RS.EOF THEN
				
			Response.Write "<img border='0' src='data/images/banners/"&RS("BANNER_NAME")&"'>"
				
		ELSE
				
			Response.Write "<img border='0' src='"&default_banner&"'>"
				
		END IF







	ELSEIF RS("ROTATION")="4" THEN
	
	currentMonth = DatePart("m", Date()) 
'	response.write currentMonth


		
	BannerSQL = "Select  * FROM BANNER_MODULE where rank = "&currentMonth&" and pageID = "&Request.Querystring("page")&""
	SET RS2 = CON.EXECUTE (BannerSQL )
	If NOT RS2.EOF THEN
	
	Response.Write "<img border='0' src='data/images/banners/"&RS2("BANNER_NAME")&"'>"
				
	ELSE
				
		Response.Write "<img border='0' src='"&default_banner&"'>"
				
	END IF
	
	
	

	ELSEIF RS("ROTATION")="5" THEN
		

	
'------------------------->> Define Quarters Start <<-------------------------	
	
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
		
'------------------------->> Define Quarters End <<-------------------------	

'------------------------->> Call Banner For Quartly Display <<-------------------------	

		if currentDate >= SpringStart and CurrentDate < SummerStart then
			
			    BannerRank = "Select  * FROM BANNER_MODULE where rank = 1 and pageID = "&Request.Querystring("page")&""
		
		elseif currentDate >= SummerStart and currentDate < FallStart then
		
			    BannerRank = "Select  * FROM BANNER_MODULE where rank = 2 and pageID = "&Request.Querystring("page")&""
		
		elseif currentDate >= FallStart and currentDate < WinterStart then
		
			    BannerRank = "Select  * FROM BANNER_MODULE where rank = 3 and pageID = "&Request.Querystring("page")&""

		elseif currentDate >= WinterStart and currentDate <= WinterEnd then
		
			    BannerRank = "Select  * FROM BANNER_MODULE where rank = 4 and pageID = "&Request.Querystring("page")&""
		end if		
				SET RS = CON.EXECUTE (BannerRank)
				If NOT RS.EOF THEN
				
					Response.Write "<img border='0' src='data/images/banners/"&RS("BANNER_NAME")&"'>"
				
				ELSE
				
					Response.Write "<img border='0' src='"&default_banner&"'>"
				
				END IF




	ELSEIF RS("ROTATION")="6" THEN
	

			myRSSQL = "Select * from BANNER_MODULE WHERE  PAGEID=" & request.querystring("PAGE") & ""
			SET RS = con.execute(myRSSQL)
	
			If Not RS.EOF then
			
    				'Banner image found - pull image from db
					Response.Write "<img border='0' src='data/images/banners/"&RS("BANNER_NAME")&"'>"
    		
			Else
					Response.Write "<img border='0' src='"&default_banner&"'>"
    		
    		end if
	
	
	ELSE
	
				myRSSQL = "Select * from BANNER_MODULE WHERE  PAGEID=" & request.querystring("PAGE") & ""
			SET RS = con.execute(myRSSQL)
	
			If Not RS.EOF then
			
    				'Banner image found - pull image from db
					Response.Write "<img border='0' src='data/images/banners/"&RS("BANNER_NAME")&"'>"
    		
			Else
					Response.Write "<img border='0' src='"&default_banner&"'>"
    		
    		end if
	
	END IF
	end if

end if
%>
   
