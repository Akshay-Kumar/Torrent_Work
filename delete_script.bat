@echo off

rem Deleting Torrents Script
rem Only run if state is finished (11), delete torrent with all files
if %3%=="11" goto continue
GOTO:EOF

:continue 

rem Parameter usage: basedir torrent-name state kind [filename]
rem corresponds to uTorrents flags: %D %N %S %K %F %L
echo *********************************************
echo Run on %date% at %time%

set basedir=%1
set name=%2
set state=%3
set kind=%4
set filename=%5
set label=%6
set info_hash=%7
echo Input: %basedir% %name% %state% %kind% %filename% %label%

echo %date% at %time%: Handling torrent %name% >> C:\Users\AkshayKumar\Desktop\Torrent_Work\logs\deleted_torrents.txt

goto deleting
GOTO:EOF

:deleting
echo **Deleting
	if %kind%=="single" goto deletefile
	if %kind%=="multi" goto deleteall
	echo ERROR - Unrecognized kind (%kind%)
GOTO:EOF

:deletefile
echo Deleting file %basedir%\%filename%
del %basedir%\%filename%
GOTO:EOF

:deleteall
rem Simply some precautions here
if %basedir%=="G:\Media1" exit
if %basedir%=="G:\Music" exit
if %basedir%=="G:\Porn" exit
if %basedir%=="C:\" exit
echo Deleting directory %basedir% with all subcontent
rmdir /S /Q %basedir%
GOTO:EOF