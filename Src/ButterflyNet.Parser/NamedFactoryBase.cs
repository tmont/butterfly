using System;

namespace ButterflyNet.Parser {
	public abstract class NamedFactoryBase<T> : INamedFactory<T> {
		private readonly NamedTypeRegistry<T> typeRegistry;

		protected NamedFactoryBase(NamedTypeRegistry<T> typeRegistry) {
			this.typeRegistry = typeRegistry;
		}

		public T Create(string identifier) {
			var moduleType = typeRegistry[identifier];
			if (moduleType == null) {
				throw new UnknownIdentifierException(identifier);
			}

			return CreateFromType(moduleType);
		}

		protected abstract T CreateFromType(Type type);
	}
}