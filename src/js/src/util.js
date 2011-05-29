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