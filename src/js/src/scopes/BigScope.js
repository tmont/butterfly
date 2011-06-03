function BigScope() {
	this.type = ScopeTypeCache.big;
	this.level = ScopeLevel.inline;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openBigText();
	};
	this.close = function(analyzer) {
		analyzer.closeBigText();
	};
}

extend(Scope, BigScope);