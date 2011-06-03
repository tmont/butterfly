function MacroScope(macro) {
	this.type = ScopeTypeCache.macro;
	this.level = ScopeLevel.inline;
	this.macro = macro;
	
	this.open = function(analyzer) {
		analyzer.openMacro(this.macro);
	};
	this.close = function(analyzer) {
		analyzer.closeMacro(this.macro);
	};
}

extend(Scope, MacroScope);