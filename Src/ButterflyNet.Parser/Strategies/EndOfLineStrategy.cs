using System;
using System.Collections.Generic;
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Strategies.Eol;

namespace ButterflyNet.Parser.Strategies {
	public class EndOfLineStrategy : ScopeDrivenStrategy {
		private readonly ClosingStrategyMap closingStrategyMap;

		private class EolSatisfier : ISatisfier {
			public bool IsSatisfiedBy(ParseContext context) {
				return context.CurrentChar == '\n' || context.Input.IsEof;
			}
		}

		private class ClosingStrategyMap {
			private readonly IDictionary<Type, Func<ParseContext, bool>> scopeClosingStrategyMap = new Dictionary<Type, Func<ParseContext, bool>>();

			public ClosingStrategyMap(IEnumerable<IEolScopeClosingStrategy> scopeClosingStrategies) {
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

		public EndOfLineStrategy(IEnumerable<IEolScopeClosingStrategy> scopeClosingStrategies) {
			closingStrategyMap = new ClosingStrategyMap(scopeClosingStrategies);
			AddSatisfier<EolSatisfier>();
			AddSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Preformatted)));
		}

		public static IEnumerable<IEolScopeClosingStrategy> DefaultScopeClosingStrategies {
			get {
				yield return new ParagraphClosingStrategy();

				yield return new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.Header);

				yield return new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.Definition);
				yield return new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.DefinitionTerm);
				yield return new DefinitionListScopeClosingStrategy();

				yield return new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.TableRowLine);
				yield return new TableCellClosingStrategy(ScopeTypeCache.TableCell);
				yield return new TableCellClosingStrategy(ScopeTypeCache.TableHeader);
				yield return new TableClosingStrategy();

				yield return new AlwaysTrueScopeClosingStrategy(ScopeTypeCache.PreformattedLine);

				yield return new ListItemClosingStrategy();
				yield return new ListClosingStrategy(ScopeTypeCache.UnorderedList);
				yield return new ListClosingStrategy(ScopeTypeCache.OrderedList);
			}
		}

		protected override void DoExecute(ParseContext context) {
			//close scopes that should be closed by a line break
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