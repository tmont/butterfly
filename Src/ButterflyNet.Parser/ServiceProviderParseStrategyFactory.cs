using System;

namespace ButterflyNet.Parser {
	public class ServiceProviderParseStrategyFactory : IParseStrategyFactory {
		private readonly IServiceProvider serviceProvider;

		public ServiceProviderParseStrategyFactory(IServiceProvider serviceProvider) {
			this.serviceProvider = serviceProvider;
		}

		public IParseStrategy Create(Type type) {
			return (IParseStrategy)serviceProvider.GetService(type);
		}
	}
}