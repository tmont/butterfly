using System;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public sealed class OpenSmallStrategy : InlineStrategy, ITokenProvider {
		protected override void Execute(ParseContext context) {
			OpenScope(new SmallScope(), context);
		}

		protected override Type Type { get { return ScopeTypeCache.Small; } }
		public string Token { get { return "(-"; } }
	}
}