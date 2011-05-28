function ParagraphClosingStrategy() {
	this.scopeType = ScopeTypeCache.paragraph;
	this.shouldClose = function(context) {
		var peek = context.input.peek();
		return context.input.isEof() || peek === ButterflyStringReader.EOF || peek === "\n";
	};
}