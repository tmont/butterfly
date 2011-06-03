function NextCharacterIsNotTheSameSatisfier() { 
	this.isSatisfiedBy = function(context) {
		return context.input.peek() !== context.currentChar;
	};
}

extend(Satisfier, NextCharacterIsNotTheSameSatisfier);