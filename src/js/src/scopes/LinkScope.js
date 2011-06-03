function LinkScope(url, baseUrl) {
	this.url = url;
	this.baseUrl = baseUrl === undefined ? "/" : baseUrl;
	
	this.type = ScopeTypeCache.link;
	this.level = ScopeLevel.inline;
	this.canNestText = true;
	
	this.open = function(analyzer) {
		analyzer.openLink(this.url, this.baseUrl);
	};
	this.close = function(analyzer) {
		analyzer.closeLink();
	};
}

extend(Scope, LinkScope);