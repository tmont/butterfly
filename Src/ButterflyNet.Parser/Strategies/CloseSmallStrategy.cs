using System;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public sealed class CloseSmallStrategy : InlineStrategy, ITokenProvider {
		public CloseSmallStrategy() {
			AddSatisfier(new InScopeStackSatisfier(Type));
			AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
		}

		protected override void Execute(ParseContext context) {
			CloseCurrentScope(context);
		}

		protected override Type Type { get { return ScopeTypeCache.Small; } }
		public string Token { get { return "-)"; } }
	}
}