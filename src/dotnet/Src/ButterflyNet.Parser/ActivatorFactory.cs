using System;

namespace ButterflyNet.Parser {
	public class ActivatorFactory<T> : NamedFactoryBase<T> {
		public ActivatorFactory(NamedTypeRegistry<T> typeRegistry) : base(typeRegistry) { }

		protected override T CreateFromType(Type type) {
			return (T)Activator.CreateInstance(type);
		}
	}
}