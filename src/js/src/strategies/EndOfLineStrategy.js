var EndOfLineStrategy = function() {
	var defaultScopeClosingStrategies = [
		new ParagraphClosingStrategy(),
		
		new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.header),
		
		new DefinitionListScopeClosingStrategy(),
		new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.definition),
		new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.definitionTerm),
		
		new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.preformattedLine),
		
		new ListItemClosingStrategy(),
		new ListClosingStrategy(ScopeTypeCache.orderedList),
		new ListClosingStrategy(ScopeTypeCache.unorderedList),
		
		new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.tableRowLine),
		new TableCellClosingStrategy(ScopeTypeCache.tableCell),
		new TableCellClosingStrategy(ScopeTypeCache.tableHeader),
		new TableClosingStrategy(ScopeTypeCache.table)
	];
	
	return function(scopeClosingStrategies) {
		EndOfLineStrategy.$parent.call(this);
		
		this.addSatisfier(function(context) { return context.currentChar === "\n" || context.input.isEof(); });
		this.addSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.preformatted)));
		
		var closingStrategyMap = {};
		
		scopeClosingStrategies = scopeClosingStrategies || defaultScopeClosingStrategies;
		for (var i = 0; i < scopeClosingStrategies.length; i++) {
			closingStrategyMap[scopeClosingStrategies[i].scopeType] = scopeClosingStrategies[i];
		}
		
		this.doExecute = function(context) {
			while (!context.scopes.isEmpty()) {
				var strategy = closingStrategyMap[context.scopes.peek().type];
				if (!strategy || !strategy.shouldClose(context)) {
					break;
				}
				
				this.closeCurrentScope(context);
			}
		};
	};
}();

extend(ScopeDrivenStrategy, EndOfLineStrategy);