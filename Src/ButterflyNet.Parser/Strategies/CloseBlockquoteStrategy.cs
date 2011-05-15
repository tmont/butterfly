using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class CloseBlockquoteStrategy : BlockStrategy, ITokenProvider {
		public CloseBlockquoteStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Blockquote));
			AddSatisfier(new LastNonContextualScopeSatisfier(ScopeTypeCache.Blockquote));
		}

		protected override void Execute(ParseContext context) {
			CloseContextualScopes(context);
			CloseCurrentScope(context);
		}

		public string Token { get { return ">>"; } }
	}
}