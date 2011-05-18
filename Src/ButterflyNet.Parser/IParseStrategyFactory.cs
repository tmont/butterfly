using System;

namespace ButterflyNet.Parser {
	public interface IParseStrategyFactory {
		ParseStrategy Create(Type type);
	}

	public sealed class DefaultParseStrategyFactory : IParseStrategyFactory {
		public ParseStrategy Create(Type type) {
			return (ParseStrategy)Activator.CreateInstance(type);
		}
	}
}