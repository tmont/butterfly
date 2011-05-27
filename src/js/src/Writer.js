function Writer() {}
Writer.prototype  = {
	write: function(text) {},
	flush: function() {},
	toString: function() {}
};

function AppendStringWriter() {
	var buffer = "";
	
	this.write = function(text) {
		buffer += text;
	};
	
	this.flush = function() {
		buffer = "";
	};
	
	this.toString = function() {
		return buffer;
	};
}
extend(Writer, AppendStringWriter);