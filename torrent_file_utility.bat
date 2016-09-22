@echo off
rem Parameter usage: fromdir torrent-name label kind [filename]
rem corresponds to uTorrents flags: %D %N %L %K %F 
echo *********************************************
echo Run on %date% at %time%
set filename=%1
set fromdir=%2
set name=%3
set prev_state=%4
set label=%5
set info_hash=%6
set message=%7
set state=%8
set kind=%9

set savepartition=G:
set moviedir=%savepartition%\Movies
set musicdir=%savepartition%\Music
set label_prefix=""
echo Input: %fromdir% %name% %label% %kind% %filename% %prev_state% %info_hash% %message% %state%

rem Determine length of label string
echo.%label%>len
for %%a in (len) do set /a len=%%~za -2
rem This is to pick up if label begins with "Shows" (ex. "Shows\Boardwalk Empire")
if %len% geq 7 set label_prefix=%label:~1,5%
echo label_prefix = %label_prefix%

echo %date% at %time%: Handling torrent %name% >> C:\Users\AkshayKumar\Desktop\Torrent_Work\logs\handled_torrents.txt

rem If label is "Movies"
if %label%=="Movies" goto movies

rem If label starts with "Shows"
if %label_prefix%==Shows goto shows

rem If label is "Music"
if %label%=="Music" goto copymp3s

rem If there are mp3 files in fromdir and it is a multi-file torrent
if exist %fromdir%\*.mp3 if %kind%=="multi" goto copymp3s

rem Last resort
rem Double underscores so the folders are easier to spot (listed on top in explorer)
if exist "%name%\" set todir=%savepartition%\Unsorted\__%name%
else set todir=%savepartition%\Unsorted\%name:~0,-4%
if %kind%=="single" goto copyfile
if %kind%=="multi" goto copyall

GOTO:EOF

rem xcopy switches:
rem S Copies directories and subdirectories except empty ones.
rem I If destination does not exist and copying more than one file, assumes that destination must be a directory.
rem L Displays files that would be copied. FOR DEBUG
rem Y Suppresses prompting to confirm you want to overwrite an existing destination file.

:movies
echo **Movie
set todir=%moviedir%\%name%
echo todir = %todir%
rem If there are rar files in the folder, extract them.
rem If there are mkvs, copy them. Check for rars first in case there is a sample.mkv, then we want the rars
if %kind%=="single" goto copyfile
if exist %fromdir%\*.rar goto extractrar
if exist %fromdir%\*.mkv goto copymkvs
echo Guess we didnt find anything
GOTO:EOF

:shows
echo **Show
set todir=%savepartition%\%label%
if %kind%=="single" goto copyfile
if exist %fromdir%\*.rar goto extractrar
if exist %fromdir%\*.mkv goto copymkvs
echo Guess we didnt find anything
GOTO:EOF

:copymp3s
echo **Music
set todir=%musicdir%\%name%
if %kind%=="single" goto copyfile
echo Copy all mp3s from %fromdir% and subdirs to %musicdir%\%name%
xcopy %fromdir%\*.mp3 %todir% /S /I /Y
goto send_email
GOTO:EOF

:copyall
echo **Undefined
echo Copy all contents of %fromdir% to %todir%
xcopy %fromdir%\*.* %todir% /S /I /Y
goto send_email
GOTO:EOF

:copyfile
rem Copies single file from fromdir to todir
echo Copy %filename% from %fromdir% to %todir%
xcopy %fromdir%\%filename% %todir%\ /S /Y
goto send_email
GOTO:EOF

:copymkvs
echo Copy all mkvs from %fromdir% and subdirs to %todir%
xcopy %fromdir%\*.mkv %todir% /S /I /Y
goto send_email
GOTO:EOF

:extractrar
echo Extracts all rars in %fromdir% to %todir%. 
rem Requires WinRar installed to c:\Program files
if not exist %todir% mkdir %todir%
set PATH=%PATH%;"C:\Program Files\7-Zip\"
start 7z x %fromdir%\*.rar -o%todir% * -r
goto send_email
GOTO:EOF

:send_email
call C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe -executionpolicy unrestricted -file "C:\Users\AkshayKumar\Desktop\Torrent_Work\TorrentFinished.ps1" -Name "%filename%" -Dir "%fromdir%" -Title "%name%" -PreviousState "%prev_state%" -Label "%label%" -InfoHash "%info_hash%" -StatusMessage "%message%" -State "%state%" -Kind "%kind%"
call C:\Users\AkshayKumar\Desktop\Torrent_Work\delete_script.bat "%fromdir%" "%name%" "%state%" "%kind%" "%filename%" "%label%" "%info_hash%" >> C:\Users\AkshayKumar\Desktop\Torrent_Work\logs\deletelog.txt
start C:\Users\AkshayKumar\Desktop\Torrent_Work\TextToSpeech.exe "%filename%"" finished downloading."
GOTO:EOF