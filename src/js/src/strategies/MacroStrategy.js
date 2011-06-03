function MacroStrategy() {
	ModuleStrategy.$parent.call(this);
	
	this.setAsTokenTransformer("[::");
	this.priority = ParseStrategy.defaultPriority - 2;
	
	var startIndex = -1;
	
	this.beforeExecute.attach(function(context) {
		startIndex = context.input.getIndex() - 2; //token length - 1
	});
	
	this.afterScopeCloses.attach(function(scope, context) {
		var value = scope.macro.getValue();
		var endIndex = context.input.getIndex() + 1;
		
		context.input.replace(startIndex, endIndex, value);
		
		context.input.seek(startIndex - 1);
		startIndex = -1;
	});
	
	this.createScope = function(name, data, context) {
		var macro = context.macroFactory.create(name);
		macro.load(data);
		return new MacroScope(macro);
	};
}

extend(FunctionalStrategy, MacroStrategy);