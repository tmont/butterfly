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

function filter(haystack, predicate) {
	var newHaystack = [];
	
	for (var i = 0; i < haystack.length; i++) {
		if (predicate(haystack[i])) {
			newHaystack.push(haystack[i]);
		}
	}
	
	return newHaystack;
}

function last(thing) {
	return thing.charAt ? thing.charAt(thing.length - 1) : thing[thing.length - 1];
}

function trim(string) {
	return string.replace(/^\s*|\s*$/g, "");
}

function rtrim(string) {
	return string.replace(/\s*$/g, "");
}