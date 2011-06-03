function ListStrategy() {
	ListStrategy.$parent.call(this);

	this.addSatisfier(new StartOfLineSatisfier());
	this.addSatisfier(new CannotNestInsideInlineSatisfier());
	this.addSatisfier(function(context) {
		return context.currentChar === "*" || context.currentChar === "#";
	});
	
	this.doExecute = function(context) {
		var peek = context.input.peek(),
			listText = context.currentChar;
		
		while (peek === "*" || peek === "#") {
			listText += context.input.read();
			peek = context.input.peek();
		}
		
		context.input.seekToNonWhitespace();
		context.updateCurrentChar();
		
		handleList.call(this, listText, context);
	};
	
	function isList(scope) {
		return scope.type === ScopeTypeCache.unorderedList || scope.type === ScopeTypeCache.orderedList;
	}
	
	function handleList(listStart, context) {
		var depth = listStart.length,
			newScope = last(listStart) === "*" ? new UnorderedListScope() : new OrderedListScope(),
			openLists = filter(context.scopes, isList),
			difference = depth - openLists.length,
			currentListScope;
		
		if (openLists.length === 0) {
			//new list
			if (depth > 1) {
				throw new ParseException("Cannot start a list with a depth greater than one");
			}
			
			this.openScope(newScope, context);
		} else {
			if (difference === 1) {
				this.openScope(newScope, context);
			} else if (difference > 1) {
				throw new ParseException(
					"Nested lists cannot skip levels: expected a depth of less than or equal to " + 
					(openLists.length + 1) + " but got " + depth
				);
			} else {
			
				//verify that the list types match
				currentListScope = last(openLists);
				if (currentListScope.type !== newScope.type) {
					throw new ParseException("Expected list of type " + currentListScope.type + " but got " + newScope.type);
				}
			}
		}
		
		this.openScope(new ListItemScope(depth), context);
	}
}

extend(ScopeDrivenStrategy, ListStrategy);