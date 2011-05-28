function ParagraphScope() {
	this.type = ScopeTypeCache.paragraph;
	this.level = ScopeLevel.block;
	this.canNestText = true;
	this.open = function(analyzer) {
		analyzer.openParagraph();
	};
	this.close = function(analyzer) {
		analyzer.closeParagraph();
	};
}

extend(Scope, ParagraphScope);