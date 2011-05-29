function HeaderStrategy() {
	HeaderStrategy.$parent.call(this);
	
	this.addSatisfier(new StartOfLineSatisfier());
	this.addSatisfier(new CannotNestInsideInlineSatisfier());
	
	this.setAsTokenTransformer("!");
	
	this.doExecute = function(context) {
		var depth = 1;
		while (context.input.peek() === "!") {
			depth++;
			context.input.read();
		}
		
		context.input.seekToNonWhitespace();
		context.updateCurrentChar();
		this.openScope(new HeaderScope(depth), context);
	};
}

extend(ScopeDrivenStrategy, HeaderStrategy);