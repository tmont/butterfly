using System;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public sealed class CloseBigStrategy : InlineStrategy, ITokenProvider {
		public CloseBigStrategy() {
			AddSatisfier(new InScopeStackSatisfier(Type));
			AddPreExecuteSatisfier(new CurrentScopeMustMatchSatisfier(Type));
		}

		protected override void Execute(ParseContext context) {
			CloseCurrentScope(context);
		}

		protected override Type Type { get { return ScopeTypeCache.Big; } }
		public string Token { get { return "+)"; } }
	}
}