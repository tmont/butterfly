function extend(trait, func) {
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
}