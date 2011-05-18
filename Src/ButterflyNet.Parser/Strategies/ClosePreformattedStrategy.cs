using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("}}}")]
	public class ClosePreformattedStrategy : ScopeDrivenStrategy {

		public ClosePreformattedStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Preformatted));
		}

		protected override void DoExecute(ParseContext context) {
			CloseScopeUntil(context, ScopeTypeCache.Preformatted);
			CloseCurrentScope(context);
		}
	}
}