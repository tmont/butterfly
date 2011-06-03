function HorizontalRulerScope() {
	this.type = ScopeTypeCache.horizontalRuler;
	this.level = ScopeLevel.block;
	
	this.open = function(analyzer) {
		analyzer.openHorizontalRuler();
	};
	this.close = function(analyzer) {
		analyzer.closeHorizontalRuler();
	};
}

extend(Scope, HorizontalRulerScope);