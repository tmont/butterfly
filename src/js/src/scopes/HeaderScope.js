function HeaderScope(depth) {
	this.depth = depth
	this.type = ScopeTypeCache.header;
	this.level = ScopeLevel.block;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openHeader(this.depth);
	};
	this.close = function(analyzer) {
		analyzer.closeHeader(this.depth);
	};
}

extend(Scope, HeaderScope);