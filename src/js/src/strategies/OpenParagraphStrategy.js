function OpenParagraphStrategy() {
	OpenParagraphStrategy.$parent.call(this);
	
	this.addSatisfier(function(context) { return context.scopes.isempty() || context.scopes.peek().canNestParagraph; });
	
	this.doExecute = function() {
		this.openScope(new ParagraphScope(), context);
	};
}

extend(ScopeDrivenStrategy, OpenParagraphStrategy);