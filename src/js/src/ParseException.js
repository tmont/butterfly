var exceptionPrototype = {
	message: "",
	toString: function() { return this.message; }
};

function ParseException(message) {
	this.message = message;
}

ParseException.prototype = exceptionPrototype;

function UnknownIdentifierException(id) {
	this.message = "Could not locate type for identifier \"" + id + "\"";
}

UnknownIdentifierException.prototype = exceptionPrototype;

function ModuleException(message) {
	this.message = message;
}

ModuleException.prototype = exceptionPrototype;