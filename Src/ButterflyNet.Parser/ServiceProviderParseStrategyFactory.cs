using System;

namespace ButterflyNet.Parser {
	public class ServiceProviderParseStrategyFactory : IParseStrategyFactory {
		private readonly IServiceProvider serviceProvider;

		public ServiceProviderParseStrategyFactory(IServiceProvider serviceProvider) {
			this.serviceProvider = serviceProvider;
		}

		public ParseStrategyBase Create(Type type) {
			return (ParseStrategyBase)serviceProvider.GetService(type);
		}
	}
}