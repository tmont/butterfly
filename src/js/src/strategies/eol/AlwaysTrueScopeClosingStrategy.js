function AlwaysTrueScopeClosingStrategy(scopeType) {
	this.scopeType = scopeType;
	this.shouldClose = function(context) { return true; };
}