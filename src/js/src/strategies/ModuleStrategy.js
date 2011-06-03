function ModuleStrategy() {
	ModuleStrategy.$parent.call(this);
	
	this.setAsTokenTransformer("[:");
	this.priority = ParseStrategy.defaultPriority - 1;
	
	this.createScope = function(name, data, context) {
		var module = context.moduleFactory.create(name);
		module.load(data);
		return new ModuleScope(module);
	};
}

extend(FunctionalStrategy, ModuleStrategy);