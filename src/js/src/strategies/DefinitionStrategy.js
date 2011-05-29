function DefinitionStrategy() {
	DefinitionStrategy.$parent.call(this);
	
	this.addSatisfier(new StartOfLineSatisfier());
	this.addSatisfier(new InScopeStackSatisfier(ScopeTypeCache.definitionList));
	this.setAsTokenTransformer(":");
	this.addSatisfier(function(context) {
		var previousNode = context.scopeTree.getMostRecentNode(context.scopes.length);
		return previousNode && previousNode.scope.type === ScopeTypeCache.definitionTerm;
	});
	
	this.doExecute = function(context) {
		this.openScope(new DefinitionScope(), context);
	};
}

extend(ScopeDrivenStrategy, DefinitionStrategy);