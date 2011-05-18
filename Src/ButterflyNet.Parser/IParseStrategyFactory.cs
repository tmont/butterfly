using System;

namespace ButterflyNet.Parser {
	public interface IParseStrategyFactory {
		ParseStrategyBase Create(Type type);
	}

	public sealed class DefaultParseStrategyFactory : IParseStrategyFactory {
		public ParseStrategyBase Create(Type type) {
			return (ParseStrategyBase)Activator.CreateInstance(type);
		}
	}
}