using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[Exclude]
	public class CloseParagraphStrategy : ScopeDrivenStrategy {
		public CloseParagraphStrategy() {
			AddPreExecuteSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.Paragraph));
		}

		protected override void Execute(ParseContext context) {
			CloseCurrentScope(context);
		}
	}
}