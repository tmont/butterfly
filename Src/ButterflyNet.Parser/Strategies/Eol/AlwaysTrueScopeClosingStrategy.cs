using System;

namespace ButterflyNet.Parser.Strategies.Eol {
	public sealed class AlwaysTrueScopeClosingStrategy : IEolScopeClosingStrategy {
		public AlwaysTrueScopeClosingStrategy(Type scopeType) {
			ScopeType = scopeType;
		}

		public Type ScopeType { get; private set; }

		public bool ShouldClose(ParseContext context) {
			return true;
		}
	}
}