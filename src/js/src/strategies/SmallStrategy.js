function OpenSmallStrategy() {
	OpenSmallStrategy.$parent.call(this);

	this.setAsTokenTransformer("(-");

	this.doExecute = function(context) {
		this.openScope(new SmallScope(), context);
	};
}

extend(InlineStrategy, OpenSmallStrategy);

function CloseSmallStrategy() {
	CloseSmallStrategy.$parent.call(this);

	this.setAsTokenTransformer("-)");
	this.addSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.small));

	this.doExecute = function(context) {
		this.closeCurrentScope(context);
	};
}
extend(InlineStrategy, CloseSmallStrategy);