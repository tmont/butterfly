function StrongStrategy() {
	StrongStrategy.$parent.call(this);
	this.type = ScopeTypeCache.strong;
	this.setAsTokenTransformer("__");
}

extend(InlineStrategy, StrongStrategy);

function OpenStrongStrategy() {
	OpenStrongStrategy.$parent.call(this);
	
	this.addSatisfier(new OpenNonNestableInlineScopeSatisfier(this.type));
	
	this.doExecute = function(context) {
		this.openScope(new StrongScope(), context);
	};
}

extend(StrongStrategy, OpenStrongStrategy);

function CloseStrongStrategy() {
	CloseStrongStrategy.$parent.call(this);
	
	this.addSatisfier(new CurrentScopeMustMatchSatisfier(this.type));
	
	this.doExecute = function(context) {
		this.closeCurrentScope(context);
	};
}

extend(StrongStrategy, CloseStrongStrategy);