function OpenBlockquoteStrategy() {
	OpenBlockquoteStrategy.$parent.call(this);
	
	this.addSatisfier(new StartOfLineSatisfier());
	this.addSatisfier(new CannotNestInsideInlineSatisfier());
	this.setAsTokenTransformer("<<");
	
	this.doExecute = function(context) {
		this.openScope(new BlockquoteScope(), context);
	};
}

extend(ScopeDrivenStrategy, OpenBlockquoteStrategy);

function CloseBlockquoteStrategy() {
	CloseBlockquoteStrategy.$parent.call(this);
	
	this.addSatisfier(new InScopeStackSatisfier(ScopeTypeCache.blockquote));
	this.addSatisfier(new CurrentScopeMustMatchOrBeParagraphSatisfier(ScopeTypeCache.blockquote));
	this.setAsTokenTransformer(">>");
	
	this.doExecute = function(context) {
		this.closeParagraphIfNecessary(context);
		this.closeCurrentScope(context);
	};
}

extend(ScopeDrivenStrategy, CloseBlockquoteStrategy);