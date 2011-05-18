using System;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public sealed class CloseBigStrategy : InlineStrategy {
		public CloseBigStrategy() {
			AddSatisfier(new InScopeStackSatisfier(Type));
			AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
			AddSatisfier(new ExactCharMatchSatisfier("+)"));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}

		protected override Type Type { get { return ScopeTypeCache.Big; } }
	}
}