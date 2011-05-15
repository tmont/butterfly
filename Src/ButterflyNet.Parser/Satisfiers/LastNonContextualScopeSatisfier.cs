using System;

namespace ButterflyNet.Parser.Satisfiers {
	public class LastNonContextualScopeSatisfier : ISatisfier {
		private readonly Type scopeType;

		public LastNonContextualScopeSatisfier(Type scopeType) {
			this.scopeType = scopeType;
		}

		public bool IsSatisfiedBy(ParseContext context) {
			var scope = context.Scopes.LastNonContextualScope();
			return scope != null && scope.GetType() == scopeType;
		}
	}
}