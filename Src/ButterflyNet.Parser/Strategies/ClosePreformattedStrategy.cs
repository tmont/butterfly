using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class ClosePreformattedStrategy : ScopeDrivenStrategy, ITokenProvider {

		public ClosePreformattedStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Preformatted));
		}

		protected override void Execute(ParseContext context) {
			CloseScopeUntil(context, ScopeTypeCache.Preformatted);
			CloseCurrentScope(context);
		}

		public string Token { get { return "}}}"; } }
	}
}