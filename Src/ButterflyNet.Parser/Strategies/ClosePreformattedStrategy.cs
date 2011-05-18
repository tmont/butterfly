using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class ClosePreformattedStrategy : ScopeDrivenStrategy {

		public ClosePreformattedStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Preformatted));
			AddSatisfier(new ExactCharMatchSatisfier("}}}"));
		}

		protected override void DoExecute(ParseContext context) {
			CloseScopeUntil(context, ScopeTypeCache.Preformatted);
			CloseCurrentScope(context);
		}
	}
}