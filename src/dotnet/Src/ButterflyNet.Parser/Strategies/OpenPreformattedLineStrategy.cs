using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer(" ")]
	public class OpenPreformattedLineStrategy : ScopeDrivenStrategy {
		public OpenPreformattedLineStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new PreformattedLineScope(), context);
		}
	}
}