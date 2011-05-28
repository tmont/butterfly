function TeletypeScope() {
	this.type = ScopeTypeCache.teletype;
	this.level = ScopeLevel.inline;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openTeletypeText();
	};
	this.close = function(analyzer) {
		analyzer.closeTeletypeText();
	};
}

extend(Scope, TeletypeScope);