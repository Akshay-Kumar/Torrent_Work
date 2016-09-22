Param
(
[string]$EmailFrom, 
[string]$EmailTo, 
[string]$AppKey,
[string]$Pushbullet,
[string]$PushbulletAppKey,
[string]$FileName, 
[string]$Message,
[string]$Status
)
$msgTemplatePath = "C:\Users\AkshayKumar\Desktop\Torrent_Work\ut_config\template.html"
$emailBody = get-content $msgTemplatePath -Raw
$emailBody = $emailBody -replace "-FileName-", $FileName
$emailBody = $emailBody -replace "-Status-", $Status
$emailBody = $emailBody -replace "-Message-", $Message
$EmailMessage = New-Object System.Net.Mail.MailMessage($EmailFrom, $EmailTo)
$emailMessage.IsBodyHtml = $true
$emailMessage.Subject = "$FileName - $Status"
$emailMessage.Body = $emailBody
$SMTPServer = "smtp.gmail.com"
$SMTPClient = New-Object Net.Mail.SmtpClient($SmtpServer, 587)
$SMTPClient.EnableSsl = $true
$SMTPClient.Credentials = New-Object System.Net.NetworkCredential($EmailFrom, $AppKey);
Write-Host "Sending email report."
$SMTPClient.Send($emailMessage)
Write-Host "Email sent successfully."
filebot -script "$Pushbullet\filebot_scripts\scripts-master\notification.groovy" --def pushbullet="$PushbulletAppKey" --def title="$FileName - $Status" --def message="$emailBody" --def messageNotification="$Message"