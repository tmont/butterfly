using System;

namespace ButterflyNet.Parser {
	public class ServiceProviderFactory<T> : NamedFactoryBase<T> {
		private readonly IServiceProvider serviceProvider;

		public ServiceProviderFactory(NamedTypeRegistry<T> typeRegistry, IServiceProvider serviceProvider) : base(typeRegistry) {
			this.serviceProvider = serviceProvider;
		}

		protected override T CreateFromType(Type type) {
			return (T)serviceProvider.GetService(type);
		}
	}
}