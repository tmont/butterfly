function createParser() {
	var parser = new Butterfly.Parser();
	parser.loadDefaultStrategies();
	return parser;
}

function trimLf(text) {
	return text && text.replace(/\n/g, "");
}