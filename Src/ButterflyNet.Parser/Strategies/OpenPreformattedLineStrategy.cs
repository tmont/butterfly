using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class OpenPreformattedLineStrategy : ScopeDrivenStrategy {
		public OpenPreformattedLineStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
			AddSatisfier(new ExactCharMatchSatisfier(" "));
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new PreformattedLineScope(), context);
		}
	}
}