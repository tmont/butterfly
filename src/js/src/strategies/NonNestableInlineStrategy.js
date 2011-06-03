function CloseNonNestableInlineStrategy(scopeType, token, priority) {
	CloseNonNestableInlineStrategy.$parent.call(this);
	
	if (typeof(priority) === "number") {
		this.priority = priority;
	}
	
	this.setAsTokenTransformer(token);
	this.addSatisfier(new CurrentScopeMustMatchSatisfier(scopeType));
	
	this.doExecute = function(context) {
		this.closeCurrentScope(context);
	};
}

extend(InlineStrategy, CloseNonNestableInlineStrategy);

function OpenNonNestableInlineStrategy(scopeType, token, scopeConstructor, priority) {
	OpenNonNestableInlineStrategy.$parent.call(this);
	
	if (typeof(priority) === "number") {
		this.priority = priority;
	}
	
	this.setAsTokenTransformer(token);
	this.addSatisfier(new OpenNonNestableInlineScopeSatisfier(scopeType));
	
	this.doExecute = function(context) {
		this.openScope(new scopeConstructor(), context);
	};
}

extend(InlineStrategy, OpenNonNestableInlineStrategy);