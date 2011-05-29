function BlockquoteScope() {
	this.type = ScopeTypeCache.blockquote;
	this.level = ScopeLevel.block;
	this.canNestParagraph = true;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openBlockquote();
	};
	this.close = function(analyzer) {
		analyzer.closeBlockquote();
	};
}

extend(Scope, BlockquoteScope);