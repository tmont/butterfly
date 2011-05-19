using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class WriteCharacterStrategy : ParseStrategy {
		private ParseStrategy paragraphStrategy;

		public WriteCharacterStrategy() {
			AddSatisfier<NegatingSatisfier<EofSatisfier>>();
			AddSatisfier<CanNestTextSatisfier>();
		}

		public override int Priority { get { return int.MaxValue; } }

		private ParseStrategy ParagraphStrategy { get { return paragraphStrategy ?? (paragraphStrategy = new OpenParagraphStrategy()); } }

		protected override sealed void DoExecute(ParseContext context) {
			var c = GetChar(context);

			ParagraphStrategy.ExecuteIfSatisfied(context);

			if (context.CurrentNode != null && context.CurrentNode.Scope.GetType() == ScopeTypeCache.Unescaped) {
				context.Analyzer.WriteUnescapedChar(c);
			} else {
				context.Analyzer.WriteAndEscape(c);
			}
		}

		/// <summary>
		/// Derived classes can override this method to handle any escaping
		/// </summary>
		protected virtual char GetChar(ParseContext context) {
			return (char)context.CurrentChar;
		}

		private class CanNestTextSatisfier : ISatisfier {
			public bool IsSatisfiedBy(ParseContext context) {
				return context.Scopes.IsEmpty() || context.Scopes.Peek().CanNestText;
			}
		}
	}
}