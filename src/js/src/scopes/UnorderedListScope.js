function UnorderedListScope() {
	this.type = ScopeTypeCache.unorderedList;
	this.level = ScopeLevel.block;
	this.open = function(analyzer) {
		analyzer.openUnorderedList();
	};
	this.close = function(analyzer) {
		analyzer.closeUnorderedList();
	};
}

extend(Scope, UnorderedListScope);