function ListClosingStrategyBase() {
	function getDepthOfNextListItem(context) {
		var depth = 1,
			peek = last(context.input.peek(2));
		
		while (peek === "*" || peek === "#") {
			peek = last(context.input.peek(++depth + 1));
		}
		
		return depth;
	}
	
	this.shouldClose = function(context) {
		var peek = context.input.peek();
		if (peek !== "*" && peek !== "#") {
			return true;
		}
		
		var currentDepth = this.getListItem(context).depth;
		return this.shouldCloseForDepth(currentDepth, getDepthOfNextListItem(context));
	};
}

function ListItemClosingStrategy() {
	ListItemClosingStrategy.$parent.call(this);
	
	this.scopeType = ScopeTypeCache.listItem;
	
	this.shouldCloseForDepth = function(currentDepth, nextDepth) {
		return nextDepth <= currentDepth;
	};
	
	this.getListItem = function(context) {
		return context.scopes.peek();
	};
}

extend(ListClosingStrategyBase, ListItemClosingStrategy);

function ListClosingStrategy(scopeType) {
	ListClosingStrategy.$parent.call(this);
	
	this.scopeType = scopeType;
	
	this.shouldCloseForDepth = function(currentDepth, nextDepth) {
		return nextDepth < currentDepth;
	};
	
	this.getListItem = function(context) {
		var lastNode = context.scopeTree.getMostRecentNode(context.scopes.length);
		if (!lastNode || lastNode.scope.type !== ScopeTypeCache.listItem) {
			//should never get here if parsing was done correctly
			throw "Encountered list with no list items";
		}
		
		return lastNode.scope;
	};
}

extend(ListClosingStrategyBase, ListClosingStrategy);