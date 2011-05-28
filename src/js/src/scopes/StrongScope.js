function StrongScope() {
	this.type = ScopeTypeCache.strong;
	this.level = ScopeLevel.inline;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openStrongText();
	};
	this.close = function(analyzer) {
		analyzer.closeStrongText();
	};
}

extend(Scope, StrongScope);