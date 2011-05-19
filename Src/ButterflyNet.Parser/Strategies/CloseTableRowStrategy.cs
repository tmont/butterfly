using System;
using System.Linq;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("}|")]
	public class CloseTableRowStrategy : ScopeDrivenStrategy {
		private static readonly Type[] closableScopes = new[] { ScopeTypeCache.TableCell, ScopeTypeCache.TableHeader, ScopeTypeCache.Paragraph };

		public CloseTableRowStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.TableRow));
		}

		protected override void DoExecute(ParseContext context) {
			//close paragraphs and table cells/headers
			while (!context.Scopes.IsEmpty()) {
				if (!closableScopes.Contains(context.Scopes.Peek().GetType())) {
					break;
				}

				CloseCurrentScope(context);
			}

			var currentScope = context.Scopes.PeekOrDefault();
			if (currentScope == null || currentScope.GetType() != ScopeTypeCache.TableRow) {
				throw new ParseException("Cannot close table row until all containing scopes are closed");
			}

			CloseCurrentScope(context);
		}
	}
}