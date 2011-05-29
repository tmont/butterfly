function CannotNestInsideInlineSatisfier() { 
	this.isSatisfiedBy = function(context) {
		return !any(context.scopes, function(scope) {
			return scope.level === ScopeLevel.inline;
		});
	};
}

extend(Satisfier, CannotNestInsideInlineSatisfier);