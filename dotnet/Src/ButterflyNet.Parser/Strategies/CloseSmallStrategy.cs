using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("-)")]
	public sealed class CloseSmallStrategy : InlineStrategy {
		public CloseSmallStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.Small));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}
	}
}