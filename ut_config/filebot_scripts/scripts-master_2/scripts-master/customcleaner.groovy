// filebot -script fn:cleaner [--action test] /path/to/media/

def deleteRootFolder = any{ root.toBoolean() }{ false }

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
