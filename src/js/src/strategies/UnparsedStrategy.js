function UnparsedStrategy() {
	UnparsedStrategy.$parent.call(this);

	this.setAsTokenTransformer("[!");
	this.priority = ParseStrategy.defaultPriority - 1;

	this.doExecute = function(context) {
		this.openScope(new UnparsedScope(), context);
		
		var match = /([\s\S]*?)](?!])/.exec(context.input.peekSubstring());
		if (!match) {
			throw new ParseException("Unparsed scope never closes");
		}
		
		var text = match[1].replace(/]]/g, "]");
		context.input.read(match[0].length);
		context.updateCurrentChar();
		context.analyzer.writeAndEscape(text);
		this.closeCurrentScope(context);
	};
}

extend(ScopeDrivenStrategy, UnparsedStrategy);