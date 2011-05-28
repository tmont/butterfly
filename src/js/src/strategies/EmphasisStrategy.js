function EmphasisStrategy() {
	EmphasisStrategy.$parent.call(this);
	this.type = ScopeTypeCache.emphasis;
	this.setAsTokenTransformer("''");
}

extend(InlineStrategy, EmphasisStrategy);

function OpenEmphasisStrategy() {
	OpenEmphasisStrategy.$parent.call(this);
	
	this.addSatisfier(new OpenNonNestableInlineScopeSatisfier(this.type));
	
	this.doExecute = function(context) {
		this.openScope(new EmphasisScope(), context);
	};
}

extend(EmphasisStrategy, OpenEmphasisStrategy);

function CloseEmphasisStrategy() {
	CloseEmphasisStrategy.$parent.call(this);
	
	this.addSatisfier(new CurrentScopeMustMatchSatisfier(this.type));
	
	this.doExecute = function(context) {
		this.closeCurrentScope(context);
	};
}

extend(EmphasisStrategy, CloseEmphasisStrategy);