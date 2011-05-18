using System;

namespace ButterflyNet.Parser {
	public class ServiceProviderParseStrategyFactory : IParseStrategyFactory {
		private readonly IServiceProvider serviceProvider;

		public ServiceProviderParseStrategyFactory(IServiceProvider serviceProvider) {
			this.serviceProvider = serviceProvider;
		}

		public ParseStrategy Create(Type type) {
			return (ParseStrategy)serviceProvider.GetService(type);
		}
	}
}