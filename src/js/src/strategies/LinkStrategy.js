function OpenLinkStrategy() {
	OpenLinkStrategy.$parent.call(this);

	this.addSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.link)));
	this.setAsTokenTransformer("[");

	this.doExecute = function(context) {
		var peek = context.input.peek(),
			url = "",
			closer;
		
		while (peek !== ButterflyStringReader.EOF && peek !== "|" && peek !== "]") {
			url += context.input.read();
			peek = context.input.peek();
		}
		
		closer = context.input.read();
		
		this.openScope(new LinkScope(url, context.localLinkBaseUrl), context);
		
		if (closer === "]") {
			context.analyzer.writeAndEscape(url);
			this.closeCurrentScope(context);
		}
	};
}

extend(InlineStrategy, OpenLinkStrategy);

function CloseLinkStrategy() {
	CloseLinkStrategy.$parent.call(this);
	
	this.addSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.link));
	this.addSatisfier(new NextCharacterIsNotTheSameSatisfier());
	this.setAsTokenTransformer("]");
	
	this.doExecute = function(context) {
		this.closeCurrentScope(context);
	};
}

extend(InlineStrategy, CloseLinkStrategy);