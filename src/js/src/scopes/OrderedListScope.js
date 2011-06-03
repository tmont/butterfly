function OrderedListScope() {
	this.type = ScopeTypeCache.orderedList;
	this.level = ScopeLevel.block;
	this.open = function(analyzer) {
		analyzer.openOrderedList();
	};
	this.close = function(analyzer) {
		analyzer.closeOrderedList();
	};
}

extend(Scope, OrderedListScope);