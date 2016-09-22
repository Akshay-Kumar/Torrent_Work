echo off
echo ###########################################################################################################################
echo						Script for removing finished torrent file
echo ###########################################################################################################################
rem  call C:\Users\AkshayKumar\Desktop\Torrent_Work\ut_config\config.bat "%~1" "%~2" "%~3" "%~4" "%~5" "%~6" "%~7"
echo ****************************************************************************************************************************
echo Running script for removing finished torrent file : "%ut_title%"
echo ****************************************************************************************************************************
rem if %ut_state%==11 GOTO CONTINUE
GOTO CONTINUE
GOTO:EOF
rem Deleting Torrents Script
:CONTINUE
echo ****************************************************************************************************************************
echo Entering CONTINUE block ::  File Name : "%ut_title%"
echo ****************************************************************************************************************************
echo Checking if processing of media files is over
rem FINDSTR /L /C:"%ut_title%" "%MEDIA%"\amc-input.txt
FINDSTR /L /C:"%ut_title%" "%CURRENT_DIRECTORY%"\processed-files.txt
if %ERRORLEVEL%==0 echo Processing of files is over.Preparing files for cleanup...
rem GOTO REMOVE_TORRENT
GOTO TEST
if %ERRORLEVEL%==1 echo Processing of files still in progress. Retrying again in 10 secs...
TIMEOUT 10
GOTO CONTINUE
if %ERRORLEVEL%==1 goto EXIT
GOTO:EOF

:TEST
echo Entering TEST block 
filebot.launcher -script %CLEANER_PATH% --action test "%ut_dir%"
GOTO EXIT

:REMOVE_TORRENT 
rem Parameter usage: basedir torrent-name state kind [filename]
rem corresponds to uTorrents flags: %D %N %S %K %F %L
echo ****************************************************************************************************************************
echo Entering REMOVE_TORRENT block ::  File Name : "%ut_title%"
echo ****************************************************************************************************************************
echo Run on %date% at %time%
echo Input: %ut_dir% %ut_title% %ut_state% %ut_kind% %ut_file% %ut_label%
echo %date% at %time%: Handling torrent :: %ut_title%
call powershell -executionpolicy unrestricted -file %CURRENT_DIRECTORY%\Remove-Torrent.ps1 %ut_info_hash% %ut_state%
if %ERRORLEVEL%==0 echo Torrent removed successfully from bit-torrent client. Success...
if %ERRORLEVEL%==1 echo Error occurred while removing torrent from bit-torrent client. Error...
if %ERRORLEVEL%==0 GOTO DELETING
if %ERRORLEVEL%==1 GOTO ERROR
GOTO:EOF

:DELETING
echo **Deleting
	if "%ut_kind%"=="single" GOTO DELETE_FILE
	if "%ut_kind%"=="multi" GOTO DELETE_ALL
	echo ERROR - Unrecognized kind (%ut_kind%)
GOTO:EOF

:DELETE_FILE
echo ****************************************************************************************************************************
echo Entering DELETE_FILE block ::  File Name : "%ut_title%"
echo ****************************************************************************************************************************
echo Deleting file %ut_dir%\%ut_file%
if exist "%ut_dir%\%ut_file%" (
   del "%ut_dir%\%ut_file%" 
   if %ERRORLEVEL%==0 set SUCCESS_MSG="%ut_file% deleted successfully."
   echo %SUCCESS_MSG%
   GOTO FINISH
) else (
   set ERROR_MSG="File : {%ut_file%} does not exist."
   echo %ERROR_MSG%
   GOTO ERROR
)
GOTO:EOF

:DELETE_ALL
rem Simply some precautions here
echo ****************************************************************************************************************************
echo Entering DELETE_ALL block ::  File Name : "%ut_title%"
echo ****************************************************************************************************************************
if "%ut_dir%"=="G:\Media1" goto EXIT
if "%ut_dir%"=="G:\Music" goto EXIT
if "%ut_dir%"=="G:\Porn" goto EXIT
if "%ut_dir%"=="C:\" goto EXIT
echo Deleting directory %ut_dir% with all subcontent
if exist "%ut_dir%" (
   rmdir /S /Q "%ut_dir%"
   if %ERRORLEVEL%==0 set SUCCESS_MSG="%ut_dir% deleted successfully."
   echo %SUCCESS_MSG%
   GOTO FINISH
) else (
   set ERROR_MSG="Directory : {%ut_dir%} does not exist."
   echo %ERROR_MSG%
   GOTO ERROR
)
GOTO:EOF

:ERROR
echo ****************************************************************************************************************************
echo Entering ERROR block ::
echo ****************************************************************************************************************************
echo ERRORLEVEL : {%ERRORLEVEL%}-{%ERROR_MSG%} some error occurred while executing script view deleted.log for details.
call :unquote unquoted_err_msg %ERROR_MSG%
call powershell -executionpolicy unrestricted -file %CURRENT_DIRECTORY%\send-email.ps1 "%USER_NAME%" "%EMAIL_TO%" "%PASSWORD%" "%CURRENT_DIRECTORY%" "%PUSHBULLET_API_TOKEN%" "%ut_title%" "Error occurred while cleaning %ut_title%" "Message : [%unquoted_err_msg%]"
echo ****************************************************************************************************************************
echo Exiting ERROR block ::
echo ****************************************************************************************************************************
GOTO:EOF

:FINISH
echo ****************************************************************************************************************************
echo Entering FINISH block ::
echo ****************************************************************************************************************************
echo ERRORLEVEL : {%ERRORLEVEL%}-{%SUCCESS_MSG%} script executed successfully.
call :unquote unquoted_success_msg %SUCCESS_MSG%
call powershell -executionpolicy unrestricted -file %CURRENT_DIRECTORY%\send-email.ps1 "%USER_NAME%" "%EMAIL_TO%" "%PASSWORD%" "%CURRENT_DIRECTORY%" "%PUSHBULLET_API_TOKEN%" "%ut_title%" "FileBot finished cleaning %ut_title%" "Message : [%unquoted_success_msg%]"
echo ****************************************************************************************************************************
echo Exiting FINISH block ::
echo ****************************************************************************************************************************
exit /b 0

:UNQUOTE
set %1=%~2
goto :EOF

:EXIT
echo exiting script file.
exit /b 0