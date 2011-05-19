using System;

namespace ButterflyNet.Parser.Strategies.Eol {
	public sealed class DefinitionListScopeClosingStrategy : INewlineScopeClosingStrategy {
		public Type ScopeType { get { return ScopeTypeCache.DefinitionList; } }

		public bool ShouldClose(ParseContext context) {
			//if the next char opens another term, then we don't close the list
			var peek = context.Input.Peek();
			return peek != ';' && peek != ':';
		}
	}
}