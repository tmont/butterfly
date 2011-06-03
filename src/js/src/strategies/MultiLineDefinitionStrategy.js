function OpenMultiLineDefinitionStrategy() {
	OpenMultiLineDefinitionStrategy.$parent.call(this);
	
	this.addSatisfier(new DependentSatisfier(new DefinitionStrategy()));
	this.setAsTokenTransformer(":{");
	
	this.priority = ParseStrategy.defaultPriority - 1;
	
	this.doExecute = function(context) {
		this.openScope(new MultiLineDefinitionScope(), context);
	};
}

extend(ScopeDrivenStrategy, OpenMultiLineDefinitionStrategy);

function CloseMultiLineDefinitionStrategy() {
	CloseMultiLineDefinitionStrategy.$parent.call(this);
	
	this.addSatisfier(new CurrentScopeMustMatchOrBeParagraphSatisfier(ScopeTypeCache.multiLineDefinition));
	this.setAsTokenTransformer("}:");
	
	this.priority = ParseStrategy.defaultPriority - 1;
	
	this.doExecute = function(context) {
		this.closeParagraphIfNecessary(context);
		this.closeCurrentScope(context);
	};
}

extend(ScopeDrivenStrategy, CloseMultiLineDefinitionStrategy);