function InScopeStackSatisfier() {
	var scopeTypes = arguments;
	this.isSatisfiedBy = function(context) {
		return context.scopes.containsType(scopeTypes);
	};
}