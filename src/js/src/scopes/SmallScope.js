function SmallScope() {
	this.type = ScopeTypeCache.small;
	this.level = ScopeLevel.inline;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openSmallText();
	};
	this.close = function(analyzer) {
		analyzer.closeSmallText();
	};
}

extend(Scope, SmallScope);