echo off
echo ###########################################################################################################################
echo 							Script for copying torrent file
echo ###########################################################################################################################
echo ****************************************************************************************************************************
echo Initializing...
echo Setting up local variables.
echo ****************************************************************************************************************************
call C:\Users\AkshayKumar\Desktop\Torrent_Work\ut_config\config.bat "%~1" "%~2" "%~3" "%~4" "%~5" "%~6" "%~7"
echo ****************************************************************************************************************************
echo Running script for procssing of downloaded torrent file : "%ut_title%"
echo ****************************************************************************************************************************
if %ERRORLEVEL%==0 GOTO COPY_TORRENT
if %ERRORLEVEL%==1 GOTO ERROR
:COPY_TORRENT
echo ****************************************************************************************************************************
echo Starting filebot script for procssing of file : "%ut_title%"
echo ****************************************************************************************************************************
filebot.launcher -script fn:amc --output %MEDIA% --log-file %CURRENT_DIRECTORY%/amc.log --action copy --conflict override -non-strict --def gmail=%USER_NAME%:%PASSWORD% --def mailto=%EMAIL_TO% --def pushbullet=%PUSHBULLET_API_TOKEN% --def subtitles=en --def music=y --def unsorted=y --def "exec=%CURRENT_DIRECTORY%/ut_amc_delete_automation.bat" --def artwork=y --def storeReport=y --def reportError=y --def "ut_dir=%ut_dir%" "ut_file=%ut_file%" "ut_kind=%ut_kind%" "ut_title=%ut_title%" "ut_label=%ut_label%" "ut_state=%ut_state%" plex=%PLEX_URL% deleteAfterExtract=y clean=y excludeList=%EXCLUDE_LIST% "seriesFormat=%MEDIA%/%TV%/%SERIES_FORMAT%" "animeFormat=%MEDIA%/%ANIME%/%ANIME_FORMAT%" "movieFormat=%MEDIA%/%FILM%/%MOVIE_FORMAT%" "musicFormat=%MEDIA%/%MUSIC%/%MUSIC_FORMAT%" 
if %ERRORLEVEL%==1 GOTO ERROR
if %ERRORLEVEL%==1 echo Some error occurred while running filebot script.
GOTO:EOF

:ERROR
echo ****************************************************************************************************************************
echo Entering ERROR block ::
echo ****************************************************************************************************************************
echo ERRORLEVEL : {%ERRORLEVEL%} some error occurred while executing filebot script view amc.log for details.
echo ****************************************************************************************************************************
echo Exiting ERROR block ::
echo ****************************************************************************************************************************
GOTO:EOF

rem ###########################################################################################################################################################
rem "C:\Program Files\FileBot\filebot.launcher.exe" -script fn:amc --output "G:/Media1" --log-file amc.log --action copy --conflict override -non-strict --def pushbullet="o.hPQwUc0iauq8BzOFsArEXe8QLphYI9SS" --def subtitles=en --def music=y --def artwork=y --def storeReport=y --def --def reportError=y --def "ut_dir=%D" "ut_file=%F" "ut_kind=%K" "ut_title=%N" "ut_label=%L" "ut_state=%S" music=y artwork=y plex=localhost deleteAfterExtract=n clean=n excludeList=amc-input.txt "seriesFormat=G:/Media1/TV/{n}/{'S'+s}/{fn}" "animeFormat=G:/Media1/New Films/{n}/{fn}" "movieFormat=G:/Media1/New Films/{n} {y}/{fn}" "musicFormat=G:/Media1/Music/New/{n}/{album+'/'}{pi.pad(2)+'. '}{artist} - {t}"
rem ############################################################################################################################################################