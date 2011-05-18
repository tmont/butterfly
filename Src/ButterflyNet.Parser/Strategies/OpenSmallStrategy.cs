using System;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public sealed class OpenSmallStrategy : InlineStrategy {
		public OpenSmallStrategy() {
			AddSatisfier(new ExactCharMatchSatisfier("(-"));
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new SmallScope(), context);
		}

		protected override Type Type { get { return ScopeTypeCache.Small; } }
	}
}