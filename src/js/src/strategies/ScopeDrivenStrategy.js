function ScopeDrivenStrategy() {
	ScopeDrivenStrategy.$parent.call(this);
	
	var self = this;
	
	//events
	this.beforeScopeOpens = new Event();
	this.afterScopeOpens = new Event();
	this.beforeScopeCloses = new Event();
	this.afterScopeCloses = new Event();
	
	//event delegates
	function closeParagraphForBlockScopes(scope, context) {
		if (scope.level !== ScopeLevel.block) {
			return;
		}
		
		if (!context.scopes.containsType(ScopeTypeCache.paragraph)) {
			return;
		}
		
		if (context.scopes.peek().type !== ScopeTypeCache.paragraph) {
			throw new ParseException("Cannot nest a block level scope inside a paragraph. Did you forget to close something?");
		}
		
		self.closeCurrentScope(context);
	}
	
	function createParagraphIfNecessary(scope, context) {
		if (scope.level === ScopeLevel.inline) {
			new OpenParagraphStrategy().executeIfSatisfied(context);
		}
	}
	
	function updateScopeTreeAfterOpen(scope, context) {
		var newNode = new ScopeTreeNode(scope);
		
		if (context.currentNode) {
			context.currentNode.addChild(newNode);
		} else {
			context.scopeTree.addNode(newNode);
		}
		
		context.currentNode = newNode;
	}
	
	function updateScopeTreeAfterClose(scope, context) {
		context.currentNode = context.currentNode.parent;
	}
	
	this.beforeScopeOpens.attach(createParagraphIfNecessary);
	this.beforeScopeOpens.attach(closeParagraphForBlockScopes);
	
	this.afterScopeOpens.attach(updateScopeTreeAfterOpen);
	this.afterScopeOpens.attach(updateScopeTreeAfterClose);
	
	this.openScope = function(scope, context) {
		this.beforeScopeOpens.fire(scope, context);
		
		context.scopes.push(scope);
		scope.open(context.analyzer);
		
		this.afterScopeOpens.fire(scope, context);
	};
	
	this.closeCurrentScope = function(context) {
		var currentScope = context.scopes.peek();
		if (!currentScope) {
			throw new ParseException("Stack is empty, unable to find scope to close");
		}
		
		this.beforeScopeCloses.fire(scope, context);
		
		if (context.scopes.peek() !== currentScope) {
			throw new ParseException("ScopeDrivenStrategy.beforeScopeCloses invocations created an inconsistent state");
		}
		
		context.scopes.pop();
		currentScope.close(context.analyzer);
		
		this.afterScopeCloses.fire(currentScope, context);
	};
	
	this.closeParagraphIfNecessary = function(context) {
		var scope = context.scopes.peek();
		if (!scope || scope.type !== ScopeTypeCache.paragraph) {
			return;
		}
		
		this.closeCurrentScope(context);
	};
}

extend(ParseStrategy, ScopeDrivenStrategy);