function TableCellClosingStrategy(scopeType) {
	this.scopeType = scopeType;
	this.shouldClose = function(context) {
		return !context.scopes.containsType(ScopeTypeCache.tableRow);
	};
}