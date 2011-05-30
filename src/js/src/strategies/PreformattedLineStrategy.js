function PreformattedLineStrategy() {
	OpenMultiLineDefinitionStrategy.$parent.call(this);
	
	this.addSatisfier(new StartOfLineSatisfier());
	this.setAsTokenTransformer(" ");
	
	this.doExecute = function(context) {
		this.openScope(new PreformattedLineScope(), context);
	};
}

extend(ScopeDrivenStrategy, PreformattedLineStrategy);