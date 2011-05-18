﻿using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class ParagraphOnConsecutiveNewlinesStrategy : ScopeDrivenStrategy {
		private readonly ParseStrategy paragraphStrategy = new OpenParagraphStrategy();

		public ParagraphOnConsecutiveNewlinesStrategy() {
			//preformatted line breaks are significant
			AddSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Preformatted, ScopeTypeCache.PreformattedLine)));
			AddSatisfier(new ExactCharMatchSatisfier("\n\n"));
		}

		protected override void DoExecute(ParseContext context) {
			context.AdvanceInput();

			paragraphStrategy.ExecuteIfSatisfied(context);

			var currentScope = context.Scopes.PeekOrDefault();

			if (currentScope != null && currentScope.GetType() == ScopeTypeCache.Paragraph) {
				//close the current paragraph, but only if all other scopes are closed
				CloseCurrentScope(context);

				//ignore the next consecutive linebreaks
				while (context.Input.Peek() == '\n') {
					context.AdvanceInput();
				}
			}
		}
	}
}