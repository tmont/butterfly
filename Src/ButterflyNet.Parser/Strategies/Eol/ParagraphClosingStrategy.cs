using System;

namespace ButterflyNet.Parser.Strategies.Eol {
	public sealed class ParagraphClosingStrategy : INewlineScopeClosingStrategy {
		public Type ScopeType { get { return ScopeTypeCache.Paragraph; } }

		public bool ShouldClose(ParseContext context) {
			return context.Input.IsEof || context.Input.Peek() == ButterflyStringReader.NoValue || context.Input.Peek() == '\n';
		}
	}
}