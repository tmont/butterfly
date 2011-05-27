function WriteCharacterStrategy() {
	WriteCharacterStrategy.$parent.call(this);
	this.addSatisfier(new NegatingSatisfier(new EofSatisfier()));
	this.addSatisfier(function(context) { return context.scopes.length === 0 || context.scopes.peek().canNestText; });
}

extend(ParseStrategy, WriteCharacterStrategy);