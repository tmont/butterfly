function StartOfLineSatisfier() { 
	this.isSatisfiedBy = function(context) {
		return context.input.isSol();
	};
}

extend(Satisfier, StartOfLineSatisfier);