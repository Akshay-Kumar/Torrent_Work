echo off
echo ###########################################################################################################################
echo 							Script for copying torrent file
echo ###########################################################################################################################
echo ****************************************************************************************************************************
echo Initializing...
echo Setting up local variables.
echo ****************************************************************************************************************************
set "ut_dir=%~1"
set "ut_file=%~2"
set "ut_kind=%~3"
set "ut_title=%~4"
set "ut_label=%~5"
set "ut_state=%~6"
set "ut_info_hash=%~7"
echo ****************************************************************************************************************************
echo Setting up global variables
call C:\Users\AkshayKumar\Desktop\Torrent_Work\ut_config\config.bat
echo Running some contingency checks
rem SET "ut_dir=%ut_dir:&=^&%"
rem SET "ut_file=%ut_file:&=^&%"
rem SET "ut_title=%ut_title:&=^&%"

echo ****************************************************************************************************************************
echo Running script for procssing of downloaded torrent file : "%ut_title%"
echo ****************************************************************************************************************************
if %ERRORLEVEL%==0 GOTO COPY_TORRENT
if %ERRORLEVEL%==1 GOTO ERROR
:COPY_TORRENT
echo ****************************************************************************************************************************
echo .:: Entering COPY_TORRENT block ::.
echo ****************************************************************************************************************************
echo ****************************************************************************************************************************
echo Starting filebot script for procssing of file : "%ut_title%"
echo ****************************************************************************************************************************
filebot.launcher -script %AMC_PATH% --output %MEDIA% --log-file %CURRENT_DIRECTORY%/amc.log --action copy --conflict override -non-strict --def "exec=echo {file} >> %CURRENT_DIRECTORY%/processed-files.txt" --def gmail=%USER_NAME%:%PASSWORD% --def mailto=%EMAIL_TO% --def pushbullet=%PUSHBULLET_API_TOKEN% --def subtitles=en --def music=y --def unsorted=y --def artwork=y --def storeReport=y --def reportError=y --def "ut_dir=%ut_dir%" "ut_file=%ut_file%" "ut_kind=%ut_kind%" "ut_title=%ut_title%" "ut_label=%ut_label%" "ut_state=%ut_state%" plex=%PLEX_URL% deleteAfterExtract=y clean=y excludeList=%EXCLUDE_LIST% "seriesFormat=%MEDIA%/%TV%/%SERIES_FORMAT%" "animeFormat=%MEDIA%/%ANIME%/%ANIME_FORMAT%" "movieFormat=%MEDIA%/%FILM%/%MOVIE_FORMAT%" "musicFormat=%MEDIA%/%MUSIC%/%MUSIC_FORMAT%"
if %ERRORLEVEL%==1 GOTO ERROR
rem if %ERRORLEVEL%==0 GOTO CLEANUP
echo ****************************************************************************************************************************
echo .:: Exiting COPY_TORRENT block ::.
echo ****************************************************************************************************************************
GOTO EXIT

:CLEANUP
rem call %CURRENT_DIRECTORY%/ut_amc_delete_automation.bat
GOTO EXIT

:ERROR
echo ****************************************************************************************************************************
echo .:: Entering ERROR block ::.
echo ****************************************************************************************************************************
echo ERRORLEVEL : {%ERRORLEVEL%} some error occurred while executing filebot script view amc.log for details.
echo ****************************************************************************************************************************
echo .:: Exiting ERROR block ::.
echo ****************************************************************************************************************************
GOTO EXIT

:EXIT
echo exiting script file.
exit /b 0
rem ###########################################################################################################################################################
rem version 1.0
rem "C:\Program Files\FileBot\filebot.launcher.exe" -script fn:amc --output "G:/Media1" --log-file amc.log --action copy --conflict override -non-strict --def pushbullet="o.hPQwUc0iauq8BzOFsArEXe8QLphYI9SS" --def subtitles=en --def music=y --def artwork=y --def storeReport=y --def --def reportError=y --def "ut_dir=%D" "ut_file=%F" "ut_kind=%K" "ut_title=%N" "ut_label=%L" "ut_state=%S" music=y artwork=y plex=localhost deleteAfterExtract=n clean=n excludeList=amc-input.txt "seriesFormat=G:/Media1/TV/{n}/{'S'+s}/{fn}" "animeFormat=G:/Media1/New Films/{n}/{fn}" "movieFormat=G:/Media1/New Films/{n} {y}/{fn}" "musicFormat=G:/Media1/Music/New/{n}/{album+'/'}{pi.pad(2)+'. '}{artist} - {t}"

rem version 1.1
rem filebot.launcher -script %AMC_PATH% --output %MEDIA% --log-file %CURRENT_DIRECTORY%/amc.log --action copy --conflict override -non-strict --def gmail=%USER_NAME%:%PASSWORD% --def mailto=%EMAIL_TO% --def pushbullet=%PUSHBULLET_API_TOKEN% --def subtitles=en --def music=y --def unsorted=y --def "exec=%CURRENT_DIRECTORY%/ut_amc_delete_automation.bat" --def artwork=y --def storeReport=y --def reportError=y --def "ut_dir=%ut_dir%" "ut_file=%ut_file%" "ut_kind=%ut_kind%" "ut_title=%ut_title%" "ut_label=%ut_label%" "ut_state=%ut_state%" plex=%PLEX_URL% deleteAfterExtract=y clean=y excludeList=%EXCLUDE_LIST% "seriesFormat=%MEDIA%/%TV%/%SERIES_FORMAT%" "animeFormat=%MEDIA%/%ANIME%/%ANIME_FORMAT%" "movieFormat=%MEDIA%/%FILM%/%MOVIE_FORMAT%" "musicFormat=%MEDIA%/%MUSIC%/%MUSIC_FORMAT%"
rem ############################################################################################################################################################