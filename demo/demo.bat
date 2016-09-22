FINDSTR /L /C:"quick brown" Demo.txt
if 1==1 if %errorlevel%==0 echo "correct"
if %errorlevel%==1 echo "false"