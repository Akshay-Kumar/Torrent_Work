Param(
    [alias("f")]
    $Name,
    [alias("d")]
    $Dir,
    [alias("n")]
    $Title,
    [alias("p")]
    $PreviousState,
    [alias("l")]
    $Label,
    [alias("t")]
    $InfoHash,
    [alias("i")]
    $StatusMessage,
    [alias("s")]
    $State,
    [alias("k")]
    $Kind)

$EmailFrom = "akki007.singh@gmail.com"
$EmailTo = "akki007.singh@gmail.com"
$Pass = "19233302@akc"
$Subject = "Torrent Finished Downloading!"
$EmailMessage = New-Object System.Net.Mail.MailMessage( $emailFrom , $emailTo )
$emailMessage.Subject = "Torrent Finished Downloading!"
$emailMessage.IsBodyHtml = $true
$emailMessage.Body = get-content "C:\Users\AkshayKumar\Desktop\Torrent_Work\TorrentFinished.html"
$emailMessage.Body = $emailMessage.Body -replace "!Title!", $Title
$emailMessage.Body = $emailMessage.Body -replace "!Dir!", $Dir
$emailMessage.Body = $emailMessage.Body -replace "!Name!", $Name
$emailMessage.Body = $emailMessage.Body -replace "!PreviousState!", $PreviousState
$emailMessage.Body = $emailMessage.Body -replace "!Label!", $Label
$emailMessage.Body = $emailMessage.Body -replace "!InfoHash!", $InfoHash
$emailMessage.Body = $emailMessage.Body -replace "!StatusMessage!", $StatusMessage
$emailMessage.Body = $emailMessage.Body -replace "!State!", $State
$emailMessage.Body = $emailMessage.Body -replace "!Kind!", $Kind

$SMTPServer = "smtp.gmail.com"
$SMTPClient = New-Object Net.Mail.SmtpClient($SmtpServer, 587)
$SMTPClient.EnableSsl = $true
$SMTPClient.Credentials = New-Object System.Net.NetworkCredential($EmailFrom, $Pass);
$SMTPClient.Send($emailMessage)