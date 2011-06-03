function DefinitionListStrategy() {
	DefinitionListStrategy.$parent.call(this);
	
	this.addSatisfier(new StartOfLineSatisfier());
	this.addSatisfier(new CannotNestInsideInlineSatisfier());
	this.addSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.definitionTerm)));
	this.setAsTokenTransformer(";");
	
	this.doExecute = function(context) {
		if (!context.scopes.containsType(ScopeTypeCache.definitionList)) {
			this.openScope(new DefinitionListScope(), context);
		}
		
		this.openScope(new DefinitionTermScope(), context);
	};
}

extend(ScopeDrivenStrategy, DefinitionListStrategy);