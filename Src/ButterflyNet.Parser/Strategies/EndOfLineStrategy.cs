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
			var peek = context.Input.Peek();
			return peek != ';' && peek != ':';
		}
	}

	public sealed class ParagraphClosingStrategy : INewlineScopeClosingStrategy {
		public Type ScopeType { get { return ScopeTypeCache.Paragraph; } }

		public bool ShouldClose(ParseContext context) {
			return context.Input.IsEof || context.Input.Peek() == ButterflyStringReader.NoValue || context.Input.Peek() == '\n';
		}
	}

	public class EndOfLineStrategy : ScopeDrivenStrategy {
		private readonly ClosingStrategyMap closingStrategyMap;

		private class EolSatisfier : ISatisfier {
			public bool IsSatisfiedBy(ParseContext context) {
				return context.CurrentChar == '\n' || context.Input.IsEof;
			}
		}

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

		public EndOfLineStrategy() : this(null) { }

		public EndOfLineStrategy(IEnumerable<INewlineScopeClosingStrategy> scopeClosingStrategies) {
			closingStrategyMap = new ClosingStrategyMap(scopeClosingStrategies);
			AddSatisfier<EolSatisfier>();
			AddSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Preformatted)));
		}

		public static IEnumerable<INewlineScopeClosingStrategy> DefaultScopeClosingStrategies {
			get {
				yield return new ParagraphClosingStrategy();
				yield return new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.Header);
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
			//- headers
			//- paragraphs

			while (!context.Scopes.IsEmpty()) {
				var shouldCloseScope = closingStrategyMap[context.Scopes.Peek().GetType()];
				if (shouldCloseScope == null || !shouldCloseScope(context)) {
					break;
				}

				CloseCurrentScope(context);
			}

			while (context.Input.Peek() == '\n') {
				context.AdvanceInput();
			}
		}
	}
}