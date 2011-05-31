using System;
using ButterflyNet.Parser.Modules;

namespace ButterflyNet.Parser {
	public class DefaultModuleFactory : ActivatorFactory<IButterflyModule> {
		private readonly string baseImageUrl;

		public DefaultModuleFactory(string baseImageUrl, NamedTypeRegistry<IButterflyModule> typeRegistry) : base(typeRegistry) {
			this.baseImageUrl = baseImageUrl;
		}

		protected override IButterflyModule CreateFromType(Type type) {
			return type == typeof(ImageModule) ? new ImageModule(baseImageUrl) : base.CreateFromType(type);
		}
	}
}