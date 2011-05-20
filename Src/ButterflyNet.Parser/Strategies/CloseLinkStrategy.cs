using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("]")]
	public class CloseLinkStrategy : InlineStrategy {
		public CloseLinkStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.Link));
			AddSatisfier<NextCharacterIsNotTheSameSatisfier>();
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}
	}
}