function NegatingSatisfier(satisfierToNegate) { 
	this.isSatisfiedBy = function(context) {
		return !satisfierToNegate.isSatisfiedBy(context);
	};
}

extend(Satisfier, NegatingSatisfier);