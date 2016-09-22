echo off
cls
set DOWNLOAD_DIR="C:\Users\AkshayKumar\Downloads"
set TORRENT_DIR="C:\Users\AkshayKumar\Downloads\Torrents"
:backup
move %DOWNLOAD_DIR%\*.torrent %TORRENT_DIR%
timeout 5
goto backup