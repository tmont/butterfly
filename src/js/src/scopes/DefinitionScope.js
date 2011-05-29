function DefinitionScope() {
	this.type = ScopeTypeCache.definition;
	this.level = ScopeLevel.block;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openDefinition();
	};
	this.close = function(analyzer) {
		analyzer.closeDefinition();
	};
}

extend(Scope, DefinitionScope);