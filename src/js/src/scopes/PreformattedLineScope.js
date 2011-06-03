function PreformattedLineScope() {
	this.type = ScopeTypeCache.preformattedLine;
	this.level = ScopeLevel.block;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openPreformattedLine();
	};
	this.close = function(analyzer) {
		analyzer.closePreformattedLine();
	};
}

extend(Scope, PreformattedLineScope);