using System;

namespace ButterflyNet.Parser.Strategies {
	[Exclude]
	public sealed class WriteStringStrategy : ParseStrategyBase {
		private IParseStrategy paragraphStrategy;
		private bool emptying;
		[ThreadStatic]
		private static WriteStringStrategy instance;

		private WriteStringStrategy() {
			AddSatisfier(context => !emptying);
			AddSatisfier(context => context.Buffer.Length > 0);
			AddSatisfier<CanNestTextSatisfier>();
		}

		public static IParseStrategy Instance { get { return instance ?? (instance = new WriteStringStrategy()); } }

		protected override bool Scopable { get { return false; } }

		private IParseStrategy ParagraphStrategy { get { return paragraphStrategy ?? (paragraphStrategy = new OpenParagraphStrategy()); } }

		protected override void Execute(ParseContext context) {
			emptying = true;

			ParagraphStrategy.ExecuteIfSatisfied(context);

			var bufferContents = context.Buffer.ToString();
			context.Buffer.Clear();
			if (context.CurrentNode != null && context.CurrentNode.Scope.GetType() == ScopeTypeCache.Unescaped) {
				context.Analyzers.Walk(converter => converter.WriteUnescapedString(bufferContents));
			} else {
				context.Analyzers.Walk(converter => converter.WriteAndEscapeString(bufferContents));
			}

			emptying = false;
		}

		private class CanNestTextSatisfier : ISatisfier {
			public bool IsSatisfiedBy(ParseContext context) {
				return context.Scopes.IsEmpty() || context.Scopes.Peek().CanNestText;
			}
		}
	}
}