function Stack() {
	this.peek = function() {
		return !this.isEmpty() ? this[this.length - 1] : undefined;
	};
	
	this.isEmpty = function() { return this.length === 0; };
}

Stack.prototype = new Array();

Stack.prototype.containsType = function() {
	for (var i = 0; i < arguments.length; i++) {
		for (var j = 0; j < this.length; j++) {
			if (this[j].type === arguments[i]) {
				return true;
			}
		}
	}
	
	return false;
};