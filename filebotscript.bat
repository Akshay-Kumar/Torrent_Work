echo off
cls

:: Music is disabled but have included a location if you want to enable in the filebot script, however I don't like how it organises music.
:: Variables should not have a slash on the end - the variables have an explanation below.
:: Variables shoud avoid using spaces - only because I don't use them and haven't added relevant 
:: READY - DIR where files are downloaded to
:: FILMS TV MUSIC ANIME - These are the media locations

:: CONFIGURABLE BACK SLASH VARIABLES LISTED BELOW - NO SPACES IN FILENAMES

	:: AUTOMATEDATA - A location that can be used for storing file lists to stop duplication
		set AUTOMATEDATA=C:\Users\AkshayKumar\Downloads\Automatedata
	
	:: COMPLETETORRENT - DIR where files are downloaded to in uTorrent
		set COMPLETETORRENT=C:\Users\AkshayKumar\Downloads\Complete
	
	:: TEMPMEDIA - temporary folder where a copy of your downloads is made so that uTorrent doesn't lock files (will only copy new files)
		set TEMPMEDIA=C:\Users\AkshayKumar\Downloads\Temp
	
	:: MEDIA - The main directory of your media - I don't think this really matters as media locations are defined below, and duplicate list is not used in filebot
		set MEDIA=G:\Media1


:: CONFIGURABLE FORWARD SLASH VARIABLES LISTED BELOW

		set FILMS=G:/Media1/New Films
		set TV=G:/Media1/TV
		set MUSIC=G:/Media1/Music/New
		set ANIME=G:/Media1/Anime


echo.
echo  Create automated data and temp folders - ignore already exists errors
echo  #################################################################
echo.

mkdir %AUTOMATEDATA%
mkdir %TEMPMEDIA%



echo.
echo  Check if script is already running
echo  #################################################################
echo.

	:RUNNINGCHECK
	if exist %AUTOMATEDATA%\running.txt goto PLEASEHOLD
	GOTO:NEXTPLEASE

	:PLEASEHOLD

REM Hold for 30 seconds then re-check

	echo script running... waiting 30 seconds for re-try
	ping 1.1.1.1 /n 1 /w 30000 > NUL
	GOTO:RUNNINGCHECK

	:NEXTPLEASE

REM Create a file to flag script is running and proceed to run the rest of the script.

	echo This file just shows script is running, delete file if script is not running > %AUTOMATEDATA%\running.txt


echo.
echo  Get Dir listing and save to temporary text file so next time the same files are not copied
echo  #################################################################
echo.

	dir "%COMPLETETORRENT%" /b > "%AUTOMATEDATA%\templist.txt"

echo.
echo  Copy files to safe "TempTorrent" location excluding those present last time this was run
echo  #################################################################
echo.

	XCOPY "%COMPLETETORRENT%" "%TEMPMEDIA%" /C /S /H /Y /J /Exclude:%AUTOMATEDATA%\done.txt

REM 	Below line removed as date redundant following addition of exclude list, date string converts to US date format.
REM 	XCOPY "%COMPLETETORRENT%" "%TEMPMEDIA%" /C /S /H /Y /J /D:%date:~3,2%-%date:~0,2%-%date:~8,2%


echo.
echo  Delete old list and replace with temp list.
echo  #################################################################
echo.

	del "%AUTOMATEDATA%\done.txt"
	ren "%AUTOMATEDATA%\templist.txt" done.txt
 
echo.
echo  Run Filebot - unrecognised files will remain in the "TempTorrent" location
echo  #################################################################
echo.

:: If using XBMC, replace plex below with XBMC 

	"C:\Program Files\FileBot\filebot" -script fn:amc --output "%MEDIA%" --log-file amc.log --action move --conflict override -non-strict "%TEMPMEDIA%" --def music=y artwork=y plex=localhost deleteAfterExtract=y clean=y "seriesFormat=%TV%/{n}/{'S'+s}/{fn}" "animeFormat=%ANIME%/{n}/{fn}" "movieFormat=%FILMS%/{n} {y}/{fn}" "musicFormat=%MUSIC%/{n}/{fn}"

REM Delete running indicator file

	del "%AUTOMATEDATA%\running.txt"

pause

