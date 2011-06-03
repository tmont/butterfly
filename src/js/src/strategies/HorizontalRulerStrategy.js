function HorizontalRulerStrategy() {
	HorizontalRulerStrategy.$parent.call(this);
	
	this.addSatisfier(new StartOfLineSatisfier());
	this.addSatisfier(new CannotNestInsideInlineSatisfier());
	this.addSatisfier(function(context) { return /^----(?:\n|$)/.test(context.input.substring()); });
	
	this.priority = ParseStrategy.defaultPriority - 2;
	
	this.doExecute = function(context) {
		context.input.read(3);
		context.updateCurrentChar();
		
		this.openScope(new HorizontalRulerScope(), context);
		this.closeCurrentScope(context);
	};
}

extend(ScopeDrivenStrategy, HorizontalRulerStrategy);