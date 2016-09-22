@echo off
title Jamie's torrent-file script
rem Based on: http://forum.utorrent.com/viewtopic.php?id=110024
rem Parameter usage: fromdir torrent-name label kind [filename]
rem corresponds to uTorrents flags: %D %N %L %K %F 
echo *********************************************
echo Run on %date% at %time%

set fromdir=%1
set name=%2
set label=%3
set kind=%4
set filename=%5
set savepartition=F:\Media
set gamesavedir=C:\Users\Jamie\Downloads\Completed
set sickbeardscript="C:\Development\Scripts\utorrent\autoProcessTV\sabToSickBeard.py"
set wget="C:\Program Files\GnuWin32\bin\wget.exe"
set winrar="c:\program files\winrar\winrar.exe"
set torrentlog=C:\Development\Scripts\utorrent\torrentlog.txt
set handledlog=C:\Development\Scripts\utorrent\handled_torrents.txt
set sickbeardlog=C:\Development\Scripts\utorrent\SickbeardLog.txt
set errorlog=C:\Development\Scripts\utorrent\ErrorLog.txt
set label_prefix=""

echo Input: %fromdir% %name% %label% %kind% %filename%

rem Check if the label has a sub label by searching for \
if x%label:\=%==x%label% goto skipsublabel
rem Has a sub label so split into prefix and suffix so we can process properly later
echo sub label
for /f "tokens=1,2 delims=\ " %%a in ("%label%") do set label_prefix=%%a&set label_suffix=%%b
rem add the removed quote mark
set label_prefix=%label_prefix%"
set label_suffix="%label_suffix%
echo.prefix  : %label_prefix%
echo.suffix  : %label_suffix%
goto:startprocess

:skipsublabel
echo Skipped Sub Label
goto:startprocess

:startprocess
echo %date% at %time%: Handling %label% torrent %name% >> %handledlog%

rem Process the label
if %label%=="Movies" goto known
if %label%=="AV" goto known
if %label%=="MMA" goto known
if %label%=="Software" goto known
if %label%=="UNPACK" goto known
if %label_prefix%=="Training" goto known
if %label%=="Games" goto other
if %label%=="TV" goto TV


rem Last resort
rem Double underscores so the folders are easier to spot (listed on top in explorer)
echo Last Resort
set todir=%savepartition%\Unsorted\__%name%
if %kind%=="single" goto copyfile
if %kind%=="multi" goto copyall
GOTO:EOF

:known
echo **Known Download Type - %label%
set todir=%savepartition%\%label%\%name%
echo todir = %todir%
GOTO:process

:other
rem Handle downloads that aren't going into my main save directory
rem add as required
echo **Other Download Type - %label%
if %label%==Games set todir=%gamesavedir%\%name%
echo todir = %todir%
GOTO:process

:TV
echo **Known Download Type - %label%
REM as it's TV I need to keep the directory name to give sickbeard the best chance of successfully processing
REM Strip out the last part of directory name and use it to create the todir name
for /D %%i in (%fromdir%) do set todir=%savepartition%\Unsorted\%%~ni%%~xi
echo todir = %todir%
GOTO:process

:process
rem If there are rar files in the folder, extract them.
rem If there are mkvs, copy them. Check for rars first in case there is a sample.mkv, then we want the rars
if %kind%=="single" goto copyfile
if exist %fromdir%\*.rar goto extractrar
if exist %fromdir%\*.mkv goto copymkvs
if %kind%=="multi" goto copyall
echo Guess we didnt find anything
GOTO:EOF

:copyall
echo **Type unidentified so copying all
echo Copy all contents of %fromdir% to %todir%
xcopy %fromdir%\*.* %todir% /S /I /Y
GOTO:POSTPROCESSING

:copyfile
rem Copies single file from fromdir to todir
echo Single file so just copying
echo Copy %filename% from %fromdir% to %todir%
xcopy %fromdir%\%filename% %todir%\ /S /Y
GOTO:POSTPROCESSING

:copymkvs
echo Copy all mkvs from %fromdir% and subdirs to %todir%
xcopy %fromdir%\*.mkv %todir% /S /I /Y
GOTO:POSTPROCESSING

:extractrar
echo Extracts all rars in %fromdir% to %todir%. 
rem Requires WinRar installed to c:\Program files
if not exist %todir% mkdir %todir%
IF EXIST %fromdir%\subs xcopy %fromdir%\subs %todir% /S /I /Y
IF EXIST %fromdir%\subtitles xcopy %fromdir%\subtitles %todir% /S /I /Y
call %winrar% x %fromdir%\*.rar *.* %todir% -IBCK  -ilog"%todir%\RarErrors.log"
IF EXIST %fromdir%\*.nfo xcopy %fromdir%\*.nfo %todir% /S /I /Y
GOTO:POSTPROCESSING

:POSTPROCESSING
rem If the file is media Then update Plex. 
rem Not used for other file types at this point but it might be expanded
rem update the plex section. For more info see http://wiki.plexapp.com/index.php/PlexNine_AdvancedInfo#Terminal_Commands
if %label%=="MMA" call %wget% -O NUL http://localhost:32400/library/sections/11/refresh?deep=1
if %label%=="Movies" call %wget% -O NUL http://localhost:32400/library/sections/3/refresh?deep=1
if %label%=="TV" GOTO:tvpostprocessing
GOTO:EOF

:tvpostprocessing
echo
echo ****************BEGIN SICKBEARD PROCESSING  >> %sickbeardlog%
echo %date% at %time%: Handling %name% from %todir%  >> %sickbeardlog%
call python %sickbeardscript% %todir% >> %sickbeardlog%
REM Check the sickbeard log for errors
REM Count the number of lines so we can show just the last line in the main log
for /f "tokens=3" %%i in ('find /v /c "Wont@findthisin#anyfile" %sickbeardlog%') do set countoflines=%%i
set /a countoflines=countoflines-1
echo Sickbeard processing result:
echo | more +%countoflines% < %sickbeardlog%

REM Check the last line again to check result
SET sbresult=SUCCEEDED
echo | more +%countoflines% < %sickbeardlog% | FIND "Processing succeeded" > NUL
IF ERRORLEVEL 1 SET sbresult=FAILED
ECHO Sickbeard processing %sbresult% 
REM The sickbeard processing failed so write to the error log and open the file
IF %sbresult%==FAILED ECHO %date% at %time%: Sickbeard processing %sbresult%. Handling %filename% from %todir%.  Check the main logs >> %errorlog%
IF %sbresult%==FAILED start notepad %errorlog%
REM Update Plex TV section
call %wget% -O NUL http://localhost:32400/library/sections/10/refresh?deep=1
GOTO:EOF