function ButterflyParser(options) {
	options = options || {};
	this.analyzer = null;
	
	var strategies = [];
	
	this.localLinkBaseUrl = options.localLinkBaseUrl;
	this.localImageBaseUrl = options.localImageBaseUrl;
	this.analyzer = options.analyzer || new HtmlAnalyzer();
	this.moduleFactory = options.moduleFactory;
	this.macroFactory = options.macroFactory;
	
	this.addStrategy = function(strategy) {
		strategies.push(strategy);
	};
	
	this.parse = function(markup) {
		var context = new ParseContext(new ButterflyStringReader(markup), this),
			eofHandled = false,
			strategy,
			i;
		
		strategies.sort(function(a, b) { return a.priority > b.priority; });
		
		context.analyzer.onStart();
		
		do {
			context.advanceInput();
			eofHandled = context.input.isEof();
			
			for (i = 0; i < strategies.length; i++) {
				strategy = strategies[i];
				if (strategy.isSatisfiedBy(context)) {
					break;
				}
			}
			
			if (!strategy) {
				throw "No strategy found for " + (context.currentChar === ButterflyStringReader.EOF ? "<EOF>" : context.currentChar);
			}
			
			strategy.execute(context);
		} while (!eofHandled);
		
		context.analyzer.onEnd();
	};
}



//extensions
ButterflyParser.prototype.parseAndReturn = function(markup) {
	this.parse(markup);
	return this.flushAndReturn();
};

ButterflyParser.prototype.flushAndReturn = function() {
	var data = this.analyzer.buffer;
	this.analyzer.flush();
	return data;
};

ButterflyParser.prototype.loadDefaultStrategies = function() {
	this.addStrategy(new WriteCharacterStrategy());
	return this;
};