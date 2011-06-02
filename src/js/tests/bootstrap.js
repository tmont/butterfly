function createParser() {
	var parser = new Butterfly.Parser();
	parser.loadDefaultStrategies();
	return parser;
}

function trimLf(text) {
	return text && text.replace(/\n/g, "");
}

function trimEnd(text) {
	return text.replace(/\n+$/, "");
}

if (typeof(extend) === "undefined") {
	var extend = function(trait, func) {
		func.$super = func.$super || {};
		
		for (var i in trait.prototype) {
			func.prototype[i] = trait.prototype[i];
			if (typeof(trait.prototype[i]) === "function") {
				func.$super[i] = trait.prototype[i];
			}
		}
		
		if (!func.$parents) {
			func.$parents = [];
			func.$parent = trait;
		}
		func.$parents.push(trait);
		
		func.$override = function(funcToOverride, newImplementation) {
			func.$super[funcToOverride] = func.prototype[funcToOverride];
			func.prototype[funcToOverride] = newImplementation;
		};
	};
}

if (typeof(window.Butterfly) !== "undefined") {
	for (var i in window.Butterfly) {
		window[i] = window.Butterfly[i];
	}
}