function Event() { 
	var callbacks = [];
	
	this.attach = function(callback) {
		callbacks.push(callback);
	};
		
	this.fire = function() {
		for (var i = 0; i < callbacks.length; i++) {
			callbacks[i].apply(this, arguments);
		}
	};
}