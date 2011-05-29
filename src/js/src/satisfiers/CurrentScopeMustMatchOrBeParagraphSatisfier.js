function CurrentScopeMustMatchOrBeParagraphSatisfier() {
	var scopeTypes = toArray(arguments);
	scopeTypes.push(ScopeTypeCache.paragraph);
	
	this.isSatisfiedBy = function(context) {
		return !context.scopes.isEmpty() && contains(scopeTypes, context.scopes.peek().type);
	};
}

extend(Satisfier, CurrentScopeMustMatchOrBeParagraphSatisfier);