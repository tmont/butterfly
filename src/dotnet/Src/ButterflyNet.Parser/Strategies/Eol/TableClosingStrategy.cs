using System;

namespace ButterflyNet.Parser.Strategies.Eol {
	public sealed class TableClosingStrategy :IEolScopeClosingStrategy {
		public Type ScopeType { get { return ScopeTypeCache.Table; } }

		public bool ShouldClose(ParseContext context) {
			return context.Input.Peek() != '|';
		}
	}
}