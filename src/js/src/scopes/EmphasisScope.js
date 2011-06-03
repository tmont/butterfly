function EmphasisScope() {
	this.type = ScopeTypeCache.emphasis;
	this.level = ScopeLevel.inline;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openEmphasizedText();
	};
	this.close = function(analyzer) {
		analyzer.closeEmphasizedText();
	};
}

extend(Scope, EmphasisScope);