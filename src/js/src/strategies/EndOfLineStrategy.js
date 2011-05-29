var EndOfLineStrategy = function() {
	var defaultScopeClosingStrategies = [
		new ParagraphClosingStrategy(),
		new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.header)
	];
	
	return function(scopeClosingStrategies) {
		EndOfLineStrategy.$parent.call(this);
		
		this.addSatisfier(function(context) { return context.currentChar === "\n" || context.input.isEof(); });
		this.addSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.preformatted)));
		
		var closingStrategyMap = {};
		
		scopeClosingStrategies = scopeClosingStrategies || defaultScopeClosingStrategies;
		for (var i = 0; i < scopeClosingStrategies.length; i++) {
			closingStrategyMap[scopeClosingStrategies[i].scopeType] = scopeClosingStrategies[i].shouldClose;
		}
		
		
		
		this.doExecute = function(context) {
			while (!context.scopes.isEmpty()) {
				var shouldCloseScope = closingStrategyMap[context.scopes.peek().type];
				if (!shouldCloseScope || !shouldCloseScope(context)) {
					break;
				}
				
				this.closeCurrentScope(context);
			}
		};
	}
}();

extend(ScopeDrivenStrategy, EndOfLineStrategy);