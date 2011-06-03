function DefaultModuleFactory(typeRegistry, baseImageUrl) {
	DefaultModuleFactory.$parent.call(this, typeRegistry);
	
	this.createFromCtor = function(ctor, id) {
		if (id === "image") {
			return new ctor(baseImageUrl);
		}
		
		return new ctor();
	};
}

extend(NamedFactoryBase, DefaultModuleFactory);