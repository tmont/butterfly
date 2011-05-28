function Stack() {
	this.peek = function() {
		return !this.isEmpty() ? this[this.length - 1] : undefined;
	};
	
	this.isEmpty = function() { return this.length === 0; };
}

Stack.prototype = new Array();