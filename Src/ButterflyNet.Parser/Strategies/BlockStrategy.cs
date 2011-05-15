using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public abstract class BlockStrategy : ScopeDrivenStrategy {
		protected BlockStrategy() {
			AddPreExecuteSatisfier<CannotNestInsideInlineSatisfier>();
		}

		protected void CloseContextualScopes(ParseContext context) {
			while (!context.Scopes.IsEmpty()) {
				var currentScope = context.Scopes.Peek();
				if (!currentScope.ClosesOnContext) {
					break;
				}

				CloseCurrentScope(context);
			}
		}
	}
}