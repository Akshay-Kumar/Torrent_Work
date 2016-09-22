Param
(
[string]$EmailFrom, 
[string]$EmailTo, 
[string]$AppKey, 
[string]$FileName, 
[string]$Message,
[string]$Status
)
$EmailMessage = New-Object System.Net.Mail.MailMessage($EmailFrom, $EmailTo)
$emailMessage.IsBodyHtml = $true
$emailMessage.Subject = "$FileName - $Status"
$emailMessage.Body = get-content "C:\Users\AkshayKumar\Desktop\Torrent_Work\ut_config\template.html"
$emailMessage.Body = $emailMessage.Body -replace "-FileName-", $FileName
$emailMessage.Body = $emailMessage.Body -replace "-Status-", $Status
$emailMessage.Body = $emailMessage.Body -replace "-Message-", $Message
$SMTPServer = "smtp.gmail.com"
$SMTPClient = New-Object Net.Mail.SmtpClient($SmtpServer, 587)
$SMTPClient.EnableSsl = $true
$SMTPClient.Credentials = New-Object System.Net.NetworkCredential($EmailFrom, $AppKey);
$SMTPClient.Send($emailMessage)