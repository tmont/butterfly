function EofSatisfier() { 
	this.isSatisfiedBy = function(context) {
		return context.input.isEof();
	};
}

extend(Satisfier, EofSatisfier);