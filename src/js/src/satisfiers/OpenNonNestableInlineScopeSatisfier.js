function OpenNonNestableInlineScopeSatisfier(scopeType) {
	var innerSatisfier = new NegatingSatisfier(new InScopeStackSatisfier(scopeType));
	
	this.isSatisfiedBy = function(context) {
		return innerSatisfier.isSatisfiedBy(context);
	};
}