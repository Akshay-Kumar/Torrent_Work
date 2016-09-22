amcscript
=========

Automated Media Center script for filebot

To execute the script I'm using xargs mainly because it's a lot easier to run and modify without the need to escape the agruments every time:
cat amc.arguments | tr '\n' '\0' | xargs -0 filebot
A file called amc.arguments must be created

To confige your OS account and login:
filebot -script fn:osdb.login

To check OpenSubtitles quota after configuring your account run:
filebot -script fn:osdb.stats

To revert your last rename job:
filebot -script fn:revert /path/to/files