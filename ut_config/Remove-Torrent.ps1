$finishedStatuses = 4,5,7,8,11
if ($finishedStatuses -contains $args[1])
{
    $utorrentdll = Join-Path (Split-Path $MyInvocation.MyCommand.Path) 'UTorrentAPI.dll'
    [Reflection.Assembly]::LoadFile($utorrentdll) | Out-Null

    $utorrentclient = New-Object -TypeName UTorrentAPI.UTorrentClient -ArgumentList "http://127.0.0.1:8080/gui/","admin","33021923"
    $utorrentclient.Torrents.Remove($args[0])
    $utorrentclient.Dispose()
}