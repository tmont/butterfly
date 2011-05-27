using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer(">>")]
	public class CloseBlockquoteStrategy : ScopeDrivenStrategy {
		public CloseBlockquoteStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Blockquote));
			AddSatisfier(new CurrentScopeMustMatchOrBeParagraphSatisfier(ScopeTypeCache.Blockquote));
		}

		protected override void DoExecute(ParseContext context) {
			CloseParagraphIfNecessary(context);
			CloseCurrentScope(context);
		}
	}
}