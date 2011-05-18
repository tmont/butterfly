using System;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public sealed class CloseSmallStrategy : InlineStrategy {
		public CloseSmallStrategy() {
			AddSatisfier(new InScopeStackSatisfier(Type));
			AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
			AddSatisfier(new ExactCharMatchSatisfier("-)"));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}

		protected override Type Type { get { return ScopeTypeCache.Small; } }
	}
}