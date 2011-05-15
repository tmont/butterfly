using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class OpenBlockquoteStrategy : BlockStrategy, ITokenProvider {
		public OpenBlockquoteStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
		}

		protected override void Execute(ParseContext context) {
			OpenScope(new BlockquoteScope(), context);
		}

		public string Token { get { return "<<"; } }
	}
}