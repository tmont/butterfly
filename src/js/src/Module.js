function Module() {
	this.load = function(data) {
		for (var i in data) {
			if (typeof(this[i]) === "undefined") {
				continue;
			}
			
			this[i] = data[i];
		}
	};
}
Module.prototype.render = function(writer) { };