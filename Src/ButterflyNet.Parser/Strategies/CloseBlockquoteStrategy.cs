using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class CloseBlockquoteStrategy : ScopeDrivenStrategy {
		public CloseBlockquoteStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Blockquote));
			AddSatisfier(new ExactCharMatchSatisfier(">>"));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}
	}
}