set PATH="C:\Program Files\FileBot"
set outputDir="G:/Media1"
set sourceDir="C:/Users/AkshayKumar/Downloads/Complete"
set fileName=""
filebot -script fn:amc --output %outputDir% --log-file amc.log --action copy --conflict override -non-strict --def "ut_dir=%sourceDir%" "ut_file=%fileName%" "ut_kind=multi" "ut_title=" "ut_label=Movie" "ut_state=5" music=n artwork=Y plex=localhost deleteAfterExtract=n clean=n excludeList=amc-input.txt "seriesFormat=G:/Media1/TV/{n}/{'S'+s}/{fn}" "animeFormat=G:/Media1/New Films/{n}/{fn}" "movieFormat=G:/Media1/New Films/{n} {y}/{fn}" "musicFormat=G:/Media1/Music/New/{n}/{album+'/'}{pi.pad(2)+'. '}{artist} - {t}"
call sub.bat