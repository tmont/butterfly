function Scope() {
	this.level = ScopeLevel.inline;
	this.canNestParagraph = false;
	this.canNestText = false;
	this.type = undefined;
	this.open = function(analyzers) {};
	this.close = function(analyzers) {};
}