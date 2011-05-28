function Stack() {
	var stack = [];
	
	this.peek = function() {
		return !this.isEmpty() ? stack[stack.length - 1] : undefined;
	};
	
	this.isEmpty = function() { return stack.length === 0; };
	
	this.length = 0;
	
	this.push = function(value) {
		this[stack.length] = value;
		stack.push(value);
		this.length = stack.length;
	};
	
	this.pop = function() {
		var value = stack.pop();
		delete this[stack.length];
		this.length = stack.length;
		return value;
	};
	
	this.containsType = function() {
		for (var i = 0; i < arguments.length; i++) {
			for (var j = 0; j < stack.length; j++) {
				if (stack[j].type === arguments[i]) {
					return true;
				}
			}
		}
		
		return false;
	};
}