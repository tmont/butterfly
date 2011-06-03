function CurrentScopeMustMatchSatisfier(scopeType) {
	this.isSatisfiedBy = function(context) {
		return !context.scopes.isEmpty() && context.scopes.peek().type === scopeType;
	};
}