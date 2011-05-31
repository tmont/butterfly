function FunctionalStrategy() {
	FunctionalStrategy.$parent.call(this);

	function parseOptions(optionString, data) {
		var equalsIndex = Math.max(0, optionString.indexOf("="));
		data[optionString.substring(0, equalsIndex)] = optionString.substring(Math.min(equalsIndex + 1, optionString.length));
	}
	
	this.doExecute = function(context) {
		var peek = context.input.peek();
		var functionName = "";
		while (peek !== ButterflyStringReader.EOF && peek !== "|" && peek !== "]") {
			functionName += context.input.read();
			peek = context.input.peek();
		}
		
		var closer = context.input.read();
		
		var data = {};
		
		if (closer !== "]") {
			//read options
			
			peek = context.input.peek();
			var optionString = "";
			while (peek !== ButterflyStringReader.EOF) {
				if (peek === "]") {
					context.input.read();
					if (context.input.peek() !== "]") {
						break;
					}
				} else if (peek === "|") {
					context.input.read();
					if (context.input.peek() !== "|") {
						parseOptions(optionString, data);
						optionString = "";
					}
				}
				
				optionString += context.input.read();
				peek = context.input.peek();
			}
			
			if (optionString !== "") {
				parseOptions(optionString, data);
			}
		}
		
		context.updateCurrentChar();
		this.openAndCloseScope(this.createScope(functionName, data, context), context);
	};
	
	this.openAndCloseScope = function(scope, context) {
		this.openScope(scope, context);
		this.closeCurrentScope(context);
	};
}

extend(InlineStrategy, FunctionalStrategy);