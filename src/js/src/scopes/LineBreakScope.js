function LineBreakScope() {
	this.type = ScopeTypeCache.lineBreak;
	this.level = ScopeLevel.inline;
	
	this.open = function(analyzer) {
		analyzer.openLineBreak();
	};
	this.close = function(analyzer) {
		analyzer.closeLineBreak();
	};
}

extend(Scope, LineBreakScope);