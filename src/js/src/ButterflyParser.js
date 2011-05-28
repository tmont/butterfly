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
		
		strategies.sort(function(a, b) { 
			if (a.priority === b.priority) {
				return 0;
			}
			
			return a.priority > b.priority ? 1 : -1; 
		});
		
		context.analyzer.onStart();
		
		do {
			strategy = null;
			context.advanceInput();
			eofHandled = context.input.isEof();
			
			for (i = 0; i < strategies.length; i++) {
				if (!strategies[i].isSatisfiedBy(context)) {
					continue;
				}
				
				strategy = strategies[i];
				break;
			}
			
			if (!strategy) {
				throw new ParseException(
					"No strategy found for " +
					(context.currentChar === ButterflyStringReader.EOF ? "<EOF>" : context.currentChar) + 
					" at index " + context.input.getIndex()
				);
			}
			
			strategy.execute(context);
		} while (!eofHandled);
		
		context.analyzer.onEnd();
		
		if (!context.scopes.isEmpty()) {
			throw new ParseException("Scopes that need to be manually closed were not closed");
		}
		
		return new ParseResult(context.scopeTree, context.input.value);
	};
}

//extensions
ButterflyParser.prototype.parseAndReturn = function(markup) {
	this.parse(markup);
	return this.flushAndReturn();
};

ButterflyParser.prototype.flushAndReturn = function() {
	var data = this.analyzer.writer.toString();
	this.analyzer.flush();
	return data;
};

ButterflyParser.prototype.loadDefaultStrategies = function() {
	this.addStrategy(new WriteCharacterStrategy());
	this.addStrategy(new EndOfLineStrategy());
	this.addStrategy(new OpenStrongStrategy());
	this.addStrategy(new CloseStrongStrategy());
	this.addStrategy(new OpenEmphasisStrategy());
	this.addStrategy(new CloseEmphasisStrategy());
	return this;
};

function ParseResult(tree, value) {
	this.scopeTree = tree;
	this.markup = value;
}