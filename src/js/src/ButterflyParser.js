function ButterflyParser(options) {
	options = options || {};
	this.analyzer = null;
	
	var strategies = [];
	
	this.localLinkBaseUrl = options.localLinkBaseUrl || "/";
	this.localImageBaseUrl = options.localImageBaseUrl || "/";
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
			context.advanceInput();
		} while (!eofHandled);
		
		context.analyzer.onEnd();
		
		if (!context.scopes.isEmpty()) {
			throw new ParseException("The following scope must be manually closed: " + context.scopes.peek().type);
		}
		
		return new ParseResult(context.scopeTree, context.input.value());
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
	function addStrategiesForNonNestableInline(scopeType, token, ctor, priority) {
		this.addStrategy(new OpenNonNestableInlineStrategy(scopeType, token, ctor, priority));
		this.addStrategy(new CloseNonNestableInlineStrategy(scopeType, token, priority));
	}
	
	return function() {
		this.addStrategy(new WriteCharacterStrategy());
		this.addStrategy(new WriteEscapedBracketStrategy());
		this.addStrategy(new EndOfLineStrategy());
		
		addStrategiesForNonNestableInline.call(this, ScopeTypeCache.strong, "__", StrongScope);
		addStrategiesForNonNestableInline.call(this, ScopeTypeCache.emphasis, "''", EmphasisScope);
		addStrategiesForNonNestableInline.call(this, ScopeTypeCache.teletype, "==", TeletypeScope);
		addStrategiesForNonNestableInline.call(this, ScopeTypeCache.underline, "--", UnderlineScope);
		addStrategiesForNonNestableInline.call(this, ScopeTypeCache.strikeThrough, "---", StrikeThroughScope, ParseStrategy.defaultPriority - 1);
		
		this.addStrategy(new OpenBigStrategy());
		this.addStrategy(new CloseBigStrategy());
		this.addStrategy(new OpenSmallStrategy());
		this.addStrategy(new CloseSmallStrategy());
		
		this.addStrategy(new HeaderStrategy());
		this.addStrategy(new OpenLinkStrategy());
		this.addStrategy(new CloseLinkStrategy());
		this.addStrategy(new HorizontalRulerStrategy());
		this.addStrategy(new OpenBlockquoteStrategy());
		this.addStrategy(new CloseBlockquoteStrategy());
		this.addStrategy(new DefinitionListStrategy());
		this.addStrategy(new DefinitionStrategy());
		this.addStrategy(new OpenMultiLineDefinitionStrategy());
		this.addStrategy(new CloseMultiLineDefinitionStrategy());
		this.addStrategy(new LineBreakStrategy());
		
		this.addStrategy(new PreformattedLineStrategy());
		this.addStrategy(new OpenPreformattedStrategy());
		this.addStrategy(new ClosePreformattedStrategy());
		this.addStrategy(new PreformattedCodeStrategy());
		this.addStrategy(new ListStrategy());
		this.addStrategy(new UnparsedStrategy());
		this.addStrategy(new ModuleStrategy());
		this.addStrategy(new MacroStrategy());
		this.addStrategy(new TableStrategy());
		this.addStrategy(new CloseTableRowStrategy());
		
		return this;
	};
}();

var defaultModuleRegistry = {
	image: ImageModule,
	entity: HtmlEntityModule
};

var defaultMacroRegistry = {
	timestamp: TimestampMacro
};