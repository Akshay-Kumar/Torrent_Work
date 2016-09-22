// send pushbullet report
include('lib/web')
def pushbullet = tryQuietly{ pushbullet.toString() }
def title = tryQuietly{ title.toString() }
def message = tryQuietly{ message.toString() }
def messageNotification = tryQuietly{ messageNotification.toString() }

if (pushbullet) {
	log.info 'Sending PushBullet report'
	tryLogCatch {
		PushBullet(pushbullet).sendFile(messageNotification, message, 'text/html', title, null)
	}
}