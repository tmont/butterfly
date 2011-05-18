using System;
using System.Collections.Generic;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {

	public interface INewlineScopeClosingStrategy {
		Type ScopeType { get; }
		bool ShouldClose(ParseContext context);
	}

	public sealed class AlwaysTrueScopeClosingStrategy : INewlineScopeClosingStrategy {
		public AlwaysTrueScopeClosingStrategy(Type scopeType) {
			ScopeType = scopeType;
		}

		public Type ScopeType { get; private set; }

		public bool ShouldClose(ParseContext context) {
			return true;
		}
	}

	public sealed class DefinitionListScopeClosingStrategy : INewlineScopeClosingStrategy {
		public Type ScopeType { get { return ScopeTypeCache.DefinitionList; } }

		public bool ShouldClose(ParseContext context) {
			//if the next char opens another term, then we don't close the list
			return context.Input.Peek() != ';' && context.Input.Peek() != ':';
		}
	}

	[TokenTransformer("\n")]
	public class NewlineStrategy : ScopeDrivenStrategy {
		private readonly ClosingStrategyMap closingStrategyMap;
		private readonly ParseStrategy paragraphStrategy = new OpenParagraphStrategy();

		private class ClosingStrategyMap {
			private readonly IDictionary<Type, Func<ParseContext, bool>> scopeClosingStrategyMap = new Dictionary<Type, Func<ParseContext, bool>>();

			public ClosingStrategyMap(IEnumerable<INewlineScopeClosingStrategy> scopeClosingStrategies) {
				foreach (var strategy in scopeClosingStrategies ?? DefaultScopeClosingStrategies) {
					scopeClosingStrategyMap[strategy.ScopeType] = strategy.ShouldClose;
				}
			}

			public Func<ParseContext, bool> this[Type scopeType] {
				get {
					return !scopeClosingStrategyMap.ContainsKey(scopeType) ? null : scopeClosingStrategyMap[scopeType];
				}
			}
		}

		public NewlineStrategy() : this(null) { }

		public NewlineStrategy(IEnumerable<INewlineScopeClosingStrategy> scopeClosingStrategies) {
			closingStrategyMap = new ClosingStrategyMap(scopeClosingStrategies);
			AddSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.PreformattedLine, ScopeTypeCache.Preformatted)));
		}

		public static IEnumerable<INewlineScopeClosingStrategy> DefaultScopeClosingStrategies {
			get {
				yield return new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.Definition);
				yield return new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.DefinitionTerm);
				yield return new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.TableRowLine);
				yield return new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.PreformattedLine);
				yield return new DefinitionListScopeClosingStrategy();
			}
		}

		protected override void DoExecute(ParseContext context) {
			//close scopes that should be closed by a line break
			//- list items
			//- lists
			//- table rows
			//- table cells
			//- tables
			//- definitions
			//- definition terms
			//- definition lists
			//- preformatted

			while (!context.Scopes.IsEmpty()) {
				var shouldCloseScope = closingStrategyMap[context.Scopes.Peek().GetType()];
				if (shouldCloseScope == null || !shouldCloseScope(context)) {
					break;
				}

				CloseCurrentScope(context);
			}

			if (context.Input.Peek() == '\n') {
				//consecutive line breaks means close a paragraph
				CloseParagraph(context);
			}
		}

		private void CloseParagraph(ParseContext context) {
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