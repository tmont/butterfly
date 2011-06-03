function TableClosingStrategy() {
	this.scopeType = ScopeTypeCache.table;
	this.shouldClose = function(context) {
		return context.input.peek() !== "|";
	};
}