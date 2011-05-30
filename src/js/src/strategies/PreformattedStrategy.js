function PreformattedStrategyBase() {
	PreformattedStrategyBase.$parent.call(this);
	
	this.addSatisfier(new StartOfLineSatisfier());
	this.addSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.preformatted)));
	
	this.getLanguage = function(context) {
		context.advanceInput();
		var language = "";
		while (context.currentChar !== "\n" && context.currentChar !== ButterflyStringReader.EOF) {
			language += context.currentChar;
			context.advanceInput();
		}
		
		return language;
	};
}

extend(ScopeDrivenStrategy, PreformattedStrategyBase);

function OpenPreformattedStrategy() {
	OpenPreformattedStrategy.$parent.call(this);
	
	this.setAsTokenTransformer("{{{");
	
	this.doExecute = function(context) {
		this.openScope(new PreformattedScope(this.getLanguage(context)), context);
	};
}

extend(PreformattedStrategyBase, OpenPreformattedStrategy);


function ClosePreformattedStrategy() {
	ClosePreformattedStrategy.$parent.call(this);
	
	this.addSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.preformatted));
	this.setAsTokenTransformer("}}}");
	
	this.doExecute = function(context) {
		this.closeCurrentScope(context);
	};
}

extend(ScopeDrivenStrategy, ClosePreformattedStrategy);

function PreformattedCodeStrategy() {
	PreformattedCodeStrategy.$parent.call(this);
	
	this.setAsTokenTransformer("{{{{");
	this.priority = ParseStrategy.defaultPriority - 1;
	
	this.doExecute = function(context) {
		this.openScope(new PreformattedScope(this.getLanguage(context)), context);
		
		//read until }}}}
		var match = /^([\s\S]*?)\}\}\}\}/.exec(context.input.peekSubstring());
		if (!match) {
			throw new ParseException("Preformatted scope never closes");
		}
		
		var text = match[1];
		context.input.read(match[0].length);
		context.updateCurrentChar();
		context.analyzer.writeAndEscape(text);
		this.closeCurrentScope(context);
	};
}

extend(PreformattedStrategyBase, PreformattedCodeStrategy);