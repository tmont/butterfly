function ModuleScope(module) {
	this.type = ScopeTypeCache.module;
	this.level = ScopeLevel.inline;
	this.module = module;
	
	this.open = function(analyzer) {
		analyzer.openModule(this.module);
	};
	this.close = function(analyzer) {
		analyzer.closeModule(this.module);
	};
}

extend(Scope, ModuleScope);