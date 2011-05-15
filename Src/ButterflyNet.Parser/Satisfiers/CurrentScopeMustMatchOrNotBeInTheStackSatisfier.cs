using System;

namespace ButterflyNet.Parser.Satisfiers {
	public class CurrentScopeMustMatchOrNotBeInTheStackSatisfier : ISatisfier {
		private readonly Type scopeType;

		public CurrentScopeMustMatchOrNotBeInTheStackSatisfier(Type scopeType) {
			this.scopeType = scopeType;
		}

		public bool IsSatisfiedBy(ParseContext context) {
			var currentScope = context.Scopes.PeekOrDefault();
			return currentScope == null || currentScope.GetType() == scopeType || !context.Scopes.ContainsType(scopeType);
		}
	}
}