function StrikeThroughScope() {
	this.type = ScopeTypeCache.strikeThrough;
	this.level = ScopeLevel.inline;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openStrikeThroughText();
	};
	this.close = function(analyzer) {
		analyzer.closeStrikeThroughText();
	};
}

extend(Scope, StrikeThroughScope);