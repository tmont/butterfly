function NamedFactoryBase(typeRegistry) {
	this.create = function(id) {
		var ctor = typeRegistry[id];
		if (!ctor) {
			throw new UnknownIdentifierException(id);
		}
		
		return this.createFromCtor(ctor, id);
	};
}