function LineBreakStrategy() {
	LineBreakStrategy.$parent.call(this);

	this.setAsTokenTransformer("%%%");

	this.doExecute = function(context) {
		this.openScope(new LineBreakScope(), context);
		this.closeCurrentScope(context);
	};
}

extend(InlineStrategy, LineBreakStrategy);