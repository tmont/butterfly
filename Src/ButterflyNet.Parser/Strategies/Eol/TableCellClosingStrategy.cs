using System;

namespace ButterflyNet.Parser.Strategies.Eol {
	public sealed class TableCellClosingStrategy : IEolScopeClosingStrategy {
		public TableCellClosingStrategy(Type scopeType) {
			ScopeType = scopeType;
		}

		public Type ScopeType { get; private set; }

		public bool ShouldClose(ParseContext context) {
			//if it contains a tablerow scope, then we don't close table cells on a newline
			//because the row is closed manually
			return !context.Scopes.ContainsType(ScopeTypeCache.TableRow);
		}
	}
}