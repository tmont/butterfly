using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class OpenBlockquoteStrategy : ScopeDrivenStrategy, ITokenProvider {
		public OpenBlockquoteStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new BlockquoteScope(), context);
		}

		public string Token { get { return "<<"; } }
	}
}