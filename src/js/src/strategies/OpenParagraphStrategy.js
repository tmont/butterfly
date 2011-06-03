function OpenParagraphStrategy() {
	OpenParagraphStrategy.$parent.call(this);

	this.addSatisfier(function(context) {
		return context.scopes.isEmpty() || context.scopes.peek().canNestParagraph;
	});

	this.doExecute = function(context) {
		this.openScope(new ParagraphScope(), context);
	};
}

extend(ScopeDrivenStrategy, OpenParagraphStrategy);