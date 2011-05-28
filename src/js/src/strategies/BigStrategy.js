function OpenBigStrategy() {
	OpenBigStrategy.$parent.call(this);

	this.setAsTokenTransformer("(+");

	this.doExecute = function(context) {
		this.openScope(new BigScope(), context);
	};
}

extend(InlineStrategy, OpenBigStrategy);

function CloseBigStrategy() {
	CloseBigStrategy.$parent.call(this);

	this.setAsTokenTransformer("+)");
	this.addSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.big));

	this.doExecute = function(context) {
		this.closeCurrentScope(context);
	};
}
extend(InlineStrategy, CloseBigStrategy);