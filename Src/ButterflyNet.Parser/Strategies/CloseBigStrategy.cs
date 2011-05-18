using System;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public sealed class CloseBigStrategy : InlineStrategy, ITokenProvider {
		public CloseBigStrategy() {
			AddSatisfier(new InScopeStackSatisfier(Type));
			AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}

		protected override Type Type { get { return ScopeTypeCache.Big; } }
		public string Token { get { return "+)"; } }
	}
}