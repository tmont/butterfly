function WriteEscapedBracketStrategy() {
	WriteEscapedBracketStrategy.$parent.call(this);
	
	this.addSatisfier(new InScopeStackSatisfier(ScopeTypeCache.link, ScopeTypeCache.module, ScopeTypeCache.Unescaped, ScopeTypeCache.raw));
	this.setAsTokenTransformer("]]");
	
	this.getChar = function(context) { return "]"; };
	
	this.priority = 10000;
}

extend(WriteCharacterStrategy, WriteEscapedBracketStrategy);