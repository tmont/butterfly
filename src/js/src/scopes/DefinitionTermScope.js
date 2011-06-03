function DefinitionTermScope() {
	this.type = ScopeTypeCache.definitionTerm;
	this.level = ScopeLevel.block;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openDefinitionTerm();
	};
	this.close = function(analyzer) {
		analyzer.closeDefinitionTerm();
	};
}

extend(Scope, DefinitionTermScope);