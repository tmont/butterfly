using System;

namespace ButterflyNet.Parser.Strategies.Eol {
	public sealed class ParagraphClosingStrategy : IEolScopeClosingStrategy {
		public Type ScopeType { get { return ScopeTypeCache.Paragraph; } }

		public bool ShouldClose(ParseContext context) {
			var peek = context.Input.Peek();
			return context.Input.IsEof || peek == ButterflyStringReader.NoValue || peek == '\n';
		}
	}
}