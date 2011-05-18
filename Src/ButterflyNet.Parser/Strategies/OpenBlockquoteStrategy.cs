using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class OpenBlockquoteStrategy : ScopeDrivenStrategy {
		public OpenBlockquoteStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
			AddSatisfier(new ExactCharMatchSatisfier("<<"));
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new BlockquoteScope(), context);
		}
	}
}