using System;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public sealed class OpenBigStrategy : InlineStrategy {
		public OpenBigStrategy() {
			AddSatisfier(new ExactCharMatchSatisfier("(+"));
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new BigScope(), context);
		}

		protected override Type Type { get { return ScopeTypeCache.Big; } }
	}
}