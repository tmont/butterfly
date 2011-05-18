using System;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("-)")]
	public sealed class CloseSmallStrategy : InlineStrategy {
		public CloseSmallStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}

		protected override Type Type { get { return ScopeTypeCache.Small; } }
	}
}