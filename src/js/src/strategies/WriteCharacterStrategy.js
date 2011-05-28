function WriteCharacterStrategy() {
	WriteCharacterStrategy.$parent.call(this);
	
	this.addSatisfier(new NegatingSatisfier(new EofSatisfier()));
	this.addSatisfier(function(context) { return context.scopes.isEmpty() || context.scopes.peek().canNestText; });
	
	var paragraphStrategy = new OpenParagraphStrategy();
	
	this.priority = Infinity;
	
	this.doExecute = function(context) {
		var c = this.getChar(context);
		
		if (context.currentNode && context.currentNode.scope.type === ScopeTypeCache.unescaped) {
			context.analyzer.writeUnescapedChar(c);
		} else {
			context.analyzer.writeAndEscape(c);
		}
	};
}

extend(ParseStrategy, WriteCharacterStrategy);

WriteCharacterStrategy.prototype.getChar = function(context) {
	return context.currentChar;
};