function UnparsedScope() {
	this.type = ScopeTypeCache.unparsed;
	this.level = ScopeLevel.inline;
	this.open = function(analyzer) {
		analyzer.openUnparsed();
	};
	this.close = function(analyzer) {
		analyzer.closeUnparsed();
	};
}

extend(Scope, UnparsedScope);