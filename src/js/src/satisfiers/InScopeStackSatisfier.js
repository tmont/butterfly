function InScopeStackSatisfier(types) {
	this.isSatisfiedBy = function(context) {
		return context.scopes.containsType(types);
	};
}