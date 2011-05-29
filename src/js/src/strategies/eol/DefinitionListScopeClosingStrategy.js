function DefinitionListScopeClosingStrategy() {
	this.scopeType = ScopeTypeCache.definitionList;
	this.shouldClose = function(context) {
		var peek = context.input.peek();
		return peek !== ";" && peek !== ":";
	};
}