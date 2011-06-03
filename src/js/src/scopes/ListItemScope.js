function ListItemScope(depth) {
	this.depth = depth;
	this.type = ScopeTypeCache.listItem;
	this.level = ScopeLevel.block;
	this.canNestText = true;
	this.open = function(analyzer) {
		analyzer.openListItem();
	};
	this.close = function(analyzer) {
		analyzer.closeListItem();
	};
}

extend(Scope, ListItemScope);