using System;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("+)")]
	public sealed class CloseBigStrategy : InlineStrategy {
		public CloseBigStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}

		protected override Type Type { get { return ScopeTypeCache.Big; } }
	}
}