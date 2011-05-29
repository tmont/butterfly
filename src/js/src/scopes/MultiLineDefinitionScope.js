function MultiLineDefinitionScope() {
	this.type = ScopeTypeCache.multiLineDefinition;
	this.level = ScopeLevel.block;
	this.canNestText = true;
	this.canNestParagraph = true;
	
	this.open = function(analyzer) {
		analyzer.openMultiLineDefinition();
	};
	this.close = function(analyzer) {
		analyzer.closeMultiLineDefinition();
	};
}

extend(Scope, MultiLineDefinitionScope);