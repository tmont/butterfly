function Loadable() {
	this.load = function(data) {
		for (var i in data) {
			if (typeof(this[i]) === "undefined") {
				continue;
			}
			
			this[i] = data[i];
		}
	};
};