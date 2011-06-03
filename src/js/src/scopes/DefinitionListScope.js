function DefinitionListScope() {
	this.type = ScopeTypeCache.definitionList;
	this.level = ScopeLevel.block;
	
	this.open = function(analyzer) {
		analyzer.openDefinitionList();
	};
	this.close = function(analyzer) {
		analyzer.closeDefinitionList();
	};
}

extend(Scope, DefinitionListScope);