function DependentSatisfier(strategy) { 
	this.isSatisfiedBy = function(context) {
		return strategy.isSatisfiedBy(context);
	};
}