using System;

namespace ButterflyNet.Parser {
	public interface IParseStrategyFactory {
		IParseStrategy Create(Type type);
	}

	public sealed class DefaultParseStrategyFactory : IParseStrategyFactory {
		public IParseStrategy Create(Type type) {
			return (IParseStrategy)Activator.CreateInstance(type);
		}
	}
}