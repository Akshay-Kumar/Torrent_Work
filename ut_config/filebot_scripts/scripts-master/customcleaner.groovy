// filebot -script fn:cleaner [--action test] /path/to/media/

def deleteRootFolder = any{ root.toBoolean() }{ true }

def clean = { f ->
	println "Delete $f"

	// do a dry run via --action test
	if (_args.action == 'test') {
		return false
	}

	return f.isDirectory() ? f.deleteDir() : f.delete()
}

args.getFiles().sort().reverse().each{
			clean(it)
}
args.getFolders().each{
	if (it.isDirectory()) {
		if (deleteRootFolder) 
			clean(it)
	}
}
