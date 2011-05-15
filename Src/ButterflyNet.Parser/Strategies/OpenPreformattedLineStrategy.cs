using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class OpenPreformattedLineStrategy : BlockStrategy, ITokenProvider {
		public OpenPreformattedLineStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
		}

		protected override void Execute(ParseContext context) {
			OpenScope(new PreformattedLineScope(), context);
		}

		public string Token { get { return " "; } }
	}
}