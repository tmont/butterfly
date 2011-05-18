using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class CloseBlockquoteStrategy : ScopeDrivenStrategy, ITokenProvider {
		public CloseBlockquoteStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Blockquote));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}

		public string Token { get { return ">>"; } }
	}
}