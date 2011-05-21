using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[Exclude]
	public class OpenParagraphStrategy : ScopeDrivenStrategy {
		public OpenParagraphStrategy() {
			AddSatisfier<CanNestParagraphSatisfier>();
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new ParagraphScope(), context);
		}

		private class CanNestParagraphSatisfier : ISatisfier {
			public bool IsSatisfiedBy(ParseContext context) {
				return context.Scopes.IsEmpty() || context.Scopes.Peek().CanNestParagraph;
			}
		}
	}
}