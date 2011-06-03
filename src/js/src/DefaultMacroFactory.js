function DefaultMacroFactory(typeRegistry) {
	DefaultMacroFactory.$parent.call(this, typeRegistry);
	
	this.createFromCtor = function(ctor, id) {
		return new ctor();
	};
}

extend(NamedFactoryBase, DefaultMacroFactory);