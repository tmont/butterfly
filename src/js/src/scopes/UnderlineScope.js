function UnderlineScope() {
	this.type = ScopeTypeCache.underline;
	this.level = ScopeLevel.inline;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openUnderlinedText();
	};
	this.close = function(analyzer) {
		analyzer.closeUnderlinedText();
	};
}

extend(Scope, UnderlineScope);