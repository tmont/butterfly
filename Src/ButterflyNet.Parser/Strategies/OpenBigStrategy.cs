using System;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public sealed class OpenBigStrategy : InlineStrategy, ITokenProvider {
		protected override void Execute(ParseContext context) {
			OpenScope(new BigScope(), context);
		}

		protected override Type Type { get { return ScopeTypeCache.Big; } }
		public string Token { get { return "(+"; } }
	}
}