using System;
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class TableStrategy : ScopeDrivenStrategy, ITokenProvider {
		public TableStrategy() {
			AddSatisfier<TableSatisfier>();
		}

		protected override void Execute(ParseContext context) {
			IScope rowScope;
			if (context.Input.Peek() == '{') {
				context.AdvanceInput();
				rowScope = new TableRowScope();
			} else {
				rowScope = new TableRowLineScope();
			}

			Type cellType = ScopeTypeCache.TableCell;
			if (context.Input.Peek() == '!') {
				context.AdvanceInput();
				cellType = ScopeTypeCache.TableHeader;
			}

			context.Input.SeekToNonWhitespace();
			context.UpdateCurrentChar();

			if (!context.Scopes.ContainsType(ScopeTypeCache.Table)) {
				//no tables, so create a new one
				OpenScope(new TableScope(), context);
				OpenScope(rowScope, context);
			} else if (context.Scopes.ContainsType(ScopeTypeCache.TableCell, ScopeTypeCache.TableHeader)) {
				//close table cell
				CloseScopeUntil(context, ScopeTypeCache.TableCell, ScopeTypeCache.TableHeader);
				CloseCurrentScope(context);
			} else if (!context.Scopes.ContainsType(ScopeTypeCache.TableRow, ScopeTypeCache.TableRowLine)) {
				OpenScope(rowScope, context);
			}

			//figure out if we need to open a new cell or not
			//criteria:
			// - not EOL
			// - we're in a row terminated by a line break and the next char is not a new line (signifying a new row)
			// - OR we're in a row not terminated by a line break
			if (
				context.Input.Peek() != ButterflyStringReader.NoValue 
					&& (
						(context.Scopes.ContainsType(ScopeTypeCache.TableRowLine) && context.Input.Peek() != '\n') 
							|| context.Scopes.ContainsType(ScopeTypeCache.TableRow)
						)
				) {
				OpenScope(cellType == ScopeTypeCache.TableHeader ? (IScope)new TableHeaderScope() : new TableCellScope(), context);
			}
		}

		private class TableSatisfier : ISatisfier {
			private readonly ISatisfier startOfLine = new StartOfLineSatisfier();
			private readonly ISatisfier containsTableScope = new InScopeStackSatisfier(ScopeTypeCache.Table);

			public bool IsSatisfiedBy(ParseContext context) {
				return startOfLine.IsSatisfiedBy(context) || containsTableScope.IsSatisfiedBy(context);
			}
		}

		public string Token { get { return "|"; } }
	}
}