function any(haystack, predicate) {
	for (var i = 0; i < haystack.length; i++) {
		if (predicate(haystack[i])) {
			return true;
		}
	}
	
	return false;
}

function all(haystack, predicate) {
	for (var i = 0; i < haystack.length; i++) {
		if (!predicate(haystack[i])) {
			return false;
		}
	}
	
	return true;
}

function toArray(thing) {
	return Array.prototype.slice.call(thing);
}

function contains(haystack, needle) {
	if (haystack.indexOf) {
		return haystack.indexOf(needle) !== -1;
	}
	
	return any(haystack, function(value) {
		return needle === value;
	});
}