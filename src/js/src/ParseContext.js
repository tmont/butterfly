function ParseContext(reader, options) {
	this.input = reader;
	this.currentChar = this.input.current();
	this.scopes = new Stack();
	this.localLinkBaseUrl = options.localLinkBaseUrl;
	this.localImageBaseUrl = options.localImageBaseUrl;
	this.analyzer = options.analyzer || new HtmlAnalyzer();
	this.moduleFactory = options.moduleFactory;
	this.macroFactory = options.macroFactory;
	this.scopeTree = new ScopeTree();
	
	this.advanceInput = function(amount) {
		this.input.read(amount || 1);
		this.updateCurrentChar();
	};
	
	this.updateCurrentChar = function() {
		this.currentChar = this.input.current();
	};
}