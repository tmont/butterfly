using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("}}}")]
	public class ClosePreformattedStrategy : ScopeDrivenStrategy {

		public ClosePreformattedStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.Preformatted));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}
	}
}