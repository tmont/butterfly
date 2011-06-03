function InlineStrategy() {
	InlineStrategy.$parent.call(this);
	
	this.addSatisfier(function(context) { return !context.scopes.containsType(ScopeTypeCache.module); });
}

extend(ScopeDrivenStrategy, InlineStrategy);