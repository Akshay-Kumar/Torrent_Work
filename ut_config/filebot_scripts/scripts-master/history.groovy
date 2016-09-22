// filebot -script fn:history --format "$from => $to"

// use --format parameter to specify your own output format
def format = _args.format ?: '''${from}\t${to}'''
def template = new groovy.text.GStringTemplateEngine().createTemplate(format)

// use args to list history only for the given folders if desired
def accept(from, to) {
	return args.empty || (args.any{ to.absolutePath.startsWith(it.absolutePath) } && to.exists())
}


getRenameLog(true).each { from, to ->
	if (accept(from, to))
		println template.make(from:from, to:to)
}
