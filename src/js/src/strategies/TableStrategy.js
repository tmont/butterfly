function TableStrategy() {
	TableStrategy.$parent.call(this);

	this.addSatisfier(new CannotNestInsideInlineSatisfier());
	this.addSatisfier(function() {
		var sol = new StartOfLineSatisfier();
		var containsTable = new InScopeStackSatisfier(ScopeTypeCache.table);
		
		return function(context) {
			return sol.isSatisfiedBy(context) || containsTable.isSatisfiedBy(context);
		};
	}());
	this.setAsTokenTransformer("|");
	
	this.doExecute = function(context) {
		var rowScope = new TableRowLineScope();
		if (context.input.peek() === '{') {
			context.advanceInput();
			rowScope = new TableRowScope();
		}

		var cellType = ScopeTypeCache.tableCell;
		if (context.input.peek() === '!') {
			context.advanceInput();
			cellType = ScopeTypeCache.tableHeader;
		}
		
		context.input.seekToNonWhitespace();
		context.updateCurrentChar();

		if (!context.scopes.containsType(ScopeTypeCache.table)) {
			//no tables, so create a new one
			this.openScope(new TableScope(), context);
			this.openScope(rowScope, context);
		} else if (context.scopes.containsType([ScopeTypeCache.tableCell, ScopeTypeCache.tableHeader])) {
			//close table cell
			var currentScope = context.scopes.peek();
			if (!currentScope || (currentScope.type !== ScopeTypeCache.tableCell && currentScope.type !== ScopeTypeCache.tableHeader)) {
				throw new ParseException("Cannot close table cell until all containing scopes are closed");
			}

			this.closeCurrentScope(context);
		} else if (!context.scopes.containsType([ScopeTypeCache.tableRow, ScopeTypeCache.tableRowLine])) {
			this.openScope(rowScope, context);
		}
		
		if (
			context.input.peek() !== ButterflyStringReader.EOF && (
				(context.scopes.containsType(ScopeTypeCache.tableRowLine) && context.input.peek() !== "\n") 
				|| context.scopes.containsType(ScopeTypeCache.tableRow)
			)
		) {
			this.openScope(cellType == ScopeTypeCache.tableHeader ? new TableHeaderScope() : new TableCellScope(), context);
		}
	};
}

extend(ScopeDrivenStrategy, TableStrategy);

function CloseTableRowStrategy() {
	CloseTableRowStrategy.$parent.call(this);
	
	this.addSatisfier(new InScopeStackSatisfier(ScopeTypeCache.tableRow));
	this.setAsTokenTransformer("}|");
	
	var closableScopes = [ScopeTypeCache.tableCell, ScopeTypeCache.tableHeader, ScopeTypeCache.paragraph];
	
	this.doExecute = function(context) {
		while (!context.scopes.isEmpty()) {
			if (!contains(closableScopes, context.scopes.peek().type)) {
				break;
			}

			this.closeCurrentScope(context);
		}

		var currentScope = context.scopes.peek();
		if (currentScope == null || currentScope.type !== ScopeTypeCache.tableRow) {
			throw new ParseException("Cannot close table row until all containing scopes are closed");
		}

		this.closeCurrentScope(context);
	};
}

extend(ScopeDrivenStrategy, CloseTableRowStrategy);