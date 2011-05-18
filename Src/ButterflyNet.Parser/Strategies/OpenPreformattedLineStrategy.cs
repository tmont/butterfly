using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class OpenPreformattedLineStrategy : ScopeDrivenStrategy, ITokenProvider {
		public OpenPreformattedLineStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new PreformattedLineScope(), context);
		}

		public string Token { get { return " "; } }
	}
}