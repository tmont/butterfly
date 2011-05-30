function PreformattedScope(language) {
	this.language = language;
	this.type = ScopeTypeCache.preformatted;
	this.level = ScopeLevel.block;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openPreformatted(this.language);
	};
	this.close = function(analyzer) {
		analyzer.closePreformatted(this.language);
	};
}

extend(Scope, PreformattedScope);