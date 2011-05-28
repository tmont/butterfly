function ParseException(message) {
	this.message = message;
}

ParseException.prototype.toString = function() {
	return this.message;
};