function ExactCharMatchSatisfier(chars) {
	
	this.isSatisfiedBy = function(context) {
		if (chars.length === 1) {
			return chars === context.currentChar;
		}
		
		return context.currentChar + context.input.peek(chars.length - 1) === chars;
	};
}